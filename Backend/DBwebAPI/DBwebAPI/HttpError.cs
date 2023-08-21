using Microsoft.AspNetCore.Mvc;

namespace DBwebAPI
{
    public class ServiceUnavailable : ActionResult
    {
        private readonly object errorMessage;

        public ServiceUnavailable(object errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public override void ExecuteResult(ActionContext context)
        {
            var objectResult = new ObjectResult(new { ErrorMessage = errorMessage })
            {
                StatusCode = 503
            };

            objectResult.ExecuteResult(context);
        }
    }
    public class InternalError: ActionResult
    {
        private readonly object errorMessage;
        public InternalError(object errorMessage)
        {
            this.errorMessage = errorMessage;
        }
        public override void ExecuteResult(ActionContext context)
        {
            var objectResult = new ObjectResult(new { ErrorMessage = errorMessage })
            {
                StatusCode = 500
            };

            objectResult.ExecuteResult(context);
        }
    }
}
