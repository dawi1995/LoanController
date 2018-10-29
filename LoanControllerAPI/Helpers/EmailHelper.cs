using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LoanControllerAPI.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(string subject, string content, string receiverAddress = "dawidsonb95@gmail.com")
        {
            var fromAddress = new MailAddress("loancontroller@gmail.com", "LoanController App");
            var toAddress = new MailAddress(receiverAddress);
            const string fromPassword = "AstonMartinDB11";
            string body = content;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            //Attachment att = new Attachment(new MemoryStream(PDFFile), "PDF.pdf");
            //att.ContentType.MediaType = MediaTypeNames.Application.Pdf;
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
            })
            {
                //message.Attachments.Add(att);
                smtp.Send(message);
            }
        }
    }
}
