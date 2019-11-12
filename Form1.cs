using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeracaoDeArquivoDeFolhaDePagamento
{
    public partial class frmGeracaoArquivoTexto : Form
    {
        public frmGeracaoArquivoTexto()
        {
            InitializeComponent();

            //cabeçalho da tabela do DataGridView
            dgvFuncionarios.ColumnCount = 2; //numero de colunas
            dgvFuncionarios.Columns[0].HeaderText = "Nome"; //cabeçalho da primeira coluna
            dgvFuncionarios.Columns[0].Width = 300; //tamanho do espaço da primeira coluna
            dgvFuncionarios.Columns[1].HeaderText = "Salário"; //cabeçaçho da segunda coluna
            dgvFuncionarios.Columns[0].Width = 90; //tamanho do espaço da segunda coluna
        }

        private void BtnCriarLinhas_Click(object sender, EventArgs e)
        {
            int numeroFuncionarios = Convert.ToInt16(txtNumeroFuncionarios.Text); //recebe o numero de funcionarios informado no TextBox
            //caso não informar um valor padrão será 1 (um)
            if (numeroFuncionarios < 1)
            {
                numeroFuncionarios = 1;
            }

            //inicializa as celulas com um campo vazio e a outra com o valor 0 (zero)
            int i =0;
            do
            {
                var linhaTabela = new DataGridViewRow();
                linhaTabela.Cells.Add(new DataGridViewTextBoxCell { Value = string.Empty }); //primeira celula da linha um texto vazio
                linhaTabela.Cells.Add(new DataGridViewTextBoxCell { Value = 0 }); //segunda celula da linha vslor zero (vazio)
                dgvFuncionarios.Rows.Add(linhaTabela); //lista de linhas
            }
            while (++i < numeroFuncionarios);

            //chama o habilita e desabilita componente
            crt_componentes();
        }

        private void crt_componentes()
        {
            //habilita e desabilita os componentes
            txtNumeroFuncionarios.Enabled = !(txtNumeroFuncionarios.Enabled);
            btnCriarArquivo.Enabled = !(btnCriarArquivo.Enabled);
            btnCriarLinhas.Enabled = !(btnCriarLinhas.Enabled);
            btnReiniciar.Enabled = !(btnCriarLinhas.Enabled);
        }

        private void BtnReiniciar_Click(object sender, EventArgs e)
        {
            //limpar os componentes e inicializar

            dgvFuncionarios.Rows.Clear();
            txtNumeroFuncionarios.Text = string.Empty;
            crt_componentes();
        }

        private void BtnCriarArquivo_Click(object sender, EventArgs e)
        {
            if (!ValidarDados())
            {
                MessageBox.Show("Osdados possuem problemas. Velrifique se não deixou nenhum dado em branco ou  incorreto");
            }
            else
            {
                if (sfdGravarArquivo.ShowDialog() == DialogResult.OK)
                {
                    GerarArquivo();
                    MessageBox.Show("Arquivo gerado com sucesso!");
                }
            }
           
        }
        private bool ValidarDados()
        {
            int i = 0;
            bool dadosValidados = true;
            double resultado;
            do
            {
                if (String.IsNullOrWhiteSpace(dgvFuncionarios.Rows[i].Cells[0].Value.ToString())) //verifica se há algum espaço em branco na primeira celula de cada linha
                {
                    dadosValidados = false;
                    break;
                }
                if (!Double.TryParse(dgvFuncionarios.Rows[i].Cells[1].Value.ToString(), out resultado)) //verifica se há algum espaço em branco na segunda celula de cada linha
                {
                    dadosValidados = false;
                    break;
                }
            }
            while (++i < dgvFuncionarios.Rows.Count);
            
            return dadosValidados;
        }
        private void GerarArquivo()
        {
            StreamWriter wr = new StreamWriter(sfdGravarArquivo.FileName, true);
            for (int i = 0; i < dgvFuncionarios.Rows.Count - 1; i++)
            {
                wr.WriteLine(dgvFuncionarios.Rows[i].Cells[0].Value.ToString() + ";" + dgvFuncionarios.Rows[i].Cells[1].Value.ToString());
            }

            wr.Close();
        }
    }
}
