using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);

        public List<Product> GetProduct() => ProductDAO.GetProduct();

        public Product GetProductById(int id) => ProductDAO.FindProductById(id);

        public void SaveProduct(Product c) => ProductDAO.SaveProduct(c);

        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);
    }
}
