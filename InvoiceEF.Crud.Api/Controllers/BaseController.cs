using FluentValidation;
using InvoiceEF.Crud.CrossCutting;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceEF.Crud.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ValidateAndExecute<TDto, TResult>(
            TDto dto,
            IValidator<TDto> validator,
            Func<Task<OperationResult<TResult>>> action,
            CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }

            var result = await action();

            if (result.HasErrors)
                return BadRequest(result.Errors);

            return Ok(result.Result);
        }
    }
}
