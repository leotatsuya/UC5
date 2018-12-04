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
using MariooLikeGame.model;

namespace game_mario
{
    public partial class FrmTelaJogo : Form
    {
        public FrmTelaJogo()
        {
            InitializeComponent();
        }

        //ATRIBUTOS DE MOVIMENTAÇÃO DO PERSONAGEM
        private bool paracima;
        private bool parabaixo;
        private bool paraesquerda;
        private bool paradireita;
        //VARIAVEIS PARA CONDIÇÃO DE VITORIA OU DERROTA

        private bool vitoria = false;

        //Declarando A dal <<<<
        GamerDAL gamerDAL;

        //criar um atibuto para pegar o nome do jogadofrooooooooooooooooooooo
        public string nomeGmaer { get; set; }
        
        private int pontos = 0;
        //ATRIBUTO DA LOCOMOÇÃO DO PERSONAGEM
        private int velocidade = 11;

        //CRIAR LISTA DE MUSICA
        List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();


        //CHAMAR A BIBLIOTECA (SOOOOOOOOOOONS) <<
        //referencia + COM + procura media player

            //WMPLib.WindowsMediaPlayer Tocador = new WMPLib.WindowsMediaPlayer();

        //TIMER
        private int segundos = 0;
        private int minutos = 0;


