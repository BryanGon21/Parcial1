using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fncImpar
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task RunAsync([ServiceBusTrigger("colaimpar", Connection = "MyConn")] string myQueueItem,
        [CosmosDB(
            databaseName:"dbimpar",
            collectionName:"impar",
            ConnectionStringSetting ="strCosmos"
            )]IAsyncCollector<object> datos,

            ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var random = JsonConvert.DeserializeObject<Random>(myQueueItem);
                await datos.AddAsync(random);
            }
            catch (Exception ex)

            {
                log.LogError($"No fue posible insertar datos: {ex.Message}");


            }
        }
    }
}
