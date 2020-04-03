using Email.Interfaces;
using HIMS.BL.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Email.Services
{
    public class Sender : ISender
    {
        private EmailAddress Email;
        private SendGridClient Client;

        private const string _layoutHtml =
            "<div style=\"margin-top: 20px;\">Best regards, Dev Incubator Inc.</div>" +
            "<div><img src=\"https://i.ibb.co/9tSLsd6/logo-name.png\" style=\"margin-top:26px; width:250px !important; height:100px !important;\"/>" +
            "</div>";


        public Sender(string apiKey, string email)
        {
            Email = new EmailAddress(email, "Dev Incubator Inc.");
            Client = new SendGridClient(apiKey);
        }
        public async Task MessageToUserAsync(UserDTO user, string subject, string html)
        {
            var to = new EmailAddress("vladislav.rossohin@gmail.com", $"{user.Name}");

            var htmlContent = "<div>" + html + _layoutHtml + "</div>";

            var msg = MailHelper.CreateSingleEmail(Email, to, subject, "Confirmation", htmlContent);

            await Client.SendEmailAsync(msg);
        }

        public async Task MessageToUserAsync(IEnumerable<UserDTO> users, string subject, string html)
        {
            foreach (var user in users)
            {
                await MessageToUserAsync(user, subject, html);
            }
        }
    }
}
