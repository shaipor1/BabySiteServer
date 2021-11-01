using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class BabySiteDBContext : DbContext
    {
        public User Login(string email, string pswd)
        {
            User user = this.Users.Where(u => u.Email == email && u.UserPswd == pswd).FirstOrDefault();

            return user;
        }

        public void AddUser(User u)
        {
            try
            {
                this.Users.Add(u);
            }
            catch(Exception e)
            {
                throw new Exception("unable to insert user",e);
            }

        }
        public void AddBabySitter(BabySitter b)
        {
            try
            {
                this.BabySitters.Add(b);
            }
            catch (Exception e)
            {
                throw new Exception("unable to insert user", e);
            }

        }
        public void AddParent(Parent p)
        {
            try
            {
                this.Parents.Add(p);
            }
            catch (Exception e)
            {
                throw new Exception("unable to insert user", e);
            }

        }
    }
}
