using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool TaxExempt { get; set; }
        public string Observation { get; set; }
        public CustomerViewModel Customer { get; set; }
        public ICollection<OrderItemViewModel> OrderItem { get; set; }
        public decimal Total
        {
            get
            {
                decimal qtd = 0;
                foreach (var item in OrderItem)
                {
                    qtd += item.Quantity * item.UnitPrice;
                }
                return qtd;
            }
        }

    }
}
