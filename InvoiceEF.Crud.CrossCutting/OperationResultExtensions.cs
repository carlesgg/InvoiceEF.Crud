using Microsoft.AspNetCore.Mvc;

namespace InvoiceEF.Crud.CrossCutting
{
    public static class OperationResultExtensions
    {
        public static IActionResult ToAction<T>(this OperationResult<T> result,
                                                HttpVerb httpVerb) =>
            result.HasErrors
            ? result.ToObjectResultByVerb(httpVerb)
            : new ObjectResult(
                    new ProblemDetails()
                    {
                        Status = result.Errors.First().Code,
                        Title = "An error ocurred",
                        Detail = result.Errors.First().Message
                        //Extensions = { ["errors"] = result.Errors }
                    });

        private static ObjectResult ToObjectResultByVerb<T>(this OperationResult<T> result, HttpVerb httpVerb)
        {
            //Will implement future HTTP verbs as needed
            return httpVerb switch
            {
                HttpVerb.POST => new CreatedResult("", result.Result),
                _ => new OkObjectResult(result.Result),
            };
        }
    }
}
