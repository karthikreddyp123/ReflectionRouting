using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBRouting.Models
{
    public class ControllerList
    {
        public int ControllerId { get; set; }
        public List<SelectListItem> Controller { get; set; }
        
    }
}