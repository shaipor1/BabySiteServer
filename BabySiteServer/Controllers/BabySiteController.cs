using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabySiteServerBL.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace BabySiteServer.Controllers
{
    [Route("BabySiteAPI")]
    [ApiController]
    public class BabySiteController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        BabySiteDBContext context;
        public BabySiteController(BabySiteDBContext context)
        {
            this.context = context;
        }
        #endregion
        [Route("Test")]
        [HttpGet]
        public String Hello()
        {
            return "hello me";
        }

        //set the contact default photo image naame
        public const string DEFAULT_PHOTO = "defaultphoto.png";
        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("LogOut")]
        [HttpGet]
        public bool LogOut()
        {
            if (HttpContext.Session.GetObject<User>("theUser") != null)
            {
                HttpContext.Session.Clear();
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            return false;
        }
        [Route("Login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return user;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("SignUpBabySitter")]
        [HttpPost]
        public BabySitter SignUpBabySitter([FromBody] BabySitter b)
        {
            //Check user name and password
            if (b != null)
            {
                this.context.AddBabySitter(b);
                HttpContext.Session.SetObject("theUser", b.User);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return b;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }

        }
        [Route ("PostMessage")]
        [HttpPost]
        public bool PostMessage([FromBody] Massage m)
        {
            if (m != null)
            {
                this.context.AddMessage(m);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return true;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("PostReview")]
        [HttpPost]
        public int PostReview([FromBody] Review r)
        {
            if (r != null)
            {
                int newAvg=context.AddReview(r);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return newAvg;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return -1;
            }
        }

        [Route("SignUpParent")]
        [HttpPost]
        public Parent SignUpParent([FromBody] Parent p)
        {
            //Check user name and password
            if (p != null)
            {
                this.context.AddParent(p);
                HttpContext.Session.SetObject("theUser", p.User);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return p;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
            
        }
        #region IsEmailExist
        [Route("IsEmailExist")]
        [HttpGet]
        public bool IsEmailExist([FromQuery] string email)
        {
            bool isExist = this.context.EmailExist(email);
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return isExist;
        }
        #endregion

        #region IsUserNameExist
        [Route("IsUserNameExist")]
        [HttpGet]
        public bool IsUserNameExist([FromQuery] string userName)
        {
            bool isExist = this.context.UserNameExist(userName);
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return isExist;
        }
        #endregion

        #region UpdateParent
        [Route("UpdateParent")]
        [HttpPost]
        public Parent UpdateUser([FromBody] Parent parent)
        {
            //If user is null the request is bad
            if (parent == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            User currentUser = HttpContext.Session.GetObject<User>("theUser");
            
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.UserId == parent.User?.UserId)
            {
                Parent updatedParent = context.UpdateParent(parent);

                if (updatedParent == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedParent;

                ////Now check if an image exist for the contact (photo). If not, set the default image!
                //var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", DEFAULT_PHOTO);
                //var targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{user.Id}.jpg");
                //System.IO.File.Copy(sourcePath, targetPath);

                //return the contact with its new ID if that was a new contact
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        #endregion
        #region UpdateBabySitter
        [Route("UpdateBabySitter")]
        [HttpPost]
        public BabySitter UpdateUser([FromBody] BabySitter babySitter)
        {
            //If user is null the request is bad
            if (babySitter == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            User currentUser = HttpContext.Session.GetObject<User>("theUser");

            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.UserId == babySitter.User?.UserId)
            {
                BabySitter updatedBabySitter = context.UpdateBabySitter(babySitter);

                if (updatedBabySitter == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedBabySitter;

                ////Now check if an image exist for the contact (photo). If not, set the default image!
                //var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", DEFAULT_PHOTO);
                //var targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{user.Id}.jpg");
                //System.IO.File.Copy(sourcePath, targetPath);

                //return the contact with its new ID if that was a new contact
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        #endregion
        #region GetBabySitters
        [Route("GetBabySitters")]
        [HttpGet]
        public List<BabySitter> GetBabySitters()
        {
            

            User currentUser = HttpContext.Session.GetObject<User>("theUser");

            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null)
            {
                List<BabySitter> babySitters = context.BabySitters.Include(b => b.User).ToList();
                return babySitters;

                
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        #endregion
        #region GetJobOffers
        [Route("GetJobOffers")]
        [HttpGet]
        public List<Massage> GetJobOffers()
        {


            User currentUser = HttpContext.Session.GetObject<User>("theUser");

            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null)
            {
                List<Massage> jobOffers = context.Massages.Where(m => m.MassageTypeId == 1 && m.UserId==currentUser.UserId).ToList();
                return jobOffers;


            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        #endregion


    }
}
