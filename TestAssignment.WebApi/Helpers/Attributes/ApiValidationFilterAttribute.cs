using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.WebApi.Helpers.Attributes
{
    public class ApiValidationFilterAttribute : ActionFilterAttribute
    {
        #region Overrides of ActionFilterAttribute

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Select(s =>
                        new KeyValuePair<string, string>(s.Key,
                            string.Join(", ", s.Value.Errors.Select(d => d.ErrorMessage))))
                    .ToList();
                var result = ResultModel<object>.InvalidRequest(info: new Dictionary<string, string>(errors));

                context.Result = new ObjectResult(result)
                {
                    StatusCode = result.ErrorCode.ToStatusCode()
                };
                context.ModelState.Clear();
            }

            base.OnResultExecuting(context);
        }

        #endregion
    }
}