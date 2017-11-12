using HRApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    /*
     * This class is pretty much for convenience. The only functionality it provides is to return an ApplicationUser by his/her ID
     */
    public class UserBase
    {
        private readonly ApplicationDbContext _db;

        public UserBase(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetUserById(string id)
        {
            return _db.Users.Find(Convert.ToInt64(id));
        }
    }
}
