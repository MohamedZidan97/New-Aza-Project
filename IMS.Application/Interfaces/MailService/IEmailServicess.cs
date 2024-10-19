
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Mail.MailManagementModel;

namespace _VC.Application.Contents.MailServicesIntr
{
    public interface IEmailServices
    {
        // send  email generally
        Task<AccountMailResponse> SendEmailAsync(string to, string subject, string body);

        // Send Code 
      //  Task<AccountMailResponse> SendCodeToEmail(AccountMailRequest request);


        // Check if code is valid 
        
       // Task<AccountMailResponse> IsMailValidAsync(AccountIsMailValidCommend request);

        // reset password

      //  Task<AccountMailResponse> CreateNewPassword(AccountResetPasswordCommend commend);
    }
}
