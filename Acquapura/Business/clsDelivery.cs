using Acquapura.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Acquapura.Business
{
    public class clsDelivery
    {

        private Acquapura_dbEntities dbcontext = new Acquapura_dbEntities();


        public List<Delivery> GetAllDeliveries(bool isdeleted = false)
        {
            var deliveries = dbcontext.Deliveries.ToList();
            return deliveries;
        }

        public List<Delivery> GetAllDeliveriesByCustomerId(long cusId)
        {
            var deliveries = dbcontext.Deliveries.Where(x => x.CustomerID == cusId).ToList();
            return deliveries;
        }

        public int GetDeliveriesCount()
        {
            var delivery = GetAllDeliveries().Count();
            return delivery;
        }
        public Delivery GetDeliveryByID(Int16 id)
        {
            var delivery = dbcontext.Deliveries.Where(i => i.ID == id).SingleOrDefault();
            return delivery;
        }

        public bool CreateDelivery(Delivery delivery)
        {
            try
            {

				dbcontext.Deliveries.Add(delivery);
                dbcontext.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateDelivery(Delivery delivery)
        {
            try
            {
                dbcontext.Entry(delivery).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

		public bool DeleteDelivery(Int16 ID)
		{
			try
			{
					Delivery delivery = GetDeliveryByID(ID);

					dbcontext.Deliveries.Remove(delivery);
					dbcontext.SaveChanges();
					return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbcontext.Dispose();
            }
        }
       
    }
}