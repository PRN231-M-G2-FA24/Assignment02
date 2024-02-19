using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
        void SaveOrder(Order c);
        Order GetOrderById(int id);
        void DeleteOrder(Order p);
        void UpdateOrder(Order p);
        List<Order> GetOrder();
        List<OrderDetail> GetOrderDetails(int id);
    }
}
