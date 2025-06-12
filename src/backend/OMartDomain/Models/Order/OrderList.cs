using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order
{
    public class OrderList
    {
       public int OrderListID{ get; set; }
        public string OrderID{ get; set; }
        public string ProductID{ get; set; }
        public string productName { get; set; }


        public string brand { get; set; }
        //public string establishment_name { get; set; }
        public string ImageName { get; set; }
        public double Quantity{ get; set; }
        public double Price{ get; set; }
        public double SalePrice{ get; set; }
        public double TaxAmount{ get; set; }
        public double DeliveryAmount{ get; set; }
        public double Amount{ get; set; }
        public string TaxID{ get; set; }
        public string UnitofMeasure { get; set; }
        public string UnitofQuantity { get; set; }
       


    }
}