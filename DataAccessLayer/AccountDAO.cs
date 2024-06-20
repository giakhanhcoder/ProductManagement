using BusinessObjects;
using System;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        private static string connectionString = "Server=(local);uid=sa;pwd=123;database=MyStore;Encrypt=True;TrustServerCertificate=True";

        public static AccountMember GetAccountById(string accountID)
        {
            AccountMember accountMember = null;

            string query = "SELECT MemberId, MemberPassword, MemberRole FROM AccountMember WHERE MemberId = @accountID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountID", accountID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        accountMember = new AccountMember
                        {
                            MemberId = reader.GetString(0),
                            MemberPassword = reader.GetString(1),
                            MemberRole = reader.GetInt32(2)
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Optionally, rethrow the exception or handle it as needed
                }
                finally
                {
                    connection.Close();
                }
            }
            return accountMember;
        }
    }
}
