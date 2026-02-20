namespace BikeRoubada.Api.Utilities
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel emailModel);
    }
}
