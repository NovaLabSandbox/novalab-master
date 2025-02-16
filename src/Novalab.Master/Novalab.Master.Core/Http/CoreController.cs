using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

using OneOf;

using Novalab.Master.Core.Results;

namespace Novalab.Master.Core.Http
{
    public class CoreController : ControllerBase
    {
        public string GetUserId
        {
            get
            {
                var claimAvailable = User.Claims.FirstOrDefault(f => f.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claimAvailable != null)
                {
                    return claimAvailable.Value;
                }

                throw new Exception("Claim missing cannot process.");
            }
        }

        public ActionResult Do<T>(OneOf<CreatedResult<T>, Results.NotFoundResult, Results.ForbiddenResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapCreatedResult, MapNotFoundResult, MapForbiddenResult, MapBusinessError);
        }

        public ActionResult Do<T>(OneOf<CreatedResult<T>, ForbiddenResult, Results.NotFoundResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapCreatedResult, MapForbiddenResult, MapNotFoundResult, MapBusinessError);
        }

        public ActionResult Do<T>(OneOf<T, Results.NotFoundResult, ForbiddenResult, BusinessErrorResult> oneOf)
        {
            return oneOf.Match(MapOkResult, MapNotFoundResult, MapForbiddenResult, MapBusinessError);
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
            return Ok(result);
        }

        private ActionResult MapCreatedResult<T>(CreatedResult<T> result)
        {
            return Created($"{result.EntityPath}/{result.Id}", result.Data);
        }

        private ActionResult MapForbiddenResult(ForbiddenResult result)
        {
            return Forbid();
        }

        private ActionResult MapNotFoundResult(Results.NotFoundResult result)
        {
            return NotFound(JsonSerializer.Serialize(result));
        }

        private ActionResult MapBusinessError(BusinessErrorResult result)
        {
            return BadRequest(new ProblemDetails() { Detail = result.Error, Status = 400 });
        }

        private ActionResult MapConflictResult(Results.ConflictResult result)
        {
            return Conflict(result);
        }
    }
}
