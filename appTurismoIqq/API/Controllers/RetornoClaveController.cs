

namespace API.Controllers
{
    using appTurismoIqq.Helpers;
    using appTurismoIqq.Modelo;
    using MailKit.Net.Smtp;
    using MimeKit;
    using MongoDB.Driver;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
   
    using System.Security.Authentication;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Http.Description;
    [RoutePrefix("api/RetornoClave")]
    public class RetornoClaveController : ApiController
    {
        [HttpPost]
     
        [Route ("enviarClave")]
        public async Task <IHttpActionResult> enviarClave( JObject objeto)
        {
            
     

            try
            {
                var email = string.Empty;
                dynamic recibir = objeto;
                email = recibir.Email.Value;

                string connectionString =
  @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                  new MongoUrl(connectionString)
                );
                settings.SslSettings =
                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);

                var db = mongoClient.GetDatabase("bdTurismo");
                var colection = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                var lista = db.GetCollection<Usuario>("Usuario").AsQueryable<Usuario>().Where(U => U.email.Equals(email)).SingleOrDefault();
                var pass = lista.passwordU;

                var subject = "Retorno de clave";
                    //asunto="Retorno de Contraseña";
                var body= string.Format(@"<h1>App Turismo Iquique</h1>
                                          //  <p>Tu clave es: <strong>{0}</strong></p>", pass);

          

                var from = "mirealponce1407@gmail.com";
                var smtp = "smtp.gmail.com";
                var port = "587";
                var password = "Mireal422090";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(smtp, int.Parse(port), false);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);

                }
                return BadRequest("mensaje enviado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
