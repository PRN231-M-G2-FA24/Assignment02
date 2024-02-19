using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(Order p) => OrderDAO.DeleteOrder(p);

        public List<Order> GetOrder() => OrderDAO.GetOrder();

        public Order GetOrderById(int id) => OrderDAO.FindOrderById(id);

        public void SaveOrder(Order c) => OrderDAO.SaveOrder(c);

        public void UpdateOrder(Order p) => OrderDAO.UpdateOrder(p);

        public List<OrderDetail> GetOrderDetails(int id) => OrderDAO.GetOrderDetails(id);
    }
}
