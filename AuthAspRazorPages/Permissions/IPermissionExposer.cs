using System.Collections.Generic;

namespace AuthAspRazorPages.Permissions
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}