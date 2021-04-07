using HermesLogic.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HermesLogic.Managers
{
   public class LoginManager
    {
        public Users GetUser(string username, string password)
        {
            using (var db = new HermesDatabase())
            {
                return db.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.Password.ToLower() == password.ToLower());
            }
        }
    }
}
