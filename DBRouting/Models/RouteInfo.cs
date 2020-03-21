using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBRouting.Models
{
    public class RouteInfo
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public RouteInfo(string controllerName,string actionName)
        {
            ControllerName = controllerName;
            ActionName = actionName;
        }
    }
}