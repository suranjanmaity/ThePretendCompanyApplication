using LINQExample_Northewind.Models;

namespace LINQExample_Northwind.Comparer
{
    public class OrderComparer : IEqualityComparer<Order>
    {
        bool IEqualityComparer<Order>.Equals(Order? x, Order? y)
        {
            if (x != null && y != null)
            {
                if(x.OrderID == y.OrderID && 
                   x.RequiredDate == y.RequiredDate &&
                   x.OrderDate == y.OrderDate &&
                   x.ShippedDate == y.ShippedDate &&
                   x.ShipVia == y.ShipVia &&
                   x.Freight == y.Freight &&
                   x.ShipName == y.ShipName &&
                   x.ShipAddress == y.ShipAddress &&
                   x.ShipCity == y.ShipCity &&
                   x.ShipRegion == y.ShipRegion &&
                   x.ShipPostalCode == y.ShipPostalCode &&
                   x.ShipCountry == y.ShipCountry &&
                   x.CustomerID == y.CustomerID &&
                   x.EmployeeID == y.EmployeeID)
                {
                    return true;
                }
            }
            return false;
        }

        int IEqualityComparer<Order>.GetHashCode(Order obj)
        {
            return obj.OrderID.GetHashCode();
        }
    }
}
