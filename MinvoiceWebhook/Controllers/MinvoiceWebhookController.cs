using Microsoft.AspNetCore.Mvc;
using MinvoiceWebhook.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MinvoiceWebhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinvoiceWebhookController : ControllerBase
    {
        
        private readonly IRabbitMQConnection _rabbitMQConnection;

        public MinvoiceWebhookController(IRabbitMQConnection rabbitMQConnection)
        {
            _rabbitMQConnection = rabbitMQConnection;
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            var queuename = "A";
            using (var connection = _rabbitMQConnection.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                var jsonMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);
                channel.BasicPublish(exchange: string.Empty,
                                 routingKey: queuename,
                                 basicProperties: null,
                                 body: body);

                return Ok("Sent to Queue");
            }
        }
    }
}
