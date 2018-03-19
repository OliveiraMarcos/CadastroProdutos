using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Utils
{
    public class CompareOrderItem : IEqualityComparer<OrderItem>
    {
        public bool Equals(OrderItem x, OrderItem y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.ProductId == y.ProductId && x.Quantity == y.Quantity && x.UnitPrice == y.UnitPrice;
        }

        public int GetHashCode(OrderItem obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashName = obj.ProductId.GetHashCode();

            //Get hash code for the Code field.
            int hashCode = obj.UnitPrice.GetHashCode();

            //Calculate the hash code for the product.
            return hashName ^ hashCode;
        }
    }
}
