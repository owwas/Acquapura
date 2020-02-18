using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Acquapura.DataAccess;
using Acquapura.Business;
using System.IO;
using System.Drawing;

namespace Acquapura.Controllers
{
	public class CustomersController : Controller
	{
		string ErrorMessage = string.Empty;
		string SuccessMessage = string.Empty;
		private clsCustomers ObjClsCustomers = new clsCustomers();
		private Acquapura_dbEntities db = new Acquapura_dbEntities();

		// GET: Customers
		public ActionResult Index()
		{
			

			ViewBag.Delivered = db.Deliveries.Select(x => x.Delivered).Sum();
			ViewBag.Received = db.Deliveries.Select(x => x.Received).Sum();
			ViewBag.Balance = db.Deliveries.Select(x => x.Balance).Sum();
			ViewBag.CashReceived = db.Deliveries.Select(x => x.CashReceived).Sum();
			ViewBag.CashBalance = db.Deliveries.Select(x => x.CashBalance).Sum();

			ViewBag.SuccessMessage = TempData["SuccessMessage"];
			ViewBag.ErrorMessage = TempData["ErrorMessage"];

			return View();
		}
		public ActionResult List()
		{
			var customers = ObjClsCustomers.GetAllCustomers();
			return PartialView(customers);
		}

		// GET: Customers/Details/5
		public ActionResult Details(long? id)
		{

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customer customer = ObjClsCustomers.GetCustomerByID((Int16)id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		// GET: Customers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Customer customer)
		{
			if (ModelState.IsValid)
			{
				if (ObjClsCustomers.CreateCustomer(customer))
				{
					TempData["SuccessMessage"] = "Congrats ! Customer Added Successfully ...";
					return RedirectToAction("Index");
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

		// GET: Customers/Edit/5
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customer customer = ObjClsCustomers.GetCustomerByID((Int16)id);
			if (customer == null)
			{
				return HttpNotFound();
			}

			TempData["CustomerID"] = id;

			return View(customer);
		}

		// POST: Customers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Customer customer)
		{
			if (ModelState.IsValid)
			{
				Int16 Id = Convert.ToInt16(TempData["CustomerID"].ToString());

				if (customer.ID == Id)
				{

					var Customers = ObjClsCustomers.GetCustomerUpdater(customer);

					customer = null;

					if (ObjClsCustomers.UpdateCustomer(Customers))
					{
						TempData["SuccessMessage"] = "Congrats ! Customer Updated Successfully ...";
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

		// GET: Customers/Delete/5
		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}


			Customer customer = ObjClsCustomers.GetCustomerByID((Int16)id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			TempData["CustomerID"] = id;
			return View(customer);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			Int16 Id = Convert.ToInt16(TempData["CustomerID"].ToString());
			if (id == Id)
			{
				if (ObjClsCustomers.DeleteCustomer((Int16)id))
				{
					TempData["SuccessMessage"] = "Customer Deleted Successfully ...";
					return RedirectPermanent("/Customers/Index");
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

			return RedirectPermanent("/Customers/Index");

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
