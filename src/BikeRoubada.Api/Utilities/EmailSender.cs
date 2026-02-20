using FluentEmail.Core;
namespace BikeRoubada.Api.Utilities
{
    public class EmailSender : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailSender(IFluentEmail fluentEmail) 
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(EmailModel emailModel)
        {
            var email = _fluentEmail
                .To(emailModel.Target)
                .Subject(emailModel.Subject)
                .Body(emailModel.Body, isHtml: true);

            await email.SendAsync();
        }
    }
}
