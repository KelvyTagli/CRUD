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

        public int Delete(string nome)
        {
            try
            {
                connection.Open();
                SqlCommand commandDelete = new SqlCommand("DELETE FROM Tb_user WHERE NOME = @NOME",connection);
                commandDelete.Parameters.Add(new SqlParameter("@NOME", nome));
                commandDelete.ExecuteNonQuery();
                Console.WriteLine("Usuario Excluido com Sucesso");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro {0}",ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        public string Validate_Username(string username)
        {
            string name = "";

            SqlCommand command = new SqlCommand("SELECT * FROM Tb_user WHERE NOME = @NOME",connection);
            try
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("@NOME", username));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    name += reader.GetString(1);
                    if (name == username)
                    {
                        break;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: {0}", ex);
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

            SqlCommand command = new SqlCommand("SELECT * FROM Tb_user WHERE PASS = @PASS", connection);
            try
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("@PASS", password));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _pass += reader.GetString(2);
                    if ( _pass == password)
                    {
                        break;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: {0}", ex);
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
        private string conf_pass;

        public void NewLogin()
        {
            new Program();

            Console.WriteLine("| ==>   NEW LOGIN   <==  |");
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
            conf_pass = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(conf_pass))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> Confirm Password: ");
                conf_pass = Console.ReadLine();
            }

            if (Password == conf_pass)
            {
                if (Bd.Insert(Nome, Password) == 0)
                {
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
            
          if (Bd.Validate_Username(Nome).Equals(Nome) == true && Bd.Validate_Password(Password).Equals(Password) == true )
            {
                Console.WriteLine("Login Efetuado com Sucesso");
            } 
            else
            {
                Console.WriteLine("Erro No Login");
            }
        }

        public void DeleteLogin()
        {
            Console.WriteLine("|  ==>  DELETE LOGIN  <== |");
            Console.Write("| ==> UserName: ");
            Nome = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Nome))
            {
                Console.Clear();
                Console.WriteLine("| Operação Invalida, Digite Novamente |");
                Console.Write("| ==> UserName: ");
                Nome = Console.ReadLine();
            }
            if (Bd.Validate_Username(Nome).Equals(Nome) == true)
            {
                Console.WriteLine("| Login Encotrado com Sucesso");
                Console.WriteLine("| Login ==> {0}",Nome);
                Console.WriteLine("| Deseja Excluir Esse Login");
                Console.WriteLine("| [1] ==> SIM");
                Console.WriteLine("| [2] ==> NÃO");
                Console.Write("|>> ");
                int boolean = Int32.Parse(Console.ReadLine());
                switch(boolean)
                {
                    case 1:
                        {
                            Delete(Nome);
                            break;
                        }
                    default:
                        {
                            Program.Main();
                            break;
                        }
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
            Console.WriteLine("| ==>   LOGIN   <== |");
            Console.WriteLine("| [1] ==> Login");
            Console.WriteLine("| [2] ==> New Login");
            Console.WriteLine("| [3] ==> Delete Login");
            Console.WriteLine("| [4] ==> Edit Login");
            Console.WriteLine("| [0] ==> Sair");
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
                case 3:
                    {
                        login.DeleteLogin();
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
