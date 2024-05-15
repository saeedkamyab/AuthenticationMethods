using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Models.RoleAndPermission
{
    public class Permission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  set; }
        public int Code { get;  set; }
        public string Name { get;  set; }




        public int RoleId { get;  set; }
        public Role Role { get;  set; }



        public Permission(int code)
        {
            Code = code;
        }

        //public Permission(int code,string name)
        //{
        //    Code = code;
        //    Name = name;
        //}
    }
}
