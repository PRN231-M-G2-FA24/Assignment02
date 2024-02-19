using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProduct()
        {
            var listProduct = new List<Product>();

            try
            {

                using (var context = new AppDbContext())
                {
                    listProduct = context.Products.Include(s=>s.Category).ToList();

                 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProduct;

        }
        public static Product FindProductById(int ProductId)
        {
            Product p = new Product();
            try
            {
                using (var context = new AppDbContext())
                {
                    p = context.Products.Include(s => s.Category).SingleOrDefault(x => x.ProductId == ProductId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return p;
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }

                Console.WriteLine("check");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Products.Update(p);
                    // context.Entry<Products>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var p1 = context.Products.SingleOrDefault(x => x.ProductId == p.ProductId);
                    context.Products.Remove(p1);
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
