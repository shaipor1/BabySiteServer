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
            User user = this.Users.Where(u => u.Email == email && u.UserPswd == pswd).Include(u => u.Parents).Include(u => u.BabySitters).Include(u => u.Massages).FirstOrDefault();

            return user;
        }
        public void AddMessage(Massage m)
        {
            try
            {
                this.Massages.Add(m);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("unable to post message", e);
            }
        }
        public void AddUser(User u)
        {
            try
            {
                this.Users.Add(u);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("unable to insert user", e);
            }

        }
        public void AddBabySitter(BabySitter b)
        {
            try
            {
                this.BabySitters.Add(b);
                this.SaveChanges();
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
                this.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("unable to insert user", e);
            }

        }
        #region EmailExist
        public bool EmailExist(string email)
        {
            try
            {
                return this.Users.Any(u => u.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return true;
            }
        }
        #endregion

        #region UserNameExist
        public bool UserNameExist(string userName)
        {
            try
            {
                return this.Users.Any(u => u.UserName == userName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return true;
            }
        }
        #endregion
        #region UpdateParent
        public Parent UpdateParent(Parent updatedParent)
        {
            try
            {
                //Parent currentParent = this.Parents
                //.Where(p => p.UserId == p.UserId).FirstOrDefault();
                //User newUser = new User
                //{
                //    FirstName = updatedParent.User.FirstName,
                //    LastName = updatedParent.User.LastName,
                //    UserPswd = updatedParent.User.UserPswd,
                //    BirthDate = updatedParent.User.BirthDate,
                //    PhoneNumber = updatedParent.User.PhoneNumber,
                //    City = updatedParent.User.City,
                //    Street = updatedParent.User.Street,
                //    House = updatedParent.User.House,
                //    UserTypeId=updatedParent.User.UserTypeId,
                //    UserName=updatedParent.User.UserName,
                //    Email=updatedParent.User.Email,
                //    Gender=updatedParent.User.Gender,
                //    UserId=updatedParent.User.UserId
                //};


                //currentParent.User = updatedParent.User;
                //currentParent.ChildrenCount = updatedParent.ChildrenCount;
                //currentParent.ChildrenMaxAge = updatedParent.ChildrenMaxAge;
                //currentParent.ChildrenMinAge = updatedParent.ChildrenMinAge;
                //currentParent.HasDog = updatedParent.HasDog;
                
                updatedParent.User.UserTypeId = 1;
                this.Entry(updatedParent).State = EntityState.Modified;
                this.Entry(updatedParent.User).State = EntityState.Modified;
                

                this.SaveChanges();
                return updatedParent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion
        #region Update  BabySitter
        public BabySitter UpdateBabySitter(BabySitter updatedBabySitter)
        {
            try
            {
                //Parent currentParent = this.Parents
                //.Where(p => p.UserId == p.UserId).FirstOrDefault();
                //User newUser = new User
                //{
                //    FirstName = updatedParent.User.FirstName,
                //    LastName = updatedParent.User.LastName,
                //    UserPswd = updatedParent.User.UserPswd,
                //    BirthDate = updatedParent.User.BirthDate,
                //    PhoneNumber = updatedParent.User.PhoneNumber,
                //    City = updatedParent.User.City,
                //    Street = updatedParent.User.Street,
                //    House = updatedParent.User.House,
                //    UserTypeId=updatedParent.User.UserTypeId,
                //    UserName=updatedParent.User.UserName,
                //    Email=updatedParent.User.Email,
                //    Gender=updatedParent.User.Gender,
                //    UserId=updatedParent.User.UserId
                //};


                //currentParent.User = updatedParent.User;
                //currentParent.ChildrenCount = updatedParent.ChildrenCount;
                //currentParent.ChildrenMaxAge = updatedParent.ChildrenMaxAge;
                //currentParent.ChildrenMinAge = updatedParent.ChildrenMinAge;
                //currentParent.HasDog = updatedParent.HasDog;

                updatedBabySitter.User.UserTypeId = 1;
                this.Entry(updatedBabySitter).State = EntityState.Modified;
                this.Entry(updatedBabySitter.User).State = EntityState.Modified;


                this.SaveChanges();
                return updatedBabySitter;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

    }
}
