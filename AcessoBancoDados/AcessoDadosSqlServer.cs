using AcessoBancoDados.Properties;
using System;

//Namespace's que contém as classes que manipulam dados
using System.Data;
using System.Data.SqlClient;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        private SqlConnection CriarConexao() //METODO: Pega a string de conexão com o banco nas Propriedades. 
        {
            return new SqlConnection(Settings.Default.stringConexao);
            // OU PASSAR DIRETAMENTE A STRING
            //return new SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=dbCliente;Integrated Security=True"); String de conexão
        }

        //Istancia que irá carregar os parametros das Stored Produres. 
        private SqlParameterCollection sqlParamenterCollection = new SqlCommand().Parameters;

        public void LimparParametros()//METODO: Que limpa os parametros existente na Variavel "sqlParamenterCollection" 
        {
            sqlParamenterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParametro, object valorParametro)  //METODO: Que Adiciona os parametros na Variavel "sqlParamenterCollection"
        {
            sqlParamenterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
 
        }
        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuSql)//METODO: Que manipula dados para SQLSERVER Sem retorno de Coleção.
        {
            try 
            {
                //Criar Conexao 
                SqlConnection sqlConnection = CriarConexao();
                //Abrir Conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (Dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuSql;// pode ser o SQL ou o nome da STORED PROCEDURE
                sqlCommand.CommandTimeout = 7200; // tempo de conexao em segundos Padrao e de 30 segundos.

                // Adicionar os Parametros dos Comandos 
                foreach (SqlParameter sqlParameter in sqlParamenterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                // Executar o comando, ou seja, mandar o comando ir ate o banco de dados
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
           
        }


        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuSql)//METODO: Que manipula dados para SQLSERVER Com retorno de Coleção.
        {
            try
            {
                //Criar Conexao 
                SqlConnection sqlConnection = CriarConexao();
                //Abrir Conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (Dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuSql;// pode ser o SQL ou o nome da STORED PROCEDURE
                sqlCommand.CommandTimeout = 7200; // tempo de conexao em segundos Padrao e de 30 segundos.

                // Adicionar os Parametros dos Comandos 
                foreach (SqlParameter sqlParameter in sqlParamenterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

               // Converte o Sql para ser compativel com DATA TABLE 
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //DataTable = tabela vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new DataTable();
                //Preeche o datatable com os valores trazidos do banco
                sqlDataAdapter.Fill(dataTable);
                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
