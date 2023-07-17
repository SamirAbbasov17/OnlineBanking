using BankSystem.Common.EmailSender.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;

namespace BankSystem.Common.EmailSender.Implementation
{
    public class EmailSender : IEmailSender
    {
        //private readonly string apiKey;

        //public EmailSender(string apiKey)
        //{
        //    this.apiKey = apiKey;
        //}

        private readonly SendGridConfiguration options;

        public EmailSender(IOptions<SendGridConfiguration> options)
            => this.options = options.Value;

        public async Task<bool> SendEmailAsync(string receiver, string subject, string htmlMessage)
            => await this.SendEmailAsync(GlobalConstants.BankSystemEmail, receiver, subject, htmlMessage);


        public async Task<bool> SendEmailAsync(string sender, string receiver, string subject, string htmlMessage)
        {
            //try
            //{
            //    var smtpClient = new SmtpClient("smtp-relay.sendinblue.com", 587);
            //    smtpClient.Credentials = new System.Net.NetworkCredential("apikey", this.options.ApiKey);

            //    var mailMessage = new MailMessage(sender, receiver, subject, htmlMessage);
            //    mailMessage.IsBodyHtml = true;

            //    smtpClient.Send(mailMessage);

            //    return true;
            //} 
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}

            sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Remove("api-key");
            sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Add("api-key", this.options.ApiKey);

            var apiInstance = new TransactionalEmailsApi();
            SendSmtpEmailSender Email = new SendSmtpEmailSender(sender, sender);
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(receiver, receiver);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, htmlContent: htmlMessage, subject:  subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }










        //private readonly SendGridConfiguration options;

        //public EmailSender(IOptions<SendGridConfiguration> options)
        //    => this.options = options.Value;

        //public async Task<bool> SendEmailAsync(string receiver, string subject, string htmlMessage)
        //    => await this.SendEmailAsync(GlobalConstants.BankSystemEmail, receiver, subject, htmlMessage);

        //public async Task<bool> SendEmailAsync(string sender, string receiver, string subject, string htmlMessage)
        //{
        //    var client = new SendGridClient(this.options.ApiKey);
        //    var from = new EmailAddress(sender);
        //    var to = new EmailAddress(receiver, receiver);
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);

        //    try
        //    {
        //        var isSuccessful = await client.SendEmailAsync(msg);

        //        return isSuccessful.StatusCode == HttpStatusCode.Accepted;
        //    }
        //    catch
        //    {
        //        // Ignored
        //        return false;
        //    }
        //}
    }
}
