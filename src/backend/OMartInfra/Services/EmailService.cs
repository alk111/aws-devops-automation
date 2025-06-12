using Microsoft.Extensions.Configuration;
using OMartApplication.Services;
using System.Net;
using System.Net.Mail;
using OMartDomain.Models.Utility;

namespace OMartInfra.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSetting emailSetting;

        private readonly string UserEmailSubject = "Welcome To Suuq";

        private readonly string UserCreationEmailBody = @"
                                                    <!DOCTYPE html>
                                                    <html>
                                                    <head>
                                                        <title>Greeting</title>
                                                    </head>
                                                    <body>
                                                        <h1>Welcome to  Suuq!</h1>
                                                        <p>Dear {{UserName}},</p>
                                                        <p>Thank you for joining our service. We're excited to have you with us!</p>
                                                        <p>Please verify your email. Here's your OTP below</p>
                                                        <h1>{{OTP}}</h1>
                                                        <p>Best regards,<br>Suuq Team</p>
                                                    </body>
                                                    </html>";

        private readonly string resetEmailSubject = "Password Reset Request";
        private readonly string PasswordResetEmailBody = @"
                                <!DOCTYPE html>
                                <html>
                                <head>
                                    <title>Password Reset</title>
                                </head>
                                <body>
                                    <h1>Password Reset Request</h1>
                                    <p>Dear {{UserName}},</p>
                                    <p>You have requested to reset your password. Please verify your email. Here's your OTP below</p>
                                    <h1>{{OTP}}</h1>
                                    <p>Best regards,<br>Suuq Team</p>
                                </body>
                                </html>";

        private readonly string SellerRegistrationEmailSubject = "Seller Registration Confirmation";
        private readonly string SellerRegistrationEmailBody = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Seller Registration</title>
            </head>
            <body>
                <h1>Welcome to Suuq as a Seller!</h1>
                <p>Dear {{UserName}},</p>
                <p>Thank you for registering as a seller on suuq. We look forward to a successful partnership with you!</p>
                <p>Your seller account has been successfully created. Here’s your verification OTP:</p>
                <h1>{{OTP}}</h1>
                <p>Best regards,<br>Suuq Team</p>
            </body>
            </html>";


        public EmailService(IConfiguration configuration)
        {
            var _emailSetting = configuration.GetSection("EmailSettings");

            emailSetting = new EmailSetting
            {
                SmtpServer = _emailSetting["SmtpServer"],
                SmtpPort = int.Parse(_emailSetting["SmtpPort"]),
                SmtpUser = _emailSetting["SmtpUser"],
                SmtpPass = _emailSetting["SmtpPass"],
                FromEmail = _emailSetting["FromEmail"],
                FromName = _emailSetting["FromName"]
            };
        }

        public async Task<bool> SendEmailAsync(string toEmail, string otp)
        {
            return await SendEmail(toEmail, otp, UserEmailSubject, UserCreationEmailBody);
        }

        public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string otp)
        {
            return await SendEmail(toEmail, otp, resetEmailSubject, PasswordResetEmailBody);
        }

        public async Task<bool> SendSellerRegistrationEmailAsync(string toEmail, string otp)
        {
            return await SendEmail(toEmail, otp, SellerRegistrationEmailSubject, SellerRegistrationEmailBody);
        }

        private async Task<bool> SendEmail(string toEmail, string otp, string subject, string bodyTemplate)
        {
            try
            {
                var message = GenerateHtmlMessage(ExtractNameFromEmail(toEmail), otp, bodyTemplate);

                using (var client = new SmtpClient(emailSetting.SmtpServer, emailSetting.SmtpPort))
                {
                    client.Credentials = new NetworkCredential(emailSetting.SmtpUser, emailSetting.SmtpPass);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(emailSetting.FromEmail, emailSetting.FromName),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toEmail);
                    await client.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private string GenerateHtmlMessage(string userName, string otp, string messageType)
        {
            return messageType
                .Replace("{{UserName}}", userName)
                .Replace("{{OTP}}", $"{otp}");
        }

        public string ExtractNameFromEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var localPart = email.Split('@')[0];
            var name = localPart.Replace('.', ' ').Replace('_', ' ');
            var capitalizedName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());

            return capitalizedName;
        }
    }
}