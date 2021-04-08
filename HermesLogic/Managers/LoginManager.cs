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
            using (var db = new HermesDB())
            {
                return db.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.Password.ToLower() == password.ToLower());
            }
        }

        public void Register(string username, string email, string password)
        {
            using (var db = new HermesDB())
            {
                //Check if there isn't a user with the same username already
                var existingUser = db.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
                if (existingUser != null)
                {
                    throw new LogicException("That username is already in use!");
                }
                //Check if there isn't a user with the same e-mail already
                existingUser = db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
                if (existingUser != null)
                {
                    throw new LogicException("That e-mail is already in use!");
                }
                //Check if password is at least 8 symbols
                if (!password.IsPasswordOk())
                {
                    throw new LogicException("Password should be at least 8 symbols!");
                }

                //Add user if all OK
                db.Users.Add(new Users()
                {
                    Email = email,
                    Password = password,
                    Username = username,
                });
                db.SaveChanges();
            }
        }
    }
}
