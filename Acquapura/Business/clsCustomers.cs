using Acquapura.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Acquapura.Business
{
    public class clsCustomers
    {

        private Acquapura_dbEntities dbcontext = new Acquapura_dbEntities();


        public List<Customer> GetAllCustomers(bool isdeleted = false)
        {
            var customers = dbcontext.Customers.ToList();
            return customers;
        }

        public int GetCustomersCount()
        {
            var customer = GetAllCustomers().Count();
            return customer;
        }
        public Customer GetCustomerByID(Int16 id)
        {
            var customer = dbcontext.Customers.Where(i => i.ID == id).SingleOrDefault();
            return customer;
        }

        public bool CreateCustomer(Customer customer)
        {
            try
            {

				dbcontext.Customers.Add(customer);
                dbcontext.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                dbcontext.Entry(customer).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

		public bool DeleteCustomer(Int16 ID)
		{
			try
			{
					Customer customer = GetCustomerByID(ID);

					dbcontext.Customers.Remove(customer);
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
        public Customer GetCustomerUpdater(Customer customer)
        {
            Customer Customers = GetCustomerByID((Int16)customer.ID);

            Customers.Name = customer.Name;
            Customers.Address = customer.Address;
            Customers.Phone = customer.Phone;
            Customers.CodeNo = customer.CodeNo;
            return Customers;
        }
    }
}