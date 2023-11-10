using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaBL.EmailSenderProcess
{
    public interface IEmailManager
    {
        //from 
        //to
        //CC
        //BCC
        //subject
        bool SendEmail(EmailMessageModel model);

        Task SendMailAsync(EmailMessageModel model);

        bool SendEmailGmail(EmailMessageModel model);

    }

}
