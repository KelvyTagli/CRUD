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
        
        public int Insert(string nome, string Password)
        {
            string INSERT = "INSERT INTO Tb_user (Nome, Password) VALUES (@NOME, @PASS)";

            try
            {
                SqlCommand command = new SqlCommand(INSERT, connection);
                command.Parameters.Add(new SqlParameter("@NOME", nome));
                command.Parameters.Add(new SqlParameter("@PASS", Password));
                connection.Open();
                command.ExecuteNonQuery();
                return 1;
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

    class Login:Banco
    {
        private string Nome;
        private string Password;
        private string conf_Pass;

        public void NewLogin()
        {
            Banco Bd = new Banco();
            new Program();

            Console.WriteLine("| ==>   NEW LOGIN   <==|");
            Console.Write("| ==> Name de Usuario: ");
            Nome = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Nome))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Name de Usuario: ");
                Nome = Console.ReadLine();
            }

            Console.Write("| ==> Password: ");
            Password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Password))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Password: ");
                Password = Console.ReadLine();
            }

            Console.Write("| ==> Confirm Password: ");
            conf_Pass = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(conf_Pass))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Confirm Password: ");
                conf_Pass = Console.ReadLine();
            }

            if (Password == conf_Pass)
            {
                if (Bd.Insert(Nome, Password) == 1)
                {
                    Console.WriteLine("Cadastro Efetuado com sucesso");
                    Console.ReadKey();
                    Console.Clear();
                    Program.Main();
                } else
                {
                    Console.WriteLine("Erro No cadastro !!");
                    return;
                }
            }
        }
    }

    class Program
    {
        public static void Main()
        {
            Login login = new Login();

            Console.WriteLine("| ==> Bem Vindo <== |");
            Console.WriteLine("| ==>   LOGIN   <==|");
            Console.WriteLine("| [1] ==> Login");
            Console.WriteLine("| [2] ==> New Login");
            Console.WriteLine("| [3] ==> Sair");
            Console.Write("|>> ");
            int option = Int32.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    {
                        
                        break;
                    }
                case 2:
                    {
                        login.NewLogin();
                        break;
                    }
            }
        }
    }
}
