using BusinessObject;
using DataAccess.DAO;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail p) => OrderDetailDAO.DeleteOrderDetail(p);

        public List<OrderDetail> GetOrderDetail() => OrderDetailDAO.GetOrderDetail();

        public OrderDetail GetOrderDetailById(int id) => OrderDetailDAO.FindOrderDetailById(id);

        public void SaveOrderDetail(OrderDetail c) => OrderDetailDAO.SaveOrderDetail(c);

        public void UpdateOrderDetail(OrderDetail p) => OrderDetailDAO.UpdateOrderDetail(p);
    }
}
