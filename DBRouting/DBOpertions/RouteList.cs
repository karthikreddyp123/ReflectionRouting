using DBRouting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using RouteTable = DBRouting.Models.RouteTable;

namespace DBRouting.DBOpertions
{
    public class RouteList
    {
        private static readonly DBRouteEntities DbContextEntities=new DBRouteEntities();
        public static void AddRoutes(List<string> routeList)
        {
            
            foreach (var routeString in routeList)
            {
                var check = DbContextEntities.RouteTables.SingleOrDefault(m => m.Route.Equals(routeString));
                if (check == null)
                {
                    DbContextEntities.RouteTables.Add(new RouteTable
                    {
                        Route = routeString
                    });
                }
            }

            DbContextEntities.SaveChanges();
        }
    }
}