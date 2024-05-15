

using AuthAspRazorPages.Permissions;

namespace AuthAspRazorPages.PermissionsControl
{
    public class ManagmentSystemExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                      "Articles",new List<PermissionDto>
                      {

                         new PermissionDto(FunctionPermmisionsCode.ArticleList,"ArticleList"),
                         new PermissionDto(FunctionPermmisionsCode.AddNewArticle,"AddNewArticle"),
                         new PermissionDto(FunctionPermmisionsCode.EditArticle,"EditArticle"),
                         new PermissionDto(FunctionPermmisionsCode.RemoveArticle,"RemoveArticle"),
                      }
                },
                {
                      "Books" , new List<PermissionDto>
                      {
                         new PermissionDto(FunctionPermmisionsCode.BookList,"BookList"),
                         new PermissionDto(FunctionPermmisionsCode.AddNewBook,"AddNewBook"),
                         new PermissionDto(FunctionPermmisionsCode.EditBook,"EditBook"),
                         new PermissionDto(FunctionPermmisionsCode.RemoveBook,"RemoveBook"),
                      }
                }, 
             

            };
        }
    }
}
