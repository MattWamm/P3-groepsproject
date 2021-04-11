using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EigenMaaltijd.Pages.repository_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class WinkelwagenModel : PageModel
    {
        //public void OnGet()
        //{
        //}

        [BindProperty]

        public Mail sendmail { get; set; }

        public async Task OnPost()
        {
            string to = sendmail.To;
            string subject = sendmail.Subject;
            string body = sendmail.Body;
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = false;
            mm.From = new MailAddress("eigenmaaltijd@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("eigenmaaltijd@gmail.com", "JurZijnZusIsHot");
            await smtp.SendMailAsync(mm);
            ViewData["Message"] = "The Mail Has Been Sent To " + sendmail.To;

        }
    }
}
