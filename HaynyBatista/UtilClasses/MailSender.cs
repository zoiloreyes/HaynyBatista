﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Net;

namespace HaynyBatista.UtilClasses
{
    public static class MailSender
    {
     
        public static string host = "smtp.gmail.com";
        public static int port = 587;
        public static bool enableSsl = true;
        public static SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network;

        public static Boolean SendBasicEmail(string from,string fromPassword, string to, string subject, string bodyHTML)
        {
            try
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = deliveryMethod,
                    Credentials = new NetworkCredential(from, fromPassword),
                    Timeout = 20000
                };

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(to));
                msg.From = new MailAddress(from);
                msg.Subject = subject;
                msg.Body = "<p>"+ bodyHTML + "</p>";
                msg.IsBodyHtml = true;
                smtp.Send(msg);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }
}