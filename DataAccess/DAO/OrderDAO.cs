using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        public static List<Order> GetOrder()
        {
            var listOrder = new List<Order>();

            try
            {

                using (var context = new AppDbContext())
                {
                    
                    listOrder = context.Orders.Include(s=>s.Member).ToList();

                  
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrder;

        }
        public static Order FindOrderById(int OrderId)
        {
            Order p = new Order();
            try
            {
                using (var context = new AppDbContext())
                {
                    p = context.Orders.Include(s => s.Member).SingleOrDefault(x => x.OrderId == OrderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return p;
        }
        public static void SaveOrder(Order p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Orders.Add(p);
                    context.SaveChanges();
                }

                Console.WriteLine("check");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void UpdateOrder(Order p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Orders.Update(p);
                    // context.Entry<Products>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void DeleteOrder(Order p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var p1 = context.Orders.SingleOrDefault(x => x.OrderId == p.OrderId);
                    context.Orders.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static List<OrderDetail> GetOrderDetails(int id)
        {
            var listDetails = new List<OrderDetail>();
            try
            {

                using (var context = new AppDbContext())
                {
                    listDetails = context.OrderDetails.Where(x => x.OrderId == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listDetails;

        }
    }
}
