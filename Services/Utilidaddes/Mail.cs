using confinancia.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Kiota.Abstractions;
using MimeKit;

namespace confinancia.Services.Utilidaddes
{
    public interface IMail
    {
        void SendError(PersonaDTO persona, OportunidadDTO oportunidad);
        void SendSuccess(PersonaDTO persona, OportunidadDTO oportunidad);
    }

    public class Mail : IMail
    {
        #region CONSTRUCTOR
        private readonly IConfiguration _configuration;
        public Mail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public void SendError(PersonaDTO persona, OportunidadDTO oportunidad)
        {
            var date = DateTime.Now;
            var fecha = date.ToShortDateString();
            var hora = date.ToShortTimeString();

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration.GetSection("MailKit:From").Value));
            message.To.Add(MailboxAddress.Parse(_configuration.GetSection("MailKit:To").Value));
            message.Subject = "Lead oportunidad incorrecto " + fecha + " " + hora + "!!!";


            var titulo = "<h2>El señor(a): " + persona.nombres + " " + persona.apellidos + @"</h2>" +
                    "<h3>Desea comunicarse contigo para cotizar un activo</h3>";
            var datosContacto = "<table style='margin:auto; width: 600px; padding-bottom: 22px;'>" +
                    "<caption style='text-align:center; background:#343a40; color:#fff; border-radius:5px 5px 0 0'> Informacion:</caption>" +
                    "<tbody>" +
                    "<tr><td style='background:#65C8D0; padding:6px 12px 6px;'>Fecha de Ejecutoria</td><td style='background:#f6f6f6; padding:6px 12px 6px; border-bottom:1px solid #65C8D0;'>" + oportunidad.fechaEjecutoria + "</td></tr>" +
                    "<tr><td style='background:#65C8D0; padding:6px 12px 6px;'>Numero Radicado</td><td style='background:#f6f6f6; padding:6px 12px 6px; border-bottom:1px solid #65C8D0;'>" + oportunidad.numeroRadicado + "</td></tr>" +
                    "<tr><td style='background:#65C8D0; padding:6px 12px 6px;'>Cuenta de cobro</td><td style='background:#f6f6f6; padding:6px 12px 6px; border-bottom:1px solid #65C8D0;'>" + oportunidad.cuentaCobro + "</td></tr>" +
                    "<tr><td style='background:#65C8D0; padding:6px 12px 6px;'>Demandante</td><td style='background:#f6f6f6; padding:6px 12px 6px; border-bottom:1px solid #65C8D0;'>" + oportunidad.demandante + "</td></tr>" +
                    "</tbody>" +
                    "</table>";

            var bodybuilder = new BodyBuilder
            {
                HtmlBody = titulo + datosContacto
            };
            message.Body = bodybuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("MailKit:Smtp").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("MailKit:From").Value, _configuration.GetSection("MailKit:Password").Value);
            smtp.Send(message);
            smtp.Disconnect(true);
        }

        public void SendSuccess(PersonaDTO persona, OportunidadDTO oportunidad)
        {
            var date = DateTime.Now;
            var fecha = date.ToShortDateString();
            var hora = date.ToShortTimeString();
           
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration.GetSection("MailKit:From").Value));
            message.To.Add(MailboxAddress.Parse(_configuration.GetSection("MailKit:To").Value));
            message.Subject = "Lead oportunidad correcto " + fecha + " " + hora + "!!!";

            var titulo = "<h2>El señor(a): " + persona.nombres + " " + persona.apellidos + @"</h2>" +
                    "<h3>Acaba de solicitar cotizar un activo de manera exitosa desde nuestra Landing page WebConfival</h3>";
            var datosContacto = "<p>Numero de la oportunidad de ValuezBPM: " + oportunidad.id.ToString() + "</p>" +
                "<a href='https://guivaluezbpm.azurewebsites.net' target='_blank'><p>https://guivaluezbpm.azurewebsites.net</p></a>";
            var bodybuilder = new BodyBuilder
            {
                HtmlBody = titulo + datosContacto
            };
            message.Body = bodybuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("MailKit:Smtp").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("MailKit:From").Value, _configuration.GetSection("MailKit:Password").Value);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
