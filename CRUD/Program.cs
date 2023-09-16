using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class Banco
    {
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        public void Insert(string nome, string Password)
        {
            string INSERT = "INSERT INTO Tb_user (Nome, Password) VALUES (@NOME, @PASS)";

            try
            {
                SqlCommand command = new SqlCommand(INSERT, connection);
                command.Parameters.Add(new SqlParameter("@NOME", nome));
                command.Parameters.Add(new SqlParameter("@PASS", Password));
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
               Console.WriteLine($"{ex}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Select(string nome)
        {
            string SELECT = "SELECT * FROM Tb_user";

            try
            {
                SqlCommand command = new SqlCommand(SELECT, connection);
                connection.Open();
                SqlDataReader sqlData = command.ExecuteReader();

                while (sqlData.Read())
                {
                    Console.WriteLine($"{sqlData}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"{ex}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
