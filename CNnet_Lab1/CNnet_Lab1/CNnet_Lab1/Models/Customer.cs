using System;

namespace CNnet_Lab1.Models
{
    //Общая информация о сотруднике
    public class Customer
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Telephone { get; private set; }
        public string ProductName { get; private set; }
        public string Quantity { get; private set; }
        public string OtherInfo { get; private set; }

        public Customer(int id, string fullname, string telephone, string productname, string quantity, string otherInfo=null)
        {
            Id = id;

            if (string.IsNullOrEmpty(fullname))
            {
                throw new Exception("Customer should have a Name");
            }
            FullName = fullname;

            if (string.IsNullOrEmpty(telephone))
            {
                throw new Exception("Customer should have a Telephone");
            }
            Telephone = telephone;

            if (string.IsNullOrEmpty(productname))
            {
                throw new Exception("Customer should have a ProductName");
            }
            ProductName = productname;

            if (string.IsNullOrEmpty(quantity))
            {
                throw new Exception("Customer should have a Quantity");
            }
            Quantity = quantity;
            

            OtherInfo = otherInfo ?? string.Empty;
        }
        public override string ToString()
        {
            return $"Id: {Id},  FullName: {FullName}, Telephone: {Telephone}, ProductName: {ProductName}, Quantity: {Quantity},  OtherInfo: {OtherInfo}";
        }
    }
}
