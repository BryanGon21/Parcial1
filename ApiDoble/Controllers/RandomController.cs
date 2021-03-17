namespace ApiDoble.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Random random)
        {
            string connectionString = "Endpoint=sb://queueimpar.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=E0taZrbR5+FvaHyCrmlncvUIyGyx+Zb+EoU91OYoFFY=;EntityPath=colaimpar";
            string queueName = "colaImpar";
            string mensaje = JsonConvert.SerializeObject(random);

            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }


            return true;
        }
    }
}
