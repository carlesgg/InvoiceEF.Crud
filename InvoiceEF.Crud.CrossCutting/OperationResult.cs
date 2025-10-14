namespace InvoiceEF.Crud.CrossCutting
{
    public class OperationResult<T>
    {
        public T? Result { get; set; }

        public List<Error> Errors { get; set; } = [];

        public Exception? Exception { get; set; }

        //Mejor HasErrors
        public bool IsSuccess => !Errors.Any();
    }

    public record Error(int Code, string Message = "");

    public static class OperationResultExtension
    {
        public static OperationResult<T> AddResult<T>(this OperationResult<T> operationResult, T value)
        {
            operationResult.Result = value;
            return operationResult;
        }

        public static OperationResult<T> AddError<T>(this OperationResult<T> operationResult, Error error)
        {
            operationResult.Errors.Add(error);
            return operationResult;
        }

        public static OperationResult<T> AddError<T>(this OperationResult<T> operationResult, int code, string message)
        {
            operationResult.Errors.Add(new Error(code, message));
            return operationResult;
        }

        public static OperationResult<T> AddErrors<T>(this OperationResult<T> operationResult, List<Error> errorList)
        {
            operationResult.Errors.AddRange(errorList);
            return operationResult;
        }

        public static OperationResult<T> AddException<T>(this OperationResult<T> operationResult, Exception exception)
        {
            operationResult.Exception = exception;
            return operationResult;
        }
    }
}
