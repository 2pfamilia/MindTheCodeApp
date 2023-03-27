using AppCore.Models.DTOs;

namespace MindTheCodeApp.Services.IServices
{
    public interface IEmailService
    {
        Task<bool> SendOrderConfirmationEmail(OrderEmailDTO orderEmailDTO);
    }
}
