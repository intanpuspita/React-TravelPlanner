using MailKit.Net.Smtp;
using MimeKit;
using System;
using TravelPlanner.Logic.Interfaces;

namespace TravelPlanner.Logic.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string email, string name)
        {
            try
            {
                //From Address 
                string FromAddress = "intanpusss@gmail.com";
                string FromAdressTitle = "Travel Planner App";
                //To Address 
                string ToAddress = email;
                string ToAdressTitle = name;
                string Subject = "Confirm Your Account";
                string BodyContent = "<html><body><p>Hi " + name + ",</p>"
                                     + "<p>Thanks for registering your account.<br />"
                                     //+ "Before we get started, we'll need to verify your email."
                                     + "To confirm your account, you need to click on the following link :</p>"
                                     + "<p><a href='#'>this-is-confirmation-link</a></p></body></html>";

                //Smtp Server 
                string SmtpServer = "smtp.gmail.com";
                //Smtp Port Number 
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress)); 
                    mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress)); 
                    mimeMessage.Subject = Subject; 
                    mimeMessage.Body = new TextPart("html")
                    {
                        Text = BodyContent
                    };

                using (var client = new SmtpClient()) 
                { 
                    client.Connect(SmtpServer, SmtpPortNumber, false); 
                    // Note: only needed if the SMTP server requires authentication 
                    // Error 5.5.1 Authentication  
                    client.Authenticate("apptest.intanpuspita@gmail.com", "wswtvsvssixwexvn"); 
                    client.Send(mimeMessage); 
                    Console.WriteLine("The mail has been sent successfully !!");
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
