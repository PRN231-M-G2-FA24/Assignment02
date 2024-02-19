using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail c);
        OrderDetail GetOrderDetailById(int id);
        void DeleteOrderDetail(OrderDetail p);
        void UpdateOrderDetail(OrderDetail p);
        List<OrderDetail> GetOrderDetail();
    }
}
