using BikeRoubada.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BikeRoubada.Api.AuxiliaryModels;
using BikeRoubada.Api.ViewModels;
using System.Net;
using BikeRoubada.Api.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;
using BikeRoubada.Business.Models;
using AutoMapper;

namespace BikeRoubada.Api.Controllers
{

    //TODO: Implementar a alteração de senha

    [Route("api/conta")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuxiliaryModels.JwtSettings _jwtSettings;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AuxiliaryModels.JwtSettings> jwtSettings,
                              INotificador notificador,
                              IUsuarioRepository usuarioRepository,
                              IEmailSender emailSender,
                              IUsuarioService usuarioService,
                              IMapper mapper) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _usuarioRepository = usuarioRepository;
            _emailSender = emailSender;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }   

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(HttpStatusCode.BadRequest,  ModelState);
            }

            if(_usuarioRepository.Buscar(u => u.Email == register.Email).Result.Any())
            {
                NotificarErro("Já existe um usuario cadastrado com o email fornecido");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            var usuario = _mapper.Map<Usuario>(register);
            await _usuarioService.Adicionar(usuario);

         

            var user = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email
            };

           
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                NotificarErro("Falha ao criar registro");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            await _signInManager.SignInAsync(user, false);
            var codeResponse = CodeResponses.UserCreatedEmailSended;
            try
            {
                await EnviarEmailConfirmacao(user);
            } catch(Exception ex)
            {
                codeResponse = CodeResponses.UserCreatedEmailNotSended;
            }
            

            return CustomResponse(HttpStatusCode.Created, 
                new { 
                    token = await GerarJwt(user.Email),
                    usuario
                    //nome = register.Nome, 
                    //email = register.Email,  
                    //codeResponse = codeResponse
                });

        }

        [HttpGet("reenviar-email-confirmacao")]
        public async Task<ActionResult> ReenviarEmailConfirmacao(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                NotificarErro("Usuario não encontrado");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                await EnviarEmailConfirmacao(user);
            }
            catch(Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            return CustomResponse(HttpStatusCode.NoContent);
        }
        
        private async Task EnviarEmailConfirmacao(IdentityUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action
                (nameof(ConfirmEmail),
                "Auth", new { token = HttpUtility.UrlEncode(token), email = HttpUtility.UrlEncode(user.Email) },
                protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(new EmailModel() { Body = $"<a href=\"{url}\">Clique Aqui</a>", Target = user.Email, Subject = "Confirmação de conta" });

        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(HttpUtility.UrlDecode(email));
            if(user == null)
            {
                NotificarErro("Requisição Inválida");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));
            if(!result.Succeeded)
            {
                NotificarErro("Falha de confirmação");
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user == null)
            {
                NotificarErro("Requisicao Invalida");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user)) 
            {
                NotificarErro("O email não foi confirmado");
                return CustomResponse(HttpStatusCode.Unauthorized);
            }
           
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);
            if (!result.Succeeded)
            {
                NotificarErro("Login ou senha incorretos");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(login.Email);



            return CustomResponse(HttpStatusCode.OK, new { 
                token = await GerarJwt(login.Email),
                usuario
            });
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ASCIIEncoding.ASCII.GetBytes(_jwtSettings.Segredo);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }
    }
}
