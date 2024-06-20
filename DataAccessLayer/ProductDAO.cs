using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static string connectionString = "Server=(local);uid=sa;pwd=123;database=MyStore;Encrypt=True;TrustServerCertificate=True";

        public static List<Product> GetProducts()
        {
            List<Product> listProducts = new List<Product>();
            string query = "SELECT ProductId, ProductName, CategoryId, UnitPrice, UnitsInStock FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int productId = reader.GetInt32("ProductId");
                        string productName = reader.GetString("ProductName");
                        int categoryId = reader.GetInt32("CategoryId");
                        short unitInStock = reader.GetInt16("UnitsInStock"); 
                        decimal unitPrice = reader.GetDecimal("UnitPrice");

                        Product product = new Product(productId, productName, categoryId, unitInStock, unitPrice);
                        listProducts.Add(product);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return listProducts;
        }

        public static void SaveProduct(Product p)
        {
            string query = "INSERT INTO Products (ProductName, CategoryId, UnitPrice, UnitsInStock) VALUES (@ProductName, @CategoryId, @UnitPrice, @UnitsInStock); SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", p.ProductName);
                command.Parameters.AddWithValue("@CategoryId", p.CategoryId);
                command.Parameters.AddWithValue("@UnitPrice", p.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", p.UnitInStock);

                try
                {
                    connection.Open();
                    p.ProductId = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void UpdateProduct(Product product)
        {
            string query = "UPDATE Products SET ProductName = @ProductName, CategoryId = @CategoryId, UnitPrice = @UnitPrice, UnitsInStock = @UnitsInStock WHERE ProductId = @ProductId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitInStock);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void DeleteProduct(Product product)
        {
            string query = "DELETE FROM Products WHERE ProductId = @ProductId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static Product GetProductById(int id)
        {
            Product product = null;
            string query = "SELECT ProductId, ProductName, CategoryId, UnitPrice, UnitsInStock FROM Products WHERE ProductId = @ProductId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int productId = reader.GetInt32("ProductId");
                        string productName = reader.GetString("ProductName");
                        int categoryId = reader.GetInt32("CategoryId");
                        short unitPrice = reader.GetInt16("UnitPrice");
                        short unitInStock = reader.GetInt16("UnitsInStock"); // Changed to GetInt16 for short type

                        product = new Product(productId, productName, categoryId, unitPrice, unitInStock);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return product;
        }
    }
}