        //MOVIMENTAR O PERSONAGEM QUNDO CLICAR NA TECLA
        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraesquerda = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                paradireita = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                paracima = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                parabaixo = true;
            }
        }
        //PARAR O MOVIMENTAR DO PERSONAMGE EM CADA TECLA
        private void KeyisUp(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Left:
            //        paraesquerda = false;
            //     break;
            //}
            if (e.KeyCode == Keys.Left)
            {
                paraesquerda = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                paradireita = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                paracima = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                parabaixo = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            //OU USA DOIS LEFT OU DOIS RIGHT TIRAR E COLOCAR VALOR
            //MOVIMENTA O PERSONAGEM PARA ESQUERDA
            if (paraesquerda)
            {
                personagem.Left -= velocidade;

            } //MOVIMENTA O PERSONAGEM PARA DIREITA
            if (paradireita)
            {
                personagem.Left += velocidade;
            } //MOVIMENTA O PERSONAGEM PARA cima
            if (paracima)
            {
                personagem.Top -= velocidade;
            } //MOVIMENTA O PERSONAGEM PARA baixo
            if (parabaixo)
            {
                personagem.Top += velocidade;
            }
            if (personagem.Left < 0)
            {
                personagem.Left = 0;
            }
            if (personagem.Left > 1090)
            {
                personagem.Left = 1090;
            }
            if (personagem.Top < 69)
            {
                personagem.Top = 69;
            }
            if (personagem.Top > 680)
            {
                personagem.Top = 680;
            }

            //LOOP PARA CHECAR TODOS OS COMPONENTES INSERIDOS NO FORM
            foreach (Control item in this.Controls)
            {
                //VERIFICA SE O JOGADOR COLIDIO COM O INIMIGO,CASO SIM GAME OVER (UTILIZANDO A TAG DELE)
                if (item is PictureBox && (string)item.Tag == "inimigo")
                {
                    //CHECA A COLISAO COM A PICTUREBOX
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        GravaHiscore();
                        vitoria = false;
                        Gameover(vitoria);
                        limpa();
                       
                    }
                } //SE TOCAR NO COLETAVEL DESTRUA
                if (item is PictureBox && (string)item.Tag == "coletavel" ||  (string)item.Tag == "cogumelo")
                {
                    

                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        if ((string)item.Tag == "coletavel")
                        {
                            playsound("smb_coin.wav");

                        }
                        else
                        {

                            playsound("smb_1-up.wav");
                            personagem.Width += 14;
                            personagem.Height += 14;


                        }
                        //remove o item coletavel
                        this.Controls.Remove(item);

                        //AUMENTAR PONTOS
                        pontos++;

                        //AUDIO NO ITEM
                        //Audio("smb_coin.wav", "Play");
                        

                        if (pontos == 15)
                        {
                            
                            vitoria = true;
                            Gameover(vitoria);
                            limpa();
                            GravaHiscore();
                        }



                    }
                }
            }
       
            LblPontos.Text = "Pontos: " + pontos;


        }
      

        //limpa tela no game over ou vitoria
        private void limpa()
        {
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && (string)item.Tag != "Gameover")
                {
                    
                    ((PictureBox)item).Image = null;


                }

            }

        }
        //remove 
        private void winner(bool ganhou)
        {
            
            BtmRestart.Visible = true;
            BtmRestart.Focus();


        }
        private void GravaHiscore()
        {
            //isntancia DAL
            gamerDAL = new GamerDAL();

            Placar placar = new Placar();

            var frm = new frmTelaInicial();

            // ! < Senao [negando]
            if (!this.nomeGmaer.Equals(""))
            {
                placar.NomeJogador = this.nomeGmaer;
            }
            else
            {
                placar.NomeJogador = "Esqueceu o Nome";
;            }
            placar.ScoreJogador = pontos;
            placar.DataScoreJogador = DateTime.Now;
            placar.TempoJogador = minutos.ToString("00") + ":" + segundos.ToString("00"); 

            //chama a função inserir da DAL passando o objeto populado como parametro
            if (!gamerDAL.Inserir(placar))
            {
                //Deu pauu Exibe mensagem pro usuario .... CRY
                MessageBox.Show("Deu erro nos Dados :( \r\n\r\n"+
                    gamerDAL.MensagemErro,"Mario Like Game");

            }
           

        }
        //CONDIÇÃO DERROTA
        private void Gameover(bool ganhou)
        {
          
            
            personagem.Visible = false;
            BtmRestart.Visible = true;
            BtmRestart.Focus();
            pictureBox37.Visible = true;
            pictureBox38.Visible = true;
            
            if (ganhou)
            {
               
                stopsound();
                pictureBox25.Visible = true;
                pictureBox32.Visible = true;
                pictureBox34.Visible = true;
                pictureBox33.Visible = false;
                panel1.Visible = false;


                playsound("smb_world_clear.wav");

            }
            else
            {
                stopsound();
                pictureBox26.Visible = true;
                pictureBox31.Visible = true;
                pictureBox33.Visible = false;
                panel1.Visible = false;
                pictureBox35.Visible = true;


                playsound("smb_gameover.wav");

            }

            timer1.Stop();
            timer2.Stop();


        }
        // CONTADOR DE TEMPO
        private void timer2_Tick(object sender, EventArgs e)
        {

            segundos++;


            if (segundos == 60)
            {
                minutos++;
                segundos = 0;

            }
            //segundos = segundos % 60;  outra maneira em vez de zerar
            LblTempo.Text = "Tempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
        }
        //função do botam iniciar

        private void BtmRestart_Click(object sender, EventArgs e)
        {
            BtmRestart.Text = "Voltar ao Menu";
            Application.Restart();
            
        }
        //para usar apenas uma musica
        //MUSICAAAAAAA  <<<<<<<<<<<<
        //public void Audio(string caminho,string estadoMP)
        // {
        //     //VERICAR SE OCORREU ERROS AO INSTANCIAR O WINDONWS MEDIA PLAYER
        //     Tocador.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Tocador_MediaError);

        //     //Pega o arquivo e toca aqui
        //     Tocador.URL = caminho;


        //     if (estadoMP.Equals("play") )
        //     {
        //         Tocador.controls.play();

        //     }
        //     else if (estadoMP.Equals("Stop"))
        //     {
        //         Tocador.controls.stop();

        //     }

        // }
        //public void Tocador_MediaError(object pMediaObject)
        // {
        //     MessageBox.Show("DEU ERRO ESSA POHA");
        //     this.Close();
        // }

        //    public void FrmTelaJogo_Load(object sender, EventArgs e)
        //    {
        //        Audio("18 overworld bgm.mp3", "Play");
        //    }

        
        private void playsound(string nome)
        //para colocar varias musicas de efeito 
        {
            string url = Application.StartupPath + @"\" + nome;
            var sound = new System.Windows.Media.MediaPlayer();
            sound.Open(new Uri(url));
            sound.Play();
            sounds.Add(sound);


        }
        private void stopsound()
        {
            for (int i = sounds.Count - 1; i >= 0 ; i--)
            {
                sounds[i].Stop();
                //removeat remove apenas a instancia de I
                sounds.RemoveAt(i);
            }

        }

        private void FrmTelaJogo_Load(object sender, EventArgs e)
        {
             playsound("18 overworld bgm.mp3");
        }

        private void Maximizar()
        {
            Screen tela = Screen.FromPoint(this.Location);
            this.Size = tela.WorkingArea.Size;
            this.Location = Point.Empty;
        }

    }
}
