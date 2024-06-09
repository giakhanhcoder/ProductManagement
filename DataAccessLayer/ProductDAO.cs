using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {


        public static ObservableCollection<Product> listProducts = InitializeProducts();
        private static int nextProductId = 4;

        private static ObservableCollection<Product> InitializeProducts()
        {
            Product chai = new Product(1, "Chai", 3, 12, 18);
            Product chang = new Product(2, "Chang", 1, 23, 19);
            Product aniseed = new Product(3, "Aniseed Syrep", 2, 23, 10);

            return new ObservableCollection<Product> { chai, chang, aniseed };
        }


        public static ObservableCollection<Product> GetProducts()
        {
            return listProducts;
        }

        /* public static List<Product> GetProducts()
        {
            return listProducts;
        } */


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
            p.ProductId = nextProductId;
            nextProductId++;
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



