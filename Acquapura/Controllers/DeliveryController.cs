using Acquapura.Business;
using Acquapura.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Acquapura.Controllers
{
    public class DeliveryController : Controller
	{
		string ErrorMessage = string.Empty;
		string SuccessMessage = string.Empty;
		private clsDelivery ObjClsDeliveries = new clsDelivery();
		private Acquapura_dbEntities db = new Acquapura_dbEntities();

		// GET: Deliveries
		public ActionResult Index(long id)
		{
			ViewBag.SuccessMessage = TempData["SuccessMessage"];
			ViewBag.ErrorMessage = TempData["ErrorMessage"];

			TempData["CustomerId"] = id;

			return View();
		}
		public ActionResult List()
		{
			var cusId = Convert.ToInt64(TempData["CustomerId"]);

			var deliveries = ObjClsDeliveries.GetAllDeliveriesByCustomerId(cusId);
			return PartialView(deliveries);
		}

		// GET: Deliveries/Details/5
		public ActionResult Details(long? id)
		{

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Delivery delivery = ObjClsDeliveries.GetDeliveryByID((Int16)id);
			if (delivery == null)
			{
				return HttpNotFound();
			}
			return View(delivery);
		}

		// GET: Deliveries/Create
		public ActionResult Create(long id)
		{
			ViewBag.CustomerID = id;
			return View();
		}

		// POST: Deliveries/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Delivery delivery)
		{
			if (ModelState.IsValid)
			{
				if (ObjClsDeliveries.CreateDelivery(delivery))
				{
					TempData["SuccessMessage"] = "Congrats ! Delivery Added Successfully ...";
					return RedirectToAction("Index","Customers");
				}
				else
				{
					TempData["ErrorMessage"] = "Oops ! something went wrong please try later ...";
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Please fill the form properly ...";
			}

			return RedirectToAction("Index");
		}

		// GET: Deliveries/Edit/5
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Delivery delivery = ObjClsDeliveries.GetDeliveryByID((Int16)id);
			if (delivery == null)
			{
				return HttpNotFound();
			}

			TempData["DeliveryID"] = id;

			return View(delivery);
		}

		// POST: Deliveries/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Delivery delivery)
		{
			if (ModelState.IsValid)
			{
				Int16 Id = Convert.ToInt16(TempData["DeliveryID"].ToString());

				if (delivery.ID == Id)
				{

					if (ObjClsDeliveries.UpdateDelivery(delivery))
					{
						TempData["SuccessMessage"] = "Congrats ! Delivery Updated Successfully ...";
						return RedirectToAction("Index");
					}
					else
					{
						TempData["ErrorMessage"] = "Oops ! something went wrong please try later ...";
					}
				}
				else
				{
					TempData["ErrorMessage"] = "Oops ! something went wrong please try later ...";
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Please fill the form properly ...";
			}
			return RedirectToAction("Index");
		}

		// GET: Deliveries/Delete/5
		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}


			Delivery delivery = ObjClsDeliveries.GetDeliveryByID((Int16)id);
			if (delivery == null)
			{
				return HttpNotFound();
			}
			TempData["DeliveryID"] = id;
			return View(delivery);
		}

		// POST: Deliveries/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			Int16 Id = Convert.ToInt16(TempData["DeliveryID"].ToString());
			if (id == Id)
			{
				if (ObjClsDeliveries.DeleteDelivery((Int16)id))
				{
					TempData["SuccessMessage"] = "Delivery Deleted Successfully ...";
					return RedirectPermanent("/Deliveries/Index");
				}
				else
				{
					TempData["ErrorMessage"] = "Oops ! something went wrong please try later ...";
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Oops ! something went wrong please try later ...";
			}

			return RedirectPermanent("/Deliveries/Index");

		}

		public ActionResult Record(DateTime startDate, DateTime endDate)
		{
			var deliveries = db.Deliveries.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();


			var cashReceived = deliveries.Select(x => x.CashReceived).Sum();
			var cashBalance = deliveries.Select(x => x.CashBalance).Sum();
			var balance = cashBalance - cashReceived;

			var data = new
			{
				TotalCan = deliveries.Select(x => x.Delivered).Count(),
				CashReceived = cashReceived,
				CashBalance = cashBalance,
				Balance = balance
			};


			return Json(new { success = false, data = data }, JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

	}

}