using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetail()
        {
            var listOrderDetail = new List<OrderDetail>();

            try
            {

                using (var context = new AppDbContext())
                {
                  
                    listOrderDetail = context.OrderDetails.Include(s=>s.Product).Include(s=>s.Order).ToList();

                  
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetail;

        }
        public static OrderDetail FindOrderDetailById(int OrderDetailId)
        {
            OrderDetail p = new OrderDetail();
            try
            {
                using (var context = new AppDbContext())
                {
                    p = context.OrderDetails.Include(s => s.Product).Include(s => s.Order).FirstOrDefault(x => x.OrderId == OrderDetailId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return p;
        }
        public static void SaveOrderDetail(OrderDetail p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.OrderDetails.Add(p);
                    context.SaveChanges();
                }

                Console.WriteLine("check");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void UpdateOrderDetail(OrderDetail p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.OrderDetails.Update(p);
                    // context.Entry<Products>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void DeleteOrderDetail(OrderDetail p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var p1 = context.OrderDetails.SingleOrDefault(x => x.OrderId == p.OrderId);
                    context.OrderDetails.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
