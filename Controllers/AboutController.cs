using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;
using System.Net.Mail;
using System.Configuration;

using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace noa.Controllers;

public class AboutController : Controller
{

    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger)
    {
        _logger = logger;
    }

    [Route("/about")]
    public async Task<ActionResult> Insdex()
    {
        MailMessage correo = new MailMessage();
        correo.From = new MailAddress("edgar.martinez@confival.com", "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
        correo.To.Add("automatizacion@confival.com"); //Correo destino?
        correo.Subject = "Correo de prueba"; //Asunto
        correo.Body = "Este es un correo de prueba desde c#"; //Mensaje del correo
        correo.IsBodyHtml = true;
        correo.Priority = MailPriority.Normal;
        SmtpClient smtp = new SmtpClient();
        smtp.UseDefaultCredentials = true;
        smtp.Host = "smtp.office365.com"; //Host del servidor de correo
        smtp.Port = 587; //Puerto de salida
        smtp.Credentials = new System.Net.NetworkCredential("edgar.martinez@confival.com", "A1B2C3D4a1b2c3d4#$2023");//Cuenta de correo
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        smtp.EnableSsl = true;//True si el servidor de correo permite ssl
        smtp.Send(correo);
        return Ok("ss");
    }
}