using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarioLikeGame.DAL;
//colocar o using para poder usar nesse form ( deixar a class publica
namespace game_mario
{
    public partial class frmTelaInicial : Form
    {
        

        //private void Maximizar()
        //{
        //    Screen tela = Screen.FromPoint(this.Location);
        //    this.Size = tela.WorkingArea.Size;
        //    this.Location = Point.Empty;
        //}

        public frmTelaInicial()
        {
            
            InitializeComponent();
            //deixar maximizado
            this.WindowState = FormWindowState.Maximized;
            txtNome.MaxLength = 49;

            
        }

      

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            //FrmTelaJogo Chama = new FrmTelaJogo();
            //this.Hide(); 
            //Chama.Show();
            //COLOCA O NOME DO JOGADOR AQUI POHAAAAAAAAA NO NOME GAMER 
            //FRM.NOME GAMER = TXTNOME.TEXT;
            //foreach (Control nome in this.Controls)
            //{

            if (string.IsNullOrWhiteSpace(txtNome.Text)/*txtNome.Text == ""*/ )
                {
                    MessageBox.Show("Por favor Digite um nome para começar","MARIO LIKE GAME");

                }
                else
                {
                    this.Visible = false;
                    var frm = new FrmTelaJogo();
                    frm.nomeGmaer = txtNome.Text;
                    frm.ShowDialog();
                    //preencherGrid();
                    //this.Visible = true;
                    //btnIniciar.Focus();
                }

            

        }

     

        private void btnSair_Click_1(object sender, EventArgs e)
        {
               this.Close();
        }
        private void frmTelaInicial_Load_1(object sender, EventArgs e)
        {
            //SETA O FOCO PARA O TEXTO
            txtNome.Focus();
            txtNome.Select();

            //Preeencher o grid do Score
            preencherGrid();
            dgvListaRecorde.ReadOnly = true;

        }

        private void preencherGrid()
        {

           
            //Declarando a dal
            GamerDAL gamerDal;

            //isntanciar a dal na ocntrução od formulario
            gamerDal = new GamerDAL();

            //limpando o datasourse

            dgvListaRecorde.DataSource = null;

            //listando a DAL
            dgvListaRecorde.DataSource = gamerDal.Listar();

            //removendo a coluna que nao tmem nessecidade ( ID jogador)

            dgvListaRecorde.Columns.Remove("IdJogador");


            //colocando cor
            Mudafonte();

            

        }
        private void Mudafonte()
        {
            dgvListaRecorde.Columns[0].HeaderText = "Jogador";
            dgvListaRecorde.Columns[1].HeaderText = "Pontos";
            dgvListaRecorde.Columns[1].Width = 80;
            dgvListaRecorde.Columns[2].HeaderText = "Data/Hora";
            dgvListaRecorde.Columns[2].Width = 220;
            dgvListaRecorde.Columns[3].HeaderText = "Tempo";
            dgvListaRecorde.Columns[3].Width = 200;


            dgvListaRecorde.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Font = new Font("Tekton Pro", 30, FontStyle.Regular);
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaRecorde.EnableHeadersVisualStyles = false;
            dgvListaRecorde.CurrentRow.DefaultCellStyle.BackColor = Color.OrangeRed;
            dgvListaRecorde.DefaultCellStyle.Font = new Font("Tekton Pro",30, FontStyle.Regular );
            dgvListaRecorde.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dgvListaRecorde.DefaultCellStyle.BackColor = Color.Blue;
            dgvListaRecorde.DefaultCellStyle.ForeColor = Color.White;
            
            
        }

    }
}
