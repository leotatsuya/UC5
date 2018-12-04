using System;
using System.Collections.Generic;
using System.Text;

namespace MariooLikeGame.model
{
     public class Placar
    {
        //boa pratica de programação  camel case (PRImeira palavra minusculo e as demais palavras maiusculo)
        private int idJogador;
        private string nomeJogador;
        private int scoreJogador;
        private DateTime dataScoreJogador;
        private string tempoJogador;

        //para criar uma conexao com o projeto
        public Placar()
        {

        }

        public Placar(int idJogador, string nomeJogador, int scoreJogador, DateTime dataScoreJogador, string tempoJogador)
        {
            IdJogador = idJogador;
            NomeJogador = nomeJogador;
            ScoreJogador = scoreJogador;
            DataScoreJogador = dataScoreJogador;
            TempoJogador = tempoJogador;
            
        }
        //geters and seters
        public int IdJogador { get => idJogador; set => idJogador = value; }
        public string NomeJogador { get => nomeJogador; set => nomeJogador = value; }
        public int ScoreJogador { get => scoreJogador; set => scoreJogador = value; }
        public DateTime DataScoreJogador { get => dataScoreJogador; set => dataScoreJogador = value; }
        public string TempoJogador { get => tempoJogador; set => tempoJogador = value; }


        //private Placar(int Id_Jogador,string Nome_Jogador,int Score_Jogador,DateTime Data_Score_Jogador)
        //{
        //    this.Idjogador = Id_Jogador;
        //    this.nomejogador = nomejogador;
        //    this.Scorejogador = Scorejogador;
        //    this.Datascorejogador = Datascorejogador;
        //}
        ////Get e Setss
        //public int Idjogador { get => Idjogador; set => Idjogador = value; }
        //public string nomejogador { get => nomejogador; set => nomejogador = value; }
        //public int Scorejogador { get => Scorejogador; set => Scorejogador = value; }
        //public DateTime Datascorejogador { get => Datascorejogador; set => Datascorejogador = value; }
    }
}
