using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {


        public static List<Product> listProducts = new List<Product>();

        public ProductDAO()
        {
            Product chai = new Product(1, "Chai", 1, 39, 18);
            Product chang = new Product(2, "Chang", 1, 17, 19);
            Product aniseedSyrup = new Product(3, "Aniseed Syrup", 2, 13, 10);
            listProducts = new List<Product>() { chai, chang, aniseedSyrup };
        }

        public static List<Product> GetProducts()
        {
            return listProducts;
        }


        /* public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var db = new MyStoreContext();
                listProducts = db.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        */

        public static void SaveProduct(Product p)
        {
            listProducts.Add(p);
        }

        public static void UpdateProduct(Product product)
        {
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    p.ProductId = product.ProductId;
                    p.ProductName = product.ProductName;
                    p.CategoryId = product.CategoryId;
                    p.UnitInStock = product.UnitInStock;
                    p.UnitPrice = product.UnitPrice;
                }
            }
        }

        public static void DeleteProduct(Product product)
        {
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == product.ProductId)
                {
                    listProducts.Remove(p);
                }
            }
        }

        public static Product GetProductById(int id)
        {
            foreach (Product p in listProducts.ToList())
            {
                if (p.ProductId == id)
                {
                    return p;
                }
            }
            return null;
        }

    }
}



