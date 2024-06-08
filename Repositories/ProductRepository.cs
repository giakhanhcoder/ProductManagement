using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);

        public List<Product> GetProducts() => ProductDAO.GetProducts();

        public Product GetProductbyId(int id) => ProductDAO.GetProductById(id);

        public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);

        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);

   
    }
}
