using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MariooLikeGame.model;


namespace MarioLikeGame.DAL
{
    public class GamerDAL
    {
        //declara o objeto de conexao com o banco de dados
        private SqlConnection conexao;

        //exiba a mensagem de erro
        public string MensagemErro { get; set; }

        public GamerDAL()
        {
            //criar o objeto para ler a configuração 
            LeitorConfiguracao leitor = new LeitorConfiguracao();

            //instancia a conexao
            conexao = new SqlConnection();
            conexao.ConnectionString = leitor.lerconexao();

           
        }
        public bool Inserir(Placar placar)
        {
            bool resultado = false;
            //limpa mesagem erro
            MensagemErro = "";
            //declarar comando SQL
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "INSERT INTO jogador(Nome_Jogador , Score_Jogador , Data_Score_Jogador , Tempo_Jogador ) " +
                "VALUES (@Nome,@Score,@Data,@Tempo) ";

            //criar os parametros
            comando.Parameters.AddWithValue("@Nome", placar.NomeJogador);
            comando.Parameters.AddWithValue("@Score", placar.ScoreJogador);
            comando.Parameters.AddWithValue("@Data", placar.DataScoreJogador);
            comando.Parameters.AddWithValue("@Tempo", placar.TempoJogador);

            //executar o comando
            try
            {
                //abri conexao
                conexao.Open();
                //excutar o comanda
                comando.ExecuteNonQuery();
                //se chegoa te aqui funcionaaa
                resultado = true;

            }
            catch (Exception ex)
            {
                //se entro aqui deu ruim nessa poha
                MensagemErro = ex.Message;
               
            }
            finally
            {
                //finalizo a conexao
                conexao.Close();
            }
            return resultado;

        }

        public List<Placar> Listar()
        {
            //instanncia a lista
            List<Placar> resultado = new List<Placar>();

            //declarar o comando

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = " SELECT TOP 10 Id_Jogador,Nome_Jogador,Score_Jogador,Data_Score_Jogador,Tempo_Jogador" + 
                " FROM jogador ORDER BY Score_Jogador desc , Tempo_Jogador , Data_Score_Jogador ";
            //DUVIDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            //, select min(Tempo_Jogador) from jogador
            //executa os comandos

            try
            {
                //abrir a conexao
                conexao.Open();

                //executa o comando e receber o resultado
                SqlDataReader leitor = comando.ExecuteReader();

                //verifica se encontrou algo
                while (leitor.Read() == true )
                {
                    //instancia o objeto
                    Placar placar = new Placar();
                    placar.IdJogador = Convert.ToInt32(leitor["Id_Jogador"]);
                    placar.NomeJogador = leitor["Nome_Jogador"].ToString();
                    placar.ScoreJogador = Convert.ToInt32(leitor["Score_Jogador"]);
                    placar.DataScoreJogador = Convert.ToDateTime(leitor["Data_Score_Jogador"]);
                    placar.TempoJogador = leitor["Tempo_Jogador"].ToString();

                    //adiciona na lsita

                    resultado.Add(placar);

                }

                //fechar o leitor
                leitor.Close();

            }
            catch (Exception ex )
            {
                //Se entrou aqui, então fudeu
                string mensagem = ex.Message;
            }
            finally
            {
                //finaliza a conexao
                conexao.Close();
            }
            return resultado;





        }

        
                


    }
}
