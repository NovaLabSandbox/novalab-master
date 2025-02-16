using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

using OneOf;

using Novalab.Master.Core.Results;

namespace Novalab.Master.Core.Http
{
    public class CoreFunction
    {
        public ActionResult Do<T>(OneOf<CreatedResult<T>, ForbiddenResult, Results.NotFoundResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapCreatedResult, MapForbiddenResult, MapNotFoundResult, MapBusinessError);
        }

        public ActionResult Do<T>(OneOf<T, ForbiddenResult, Results.NotFoundResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapOkResult, MapForbiddenResult, MapNotFoundResult, MapBusinessError);
        }

        public ActionResult Do<T>(OneOf<CreatedResult<T>, Results.ConflictResult, ForbiddenResult, Results.NotFoundResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapCreatedResult, MapConflictResult, MapForbiddenResult, MapNotFoundResult, MapBusinessError);
        }

        public ActionResult Do<T>(OneOf<T, Results.ConflictResult, ForbiddenResult, Results.NotFoundResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapOkResult, MapConflictResult, MapForbiddenResult, MapNotFoundResult, MapBusinessError);
        }

        private ActionResult MapOkResult<T>(T result)
        {
            return new OkObjectResult(result);
        }

        private ActionResult MapCreatedResult<T>(CreatedResult<T> result)
        {
            return new CreatedResult($"{result.EntityPath}/{result.Id}", result.Data);
        }

        private ActionResult MapForbiddenResult(ForbiddenResult result)
        {
            return new ForbidResult();
        }

        private ActionResult MapNotFoundResult(Results.NotFoundResult result)
        {
            return new NotFoundObjectResult(JsonSerializer.Serialize(result));
        }

        private ActionResult MapBusinessError(BusinessErrorResult result)
        {
            return new BadRequestObjectResult(new ProblemDetails() { Detail = result.Error, Status = 400 });
        }

        private ActionResult MapConflictResult(Results.ConflictResult result)
        {
            return new ConflictObjectResult(result);
        }
    }
}
