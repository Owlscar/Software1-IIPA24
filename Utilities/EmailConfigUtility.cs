using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Software1_IIPA24.Utilities
{
    public class EmailConfigUtility
    {
        private SmtpClient cliente;
        private MailMessage email;
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = "owlblack10@gmail.com";
        private string Password = "zstnansoaubmxgwp";//Contraseña de Aplicación zstn anso aubm xgwp
        private bool EnabledSSL = true;
        public EmailConfigUtility()
        {
            cliente = new SmtpClient(Host, Port)
            {
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(User, Password)
            };
        }
        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = false)
        {
            email = new MailMessage(User, destinatario, asunto, mensaje);
            email.IsBodyHtml = esHtlm;
            cliente.Send(email);
        }
    }
}