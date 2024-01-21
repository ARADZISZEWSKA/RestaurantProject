using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace RestaurantPageProject
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email
            return Task.CompletedTask;
        }
    }
}
