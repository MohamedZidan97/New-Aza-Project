using _VC.Application.Contents.MailServicesIntr;
using _VC.Domain.Contents.MailEntities;
using IMS.Domain.Entites;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using static IMS.Application.Features.Mail.MailManagementModel;

namespace _VC.Persistance.Repositories.MailServicesRep
{
    public class EmailServices : IEmailServices
    {
        private readonly OutlookMailSettings _setting;
        private readonly UserManager<ApplicationUser> userManager;
       

        public EmailServices(IOptions<OutlookMailSettings> _setting, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._setting = _setting.Value;
            this.userManager = userManager;
           
        }
        //public async Task<AccountMailResponse> CreateNewPassword(AccountResetPasswordCommend commend)
        //{

        //    var user = await userManager.FindByEmailAsync(commend.Email);

        //    if (user == null)
        //        return new AccountMailResponse() { Message = "Couldn't Change Your Password" };


        //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
        //    var result = await userManager.ResetPasswordAsync(user, token, commend.Password);

        //    if(!result.Succeeded)
        //        return new AccountMailResponse() { Message = "Couldn't Change Your Password" };

        //    return new AccountMailResponse() { Message = "Password Was Change", Success = true};
        //}

        //public async Task<AccountMailResponse> IsMailValidAsync(AccountIsMailValidCommend request) 
        //{

        //    var user = await userManager.FindByEmailAsync(request.Email);

        //    if (user.CodeVaildEmail != request.CodeVaildEmail)
        //        return new AccountMailResponse { Message = "Enter Correct Code!!" };

        //    return new AccountMailResponse { Success = true, Message = "Successfull :)" };
            
        //}

        //public async Task<AccountMailResponse> SendCodeToEmail(AccountMailRequest request)
        //{
        //    // Generate a random number
        //    Random random = new Random();
        //    int randomNumber = random.Next(1000, 9999); // Generate a 4-digit random number

            
            
            
        //    var response = await SendEmailAsync(request.Email, "Your Verification Code", $"Your verification code is: {randomNumber}");


        //    if (!response.Success)
        //        return response;
            
        //    var user = await userManager.FindByEmailAsync(request.Email);
        //    user.CodeVaildEmail = randomNumber.ToString();
        //    await userManager.UpdateAsync(user);

        //    return response;
        //}

        public async Task<AccountMailResponse> SendEmailAsync(string mailTo, string subject, string body)
        {
            var user = await userManager.FindByEmailAsync(mailTo);
            if (user == null)
                return new AccountMailResponse() { Message = "Enter Email Valid" };

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_setting.Host, _setting.Port, true);
                await smtpClient.AuthenticateAsync(_setting.Email, _setting.Password);

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"{body}",
                    TextBody = "Wellcome"
                };
                var mailMessage = new MimeMessage()
                {
                    Body = bodyBuilder.ToMessageBody()
                };
                mailMessage.From.Add(new MailboxAddress(_setting.DisplayName, _setting.Email));
                mailMessage.To.Add(new MailboxAddress("Test", mailTo));
                mailMessage.Subject = subject == null ? "Welcome" : subject;
                await smtpClient.SendAsync(mailMessage);
                smtpClient.Disconnect(true);
            }

            return new AccountMailResponse { Success = true, Message = "Check Your Email" };
        }

      
    }
}
