using MvcTest.AuthFilter;
using MvcTest.Models.Context;
using MvcTest.Models.Models;
using MvcTest.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTest.Controllers
{

    public class HomeController : Controller
    {

        IHomeInterface homeInterface;

        public HomeController(IHomeInterface homeInterface)
        {
            this.homeInterface = homeInterface;
        }
        public ActionResult CreateOrder()
        {
            ViewBag.ItemList = homeInterface.GetItemList();
            return View();
        }

        public ActionResult RetriveOrders()
        {
            ViewBag.Message = "Your application description page.";
            int UserId = Convert.ToInt32(Session["UserId"]);
            return View(homeInterface.GetOrdersList(UserId));
        }

        public ActionResult GetItemById(int ItemId)
        {
            return Json(homeInterface.GetItemById(ItemId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCoupenByCode(string Code)
        {
            return Json(homeInterface.GetCoupenByCode(Code), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateNewOrder(List<ItemDetailModel> itemDetails, OrdersModel ordersModel)
        {
            try
            {
                ordersModel.UserId = (int)Session["UserId"];
                homeInterface.CreateOrder(itemDetails, ordersModel);
                return Json("Order Placed Successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ItemDetail(int OrderId)
        {
            try
            {
                return View(homeInterface.GetItemList(OrderId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}