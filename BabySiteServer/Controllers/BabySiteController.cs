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
    }
}
