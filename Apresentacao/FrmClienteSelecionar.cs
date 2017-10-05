using System;
using System.Windows.Forms;
using Negocios;
using ObjetosTransferencia;

namespace Apresentacao
{
    public partial class FrmClienteSelecionar : Form
    {
        public FrmClienteSelecionar()
        {
            InitializeComponent();

            //Nao gerar  linhas  automaticas 
            dataGridViewPrincipal.AutoGenerateColumns = false;

        }

     

        private void btnFechar_Click(object sender, EventArgs e)// EVENTO DE CLICK do BOTÃO [FECHAR]
        {
            this.Dispose();
            this.Close(); 
        }

        private void btnPesquisar_Click(object sender, EventArgs e)// EVENTO DE CLICK do BOTÃO [PESQUISAR]
        {

            AtualizarGrid();

        }

        private void btnInserir_Click(object sender, EventArgs e)// EVENTO DE CLICK do BOTÃO [INSERIR]
        {
            FrmClienteCadastrar frm = new FrmClienteCadastrar(AcaoNaTela.Inserir, null);
            DialogResult dialogResult = frm.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                AtualizarGrid();
            }

        }

        private void btnAlterar_Click(object sender, EventArgs e)// EVENTO DE CLICK do BOTÃO [ALTERAR]
        {

            //Verifica se possui algum registro selecionada 
            if (dataGridViewPrincipal.SelectedRows.Count == 0)   // Caso contrario , Possui linha selecionada no Grid.
            {
                MessageBox.Show("Nenhum cliente selecionado");
                return;                                           //Retorno de Parada do evento 
            }

           
            //( Pega os dados da linha selecionada (DataBoundItem))( SelctedRows[0] pega a primeira linha selecionada.) 
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);   // Pega o cliente selecionado

            FrmClienteCadastrar frm = new FrmClienteCadastrar(AcaoNaTela.Alterar, clienteSelecionado);
            DialogResult dialogResult = frm.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                AtualizarGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e) // EVENTO DE CLICK do BOTÃO [EXCLUIR]
        {
            //Verifica se possui algum registro selecionada 
            if (dataGridViewPrincipal.SelectedRows.Count == 0 )   // Caso contrario , Possui linha selecionada no Grid.
            {
                MessageBox.Show("Nenhum cliente selecionado");
                return;                                           //Retorno de Parada do evento 
            }

            //Confirmação de Exclusão
           DialogResult result =  MessageBox.Show("Deseja realmente excluir?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //MessageBox Construtor 7

           if (result == DialogResult.No)
           {
               return;                                            //Retorno de Parada do Evento 
           }

            //( Pega os dados da linha selecionada (DataBoundItem))( SelctedRows[0] pega a primeira linha selecionada.) 
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);   // Pega o cliente selecionado

            //Instancia a regra de negócio
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            string retornoExcluir = clienteNegocio.Excluir(clienteSelecionado);             // Exclui as informações do banco 

            //Verifica se a exclusao foi feita com sucesso 
            // Se o retorno for um numero, entao foi exluido com sucesso.
            try
            {
                int idCliente = Convert.ToInt32(retornoExcluir);
                MessageBox.Show("Cliente excluido com Sucesso!!","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                AtualizarGrid();            
            }
            catch 
            {

                MessageBox.Show("Nao foi possivel excluir. Detalhes: "+ retornoExcluir, "Erro",MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }

        private void btnConsultar_Click(object sender, EventArgs e)// EVENTO DE CLICK do BOTÃO [CONSULTAR]
        {

            //Verifica se possui algum registro selecionada 
            if (dataGridViewPrincipal.SelectedRows.Count == 0)   // Caso contrario , Possui linha selecionada no Grid.
            {
                MessageBox.Show("Nenhum cliente selecionado");
                return;                                           //Retorno de Parada do evento 
            }

            //( Pega os dados da linha selecionada (DataBoundItem))( SelctedRows[0] pega a primeira linha selecionada.) 
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);   // Pega o cliente selecionado

            FrmClienteCadastrar frm = new FrmClienteCadastrar(AcaoNaTela.Consultar, clienteSelecionado);
            frm.ShowDialog();
        }


        void AtualizarGrid() // METODO: Atualizar os dados da GRID
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ClienteColecao clienteColecao = new ClienteColecao();

            clienteColecao = clienteNegocio.ConsultarPorNome(txtPesquisar.Text);     //Consultar por Nome do Alunoe envia para a colecao.

            dataGridViewPrincipal.DataSource = null;                                 //Limpa o Grid  

            dataGridViewPrincipal.DataSource = clienteColecao;                       //Manda as Informações para o Grid

            //dataGridViewPrincipal.Update();  

            //dataGridViewPrincipal.Refresh();
        }
    }
}
