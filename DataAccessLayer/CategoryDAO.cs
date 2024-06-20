using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        private static string connectionString = "Server=(local);uid=sa;pwd=123;database=MyStore;Encrypt=True;TrustServerCertificate=True";

        public static List<Category> GetCategories()
        {
            List<Category> listCategories = new List<Category>();
            string query = "SELECT CategoryId, CategoryName FROM Categories";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int categoryId = reader.GetInt32(0);
                        string categoryName = reader.GetString(1);
                        Category category = new Category(categoryId, categoryName);
                        listCategories.Add(category);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return listCategories;
        }
    }
}
