using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Add the below
using BabySiteServerBL.Models;
using System.IO;

namespace BabySiteServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BabySiteController : ControllerBase
    {
        [Route("BabySiteAPI")]
        [ApiController]
        public class ContactsController : ControllerBase
        {
            #region Add connection to the db context using dependency injection
            BabySiteDBContext context;
            public ContactsController(BabySiteDBContext context)
            {
                this.context = context;
            }
            #endregion
        }
    }
}
