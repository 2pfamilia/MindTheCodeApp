using AppCore.Models.DTOs;
using MindTheCodeApp.Controllers;
using MindTheCodeApp.Services.IServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using System.Net.Mail;

namespace MindTheCodeApp.Services.Implementation
{
    public class EmailService :IEmailService
    {
        Serilog.ILogger myLog = Log.ForContext<EmailService>();
        public EmailService()
        {
        }

        public async Task<bool> SendOrderConfirmationEmail(OrderEmailDTO orderEmailDTO)
        {
            try
            {
                var toEmail = new EmailAddress(orderEmailDTO.CustomerEmail);
                var fromEmail = new EmailAddress("mindthecodeteam3@gmail.com");
                var emailSubject = "Order Confirmation";
                var txtBody = ($"Thank you for your order {0},{1}. The total cost is {2} and will be shipped at {3} street.", orderEmailDTO.FirstName, orderEmailDTO.LastName, orderEmailDTO.TotalCost, orderEmailDTO.StreetAddress);
                var htmlBody = ($"Thank you for your order {0},{1}. The total cost is {2} and will be shipped at {3} street.", orderEmailDTO.FirstName, orderEmailDTO.LastName, orderEmailDTO.TotalCost, orderEmailDTO.StreetAddress);
                await SendAPIEmail(toEmail, fromEmail, emailSubject, txtBody.ToString(), htmlBody.ToString());
                return true;
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception at Send Email");
                throw;
            }
            
        }

        private async Task<bool> SendAPIEmail(EmailAddress toEmail, EmailAddress fromEmail, string emailSubject, string txtContent = "", string htmlContent = "")
        {
            try
            {
                SendGridClient _sendGridClient = new SendGridClient("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");
                var from = fromEmail;
                var subject = emailSubject;
                var to = toEmail;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, txtContent, htmlContent);
                var response = await _sendGridClient.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new ApplicationException(string.Format("Send Grid API Failed to send email to {0}", to.ToString()));
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
