using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;

namespace MarioLikeGame.DAL
{
    class LeitorConfiguracao
    {
        //vai ler a conexao com dados
     public string lerconexao()
        {
            string resultado = "";

            //ler a string conexao

            resultado = ConfigurationManager.ConnectionStrings
                ["game mario.Properties.Settings.Conexao"].ConnectionString;
            return resultado;

        }
       
        

    }
    


}
