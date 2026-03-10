using Microsoft.AspNetCore.Identity;

namespace BikeRoubada.Api.Utilities // Ajuste o namespace conforme a sua estrutura
{
    public class TradutorMensagensIdentity : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "A senha deve conter pelo menos um caractere especial (ex: @, #, $, !, etc)."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "A senha deve conter pelo menos um número ('0'-'9')."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "A senha deve conter pelo menos uma letra maiúscula ('A'-'Z')."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "A senha deve conter pelo menos uma letra minúscula ('a'-'z')."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"A senha deve conter no mínimo {length} caracteres."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"O email '{email}' já está sendo utilizado."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"O login '{userName}' já está sendo utilizado."
            };
        }
    }
}