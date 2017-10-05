using System;
using System.Windows.Forms;

using ObjetosTransferencia;
using Negocios;

namespace Apresentacao
{
    public partial class FrmClienteCadastrar : Form
    {
        AcaoNaTela acaoNaTelaSelecionada;

        public FrmClienteCadastrar(AcaoNaTela acaoNaTela, Cliente cliente)
        {
            InitializeComponent();

            this.acaoNaTelaSelecionada = acaoNaTela;

            if (acaoNaTela == AcaoNaTela.Inserir)
            {
                this.Text = "Inserir Cliente";

            }
            else if (acaoNaTela == AcaoNaTela.Alterar)
            {
                this.Text = "Alterar Cliente";

                ClienteSelecionadoGrid(cliente);


            }
            else if (acaoNaTela == AcaoNaTela.Consultar)
            {
                this.Text = "Consultar Cliente";

                ClienteSelecionadoGrid(cliente);

                //desabilitar acesso da tela.

                txtNome.ReadOnly = true;
                txtNome.TabStop = false;  
                dtpDataNascimento.Enabled = false;
                rbMasculino.Enabled = false;
                rbFeminino.Enabled = false;
                txtLimiteCompra.ReadOnly = true;
                txtLimiteCompra.TabStop = false; 

                //alterando configuração de botões

                btnSalvar.Visible = false;
                btnCancelar.Text = "Fechar";
                btnCancelar.Focus();

            }
        }

        private void ClienteSelecionadoGrid(Cliente clientes)
        {
            this.txtCodigo.Text = clientes.idCliente.ToString();
            this.txtNome.Text = clientes.Nome.ToString();
            this.txtLimiteCompra.Text = clientes.LimiteCompra.ToString();
            dtpDataNascimento.Value = clientes.DataNascimento;

            if (clientes.Sexo == true)
                rbMasculino.Checked = true;
            else
                rbFeminino.Checked = true;
        }



        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acaoNaTelaSelecionada == AcaoNaTela.Inserir)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = txtNome.Text;
                cliente.DataNascimento = dtpDataNascimento.Value;

                if (rbMasculino.Checked)
                    cliente.Sexo = true;
                else
                    cliente.Sexo = false;

                    cliente.LimiteCompra = Convert.ToDecimal(txtLimiteCompra.Text);

                ClienteNegocio clienteNegocio = new ClienteNegocio();

                string retorno = clienteNegocio.Inserir(cliente);

                // tenta converte para inteiro
                // se der tudo certo é porque devolveu o código do cliente 
                // se der errado tem a mensagem de erro

                try
                {
                    int idCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente Inserido com sucesso. Código: " + idCliente.ToString());

                    this.DialogResult = DialogResult.Yes;  // Fecha a tela com resposta SIM.

                }
                catch
                {
                    MessageBox.Show("Nao foi possivel Inserir. Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;  // Fecha a tela com resposta NAO.
                }

            }
            else if (acaoNaTelaSelecionada == AcaoNaTela.Alterar)
            {
                Cliente cliente = new Cliente();

                cliente.idCliente = Convert.ToInt32(txtCodigo.Text);
                cliente.Nome = txtNome.Text;
                cliente.DataNascimento = dtpDataNascimento.Value;

                if (rbMasculino.Checked)
                    cliente.Sexo = true;
                else
                    cliente.Sexo = false;

                cliente.LimiteCompra = Convert.ToDecimal(txtLimiteCompra.Text);

                ClienteNegocio clienteNegocio = new ClienteNegocio();

                string retorno = clienteNegocio.Alterar(cliente);

                try
                {
                    int idCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente Alterado com sucesso. Código: " + idCliente.ToString());

                    this.DialogResult = DialogResult.Yes;  // Fecha a tela com resposta SIM.

                }
                catch
                {
                    MessageBox.Show("Nao foi possivel Alterar. Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;  // Fecha a tela com resposta NAO.
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
