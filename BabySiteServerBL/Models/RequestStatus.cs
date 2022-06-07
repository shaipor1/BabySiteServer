using System;
using System.Collections.Generic;

#nullable disable

namespace BabySiteServerBL.Models
{
    public partial class fff
    {
        public fff()
        {
            Requests = new HashSet<Request>();
        }

        public int RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
