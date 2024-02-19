using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.DTO;
using Repository;
using BusinessObject;
using Repository.Implements;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ODataController
    {
        
       
        private IProductRepository repository = new ProductRepository();
        public ProductsController()
        {
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
            => repository.GetProduct();

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            Product mem = new Product
            {

               CategoryId=product.CategoryId,
               ProductName=product.ProductName,
               Weight=product.Weight,
               UnitPrice=product.UnitPrice,
               UnitInStock=product.UnitInStock
            };
            var pTmp = repository.GetProductById(id);
            if (pTmp == null)

                return NotFound();
            pTmp = mem;
            pTmp.ProductId = id;
            repository.UpdateProduct(pTmp);
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO product)
        {
            Product mem = new Product
            {

                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitInStock = product.UnitInStock
            };
          
           repository.SaveProduct(mem);

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

           repository.DeleteProduct(product);

            return NoContent();
        }

       
    }
}
