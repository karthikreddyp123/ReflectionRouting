using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DBRouting.Models;
namespace DBRouting.RouteTableInfo
{
    public class RouteTableGen
    {
        public static List<string> GenerateRouteTable()
        {
            var asm = Assembly.GetAssembly(typeof(MvcApplication));
            var routeList=new List<string>();
            var temp = asm.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && method.ReturnType == typeof(ActionResult)).ToList();
            temp.Count();
            hg
            foreach (var item in temp)
            {
                StringBuilder url = new StringBuilder("/" + item.DeclaringType.Name.Substring(0,item.DeclaringType.Name.Length-10) + "/" + item.Name);
                var parameters = item.GetParameters();
                if (parameters.Length > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        url.Append("/" + parameter.Name);
                    }
                    
                }
                routeList.Add(url.ToString());
            }

            return routeList;
        }
    }
}