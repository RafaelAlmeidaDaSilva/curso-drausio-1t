using AcessoBancoDados;
using ObjetosTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class ClienteNegocio
    {
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();
        public string Inserir(Cliente cliente) //METODO: Metodo que acessa Stored Procedure "uspClienteInserir" 
        {
            try
            {

            
            acessoDadosSqlServer.LimparParametros(); //limpa paramtros

            // Adicionar novos parametros
            acessoDadosSqlServer.AdicionarParametros("@Nome",cliente.Nome);
            acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
            acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
            acessoDadosSqlServer.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);
            
            //Envia os parametros para o Banco e retorna o ID
            String idCliente = Convert.ToString(acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir"));
            return idCliente;
           
            }
            catch (Exception ex)
            {
                return ex.Message;
            }   
        }

        public string Alterar(Cliente cliente) //METODO: Metodo que acessa Stored Procedure "uspClienteAlterar" 
        {
            try
            {
                acessoDadosSqlServer.LimparParametros(); 
                acessoDadosSqlServer.AdicionarParametros("@IdCliente",cliente.idCliente );
                acessoDadosSqlServer.AdicionarParametros("@Nome",cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento",cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo",cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimiteCompra",cliente.LimiteCompra);
                string IdCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure,"uspClienteAlterar").ToString();
                return Convert.ToString(IdCliente);
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        public string Excluir(Cliente cliente) //METODO: Metodo que acessa Stored Procedure "uspClienteExcluir" 
        {

            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@idCliente",cliente.idCliente);
                string IdCliente = Convert.ToString(acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir"));
                return IdCliente;
            }
            catch ( Exception ex)
            {
                return ex.Message;
                throw;
            }
        
        }

        public ClienteColecao ConsultarPorNome(string nome)    //METODO: Metodo que acessa Stored Procedure "uspClienteConsultarPorNome" 
        {
            try
            { 
                //Criar uma colecao nova de clientes (aqui ela está vazia)
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome",nome);

                DataTable ClienteDataTable = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                //Percorrer  o DataTable e transforma em uma Colecao
                //Cada linha do DataTable é um cliente.
                //Data = Dados e Row = linha 
                //Foreach FOR + EACH  = PARA CADA
                // Para cada linha dentro das Rows(Linha) / Coluns(Coluna) do ClienteDataTable
                foreach (DataRow linha  in ClienteDataTable.Rows )
                {
                   //Criar Cliente Vazio
                   //Colocar os Dados da Linha nele 
                   //Adiciona ele na colecao 
                    Cliente cliente = new Cliente();
                    cliente.idCliente = Convert.ToInt32(linha["Cod_Cliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome_Cliente"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["Data_Nascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);
                   
                    // adiciona o cliente na colecao
                    clienteColecao.Add(cliente);

                }

                return clienteColecao;

      
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível conectar por nome.  Detalhes: "+ex.Message);
            }
            

        }

        public ClienteColecao ConsultarPorCodigo(int idCliente)//METODO: Metodo que acessa Stored Procedure "uspClienteConsultaPorCodigo" 
        {
            try
            {
                 //Criar uma colecao nova de clientes (aqui ela está vazia)
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente",idCliente);
               
                DataTable ClienteDataTable = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultaPorCodigo");

                //Percorrer  o DataTable e transforma em uma Colecao
                //Cada linha do DataTable é um cliente.
                //Data = Dados e Row = linha 
                //Foreach FOR + EACH  = PARA CADA
                // Para cada linha dentro das Rows(Linha) / Coluns(Coluna) do ClienteDataTable
                foreach (DataRow linha  in ClienteDataTable.Rows )
                {
                   //Criar Cliente Vazio
                   //Colocar os Dados da Linha nele 
                   //Adiciona ele na colecao 
                    Cliente cliente =  new Cliente ();
                    cliente.idCliente =Convert.ToInt32( linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);
                   
                   //adiciona o cliente na colecao
                    clienteColecao.Add(cliente);

                }

                return clienteColecao;

            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro. Nao foi possível executar a consulta.  Detalhes: "+ex.Message);
            }

        }
    }
}
