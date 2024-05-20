using AuthApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace AuthApi.Common.Permissions
{
    public class SecurityControllerFilter : IActionFilter
    {

        private readonly IAuthHelper _authHelper;

        public SecurityControllerFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            //  throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionPermission = (NeedsPermissionAttribute)context.ActionDescriptor.EndpointMetadata.SingleOrDefault(meta =>
            meta.GetType() == typeof(NeedsPermissionAttribute));


            if (actionPermission == null) return;

            var accountPermissions = _authHelper.GetPermissions();

            if (accountPermissions.All(x => x != actionPermission.Permission))
                context.Result = new StatusCodeResult(403);
            //context.HttpContext.Response.Redirect("/AccessDenied");
        }


    }
}
