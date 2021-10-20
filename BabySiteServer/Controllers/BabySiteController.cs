using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabySiteServerBL.Models;
using System.IO;

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

        //set the contact default photo image name
        public const string DEFAULT_PHOTO = "defaultphoto.jpg";

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

    }
}
