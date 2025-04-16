using Microsoft.AspNetCore.Identity;
namespace KetNoiDB.Models.User
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
