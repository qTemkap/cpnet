using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNnet_Lab1.Models
{
    //Оганизация
    public class Organization
    {
        private List<Customer> _custemers = new List<Customer>(); 
        public int OrganizationId { get;  private set; }
        public string OrganizationName { get; private set; }
        public IEnumerable<Customer> Customers { get => _custemers; }

        public Organization(int organizationID, string organizationName, IEnumerable<Customer> custemers = null)
        {
            OrganizationId = organizationID;
            if(string.IsNullOrEmpty(organizationName))
            {
                throw new Exception("Company should have a Name");
            }
            OrganizationName = organizationName;
            _custemers = (custemers == null) 
                ? new List<Customer>() 
                : custemers.ToList();
        }

        public void AddCustomer(int id, string fullName, string telephone, string productName, string quantity, string otherInfo=null)
        {
            var existingEmployee = _custemers.SingleOrDefault(x => x.Id == id);
            if(existingEmployee != null)
            {
                throw new Exception("Employee with such Id already exists");
            }
            var customer = new Customer(id, fullName, telephone, productName, quantity, otherInfo);

            _custemers.Add(customer);
        }
    }
}
