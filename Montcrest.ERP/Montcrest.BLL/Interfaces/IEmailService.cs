namespace Montcrest.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendExamLinkAsync(string toEmail, string candidateName, string examLink);
    }
}
