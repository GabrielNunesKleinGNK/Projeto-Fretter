using DbUp;
using DbUp.Helpers;
using DbUp.ScriptProviders;
using System;
using System.Data.SqlClient;

namespace Fretter.Database
{
    class Program
    {
        public const string _tabelaVerionamento = "VersionamentoScript";
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ExibirErro("Argumento inválido a ConnectionString não foi fornecida ou é inválida");
                return;
            }

            string connectionString = args[0];
            string schema = args.Length > 1 ? args[1].ToString() : "dbo";

            SqlConnectionStringBuilder csBuilder =
                new SqlConnectionStringBuilder(connectionString);

            ExibirLog($"DbUp Update Iniciado...");
            ExibirLog($"Server: '{csBuilder.DataSource}' DataBase: '{csBuilder.InitialCatalog}'");

            bool conexao = TestarConexaoDB(csBuilder, schema);
            if (conexao)
            {
                IniciarMigrateScript(schema, connectionString);
                IniciarMigrateObject(connectionString);
            }

            ExibirLog($"DbUp Update Finalizado.");
        }
        private static bool TestarConexaoDB(SqlConnectionStringBuilder csBuilder, string schema)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(csBuilder.ConnectionString))
                {
                    conn.Open();
                    ExibirSucesso("Conexão estabelecida com sucesso!");
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 1 FROM sys.schemas WHERE name = '{schema}'", conn);
                    int result = (int?)cmd.ExecuteScalar() == null ? 0 : 1;
                    if (result == 0)
                        throw new Exception($"O schema informado: '{schema}' não foi encontrado.");
                    else
                        ExibirSucesso($"O schema: '{schema}' foi encontrado!");

                    return true;
                }
            }
            catch (Exception e)
            {
                ExibirErro($"Erro ao tentar acessar a base de dados. Error: {e.Message}");
                return false;
            }
        }
        private static void IniciarMigrateScript(string schema, string connectionString)
        {

            ExibirLog($"Iniciando Migrate Scripts...");

            FileSystemScriptOptions fsso = new FileSystemScriptOptions();
            fsso.IncludeSubDirectories = true;

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem($"{AppDomain.CurrentDomain.BaseDirectory}/SqlScripts/Migrate/", fsso)
                    //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .JournalToSqlTable(schema, _tabelaVerionamento)
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
                ExibirErro(result.Error.Message);
            else
                ExibirSucesso("Scripts atualizados com sucesso!");

        }
        private static void IniciarMigrateObject(string connectionString)
        {
            ExibirLog($"Iniciando Migrate Objects...");

            FileSystemScriptOptions fsso = new FileSystemScriptOptions();
            fsso.IncludeSubDirectories = true;

            var upgraderObjects =
               DeployChanges.To
                     .SqlDatabase(connectionString)
                     .WithScriptsFromFileSystem($"{AppDomain.CurrentDomain.BaseDirectory}/SqlScripts/Objects/", fsso)
                     .JournalTo(new NullJournal())
                     .Build();


            var result = upgraderObjects.PerformUpgrade();

            if (!result.Successful)
                ExibirErro(result.Error.Message);
            else
                ExibirSucesso("Objects atualizados com sucesso!");
        }
        private static void ExibirLog(string msg)
        {
            Console.ResetColor();
            Console.WriteLine($"{DateTime.Now.ToString()} - {msg}");
        }
        private static void ExibirErro(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now.ToString()} - {msg}");
            Console.ResetColor();
            throw new ApplicationException(msg);
        }
        private static void ExibirSucesso(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now.ToString()} - {msg}");
            Console.ResetColor();
        }
    }
}
