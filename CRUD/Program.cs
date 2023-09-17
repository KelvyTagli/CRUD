using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD
{
    class Banco
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\kelvy\\OneDrive\\Documentos\\Git_Projetos\\Visual Studio C#\\Estudos\\CRUD\\CRUD_Database.mdf\";Integrated Security=True;Connect Timeout=30");
        
        public int Insert(string nome, string Password)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Tb_user (NOME, PASS) VALUES (@NOME, @PASS)",connection);
                command.Parameters.Add(new SqlParameter("@NOME", nome));
                command.Parameters.Add(new SqlParameter("@PASS", Password));
                command.ExecuteNonQuery();
                Console.WriteLine("Cadastro efetuado com Sucesso");
                Console.ReadKey();
            }
            catch (SqlException ex)
            {
               Console.WriteLine($"{ex}");
            }
            finally
            {
                connection.Close();
            }
            return 0;
        }

        public string Validate_Username(string username)
        {
            string name = "";

            SqlCommand command = new SqlCommand("SELECT * FROM Tb_user",connection);
            
            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        name = dr.ToString();
                        Console.WriteLine($"{name}");
                        if (name == username)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Erro");
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

            return name;
        }

        public string Validate_Password(string password)
        {
            string _pass = "";

            SqlCommand command = new SqlCommand("SELECT * FROM Tb_user", connection);
            
            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _pass = dr.ToString();
                        Console.WriteLine($"{_pass}");
                        if (_pass == password)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Erro");
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

            return _pass;
        }
    }

    class Login:Banco
    {
        Banco Bd = new Banco();

        private string Nome;
        private string Password;

        public void NewLogin()
        {
            new Program();
            string _Name;
            string _Password;
            string conf;

            Console.WriteLine("| ==>   NEW LOGIN   <==  |");
            Console.Write("| ==> Name de Usuario: ");
            _Name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(_Name))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Name de Usuario: ");
                _Name = Console.ReadLine();
            }

            Console.Write("| ==> Password: ");
            _Password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(_Password))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Password: ");
                _Password = Console.ReadLine();
            }

            Console.Write("| ==> Confirm Password: ");
            conf = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(conf))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Confirm Password: ");
                conf = Console.ReadLine();
            }

            if (_Password == conf)
            {
                if (Bd.Insert(_Name, _Password) == 0)
                {
                    Console.WriteLine($"name{_Name}");
                    Console.WriteLine($"Pass{_Password}");
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

        public void SingIn()
        {
            Console.WriteLine("|  ==>  LOGIN  <== |");
            Console.Write("| ==> UserName: ");
            Nome = Console.ReadLine();
            while(string.IsNullOrWhiteSpace(Nome))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> UserName: ");
                Nome = Console.ReadLine();
            }

            Console.Write("| ==> Password: ");
            Password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Password))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> UserName: ");
                Password = Console.ReadLine();
            }
            
            if (Nome.Equals(Bd.Validate_Username(Nome)) && Password.Equals(Bd.Validate_Password(Password)))
            {
                Console.Clear();
                Console.WriteLine($"Login Efetuado");
                Console.ReadKey();
            } else
            {
                Console.WriteLine("username ou senha Incorretos"); 
            }
        }
    }

    class Program
    {
        public static void Main()
        {
            Login login = new Login();

            Console.WriteLine("| ==> Bem Vindo <== |");
            Console.WriteLine("| ==>   LOGIN   <== |");
            Console.WriteLine("| [1] ==> Login");
            Console.WriteLine("| [2] ==> New Login");
            Console.WriteLine("| [3] ==> Sair");
            Console.Write("|>> ");
            int option = Int32.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    {
                        login.SingIn();
                        break;
                    }
                case 2:
                    {
                        login.NewLogin();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
