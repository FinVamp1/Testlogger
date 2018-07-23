
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using TestIogger.Common;

namespace TestIogger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext finCtx)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            LogArguments testArg = new LogArguments(log, LoggerApp.ThisApp, finCtx.InvocationId.ToString(), finCtx.FunctionName.ToString());

            FinLogger.Log("Test Error", LoggerLevel.Error, testArg);
            FinLogger.Log("Test Information ", LoggerLevel.Info, testArg);
            FinLogger.Log("Test Success", LoggerLevel.Success, testArg);
            FinLogger.Log("Test Trace", LoggerLevel.Trace, testArg);

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
