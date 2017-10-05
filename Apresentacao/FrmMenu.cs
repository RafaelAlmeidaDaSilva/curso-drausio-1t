using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void MenuSair_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(sender.ToString());     //ORIGEM DO EVENTO   Ex: "&Sair"
            //MessageBox.Show(e.ToString());         // CONTEM INFORMAÇÕES SOBRE O TIPO DE EVENTO OCORRIDO Ex:"System.EventArgs"
           
            // Fecha a Aplicação 
            Application.Exit();

        }

        private void MenuCadastro_Click(object sender, EventArgs e)
        {


        }

        private void ItemCliente_Click(object sender, EventArgs e)
        {
            FrmClienteSelecionar frm = new FrmClienteSelecionar();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
