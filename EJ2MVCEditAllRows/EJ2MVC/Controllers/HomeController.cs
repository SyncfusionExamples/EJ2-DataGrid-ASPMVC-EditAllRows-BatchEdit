using EJ2MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EJ2MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            var order = OrdersDetails.GetAllRecords();
            ViewBag.datasource = order;
            return View();
        }


        public ActionResult UrlDatasource(Data dm)
        {
            var order = OrdersDetails.GetAllRecords();
            var Data = order.ToList();
            int count = order.Count();
            return dm.requiresCounts ? Json(new { result = Data.Skip(dm.skip).Take(dm.take), count = count }) : Json(Data);
        }


        public ActionResult Update(OrdersDetails value)
        {
            var ord = value;
            OrdersDetails val = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.EmployeeID = ord.EmployeeID;
            val.CustomerID = ord.CustomerID;
            string msg = "Successfully performed editing the record";
            return Json(new { data = value, message = msg });
        }
        //insert the record
        public ActionResult Insert(OrdersDetails value)
        {
            var ord = value;
            var add = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            if (add == null)
            {
                OrdersDetails.GetAllRecords().Insert(0, value);
                string msg = "Successfully performed inserting a record";
                return Json(new { data = value, message = msg });
            }
            else
            {
                string msg = "duplicates acnnot be inserted";
                return Json(new { data = value, message = msg });
            }
        }
        //Delete the record
        public ActionResult Delete(OrdersDetails value)
        {
            OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(or => or.OrderID == value.OrderID).FirstOrDefault());
            string msg = "Successfully performed deleting a record";
            return Json(new { data = value, message = msg });
        }

        public ActionResult getStatus(OrdersDetails value) {

            var ord = value;
            var add = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            if (add != null)
            {
                return Content("false duplicates acnnot be inserted");
            }            
            return Content("true");
        }

        public class Data
        {

            public bool requiresCounts { get; set; }
            public int skip { get; set; }
            public int take { get; set; }
            public int value { get; set; }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}