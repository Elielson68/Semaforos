using System;
using System.Threading;
using Restaurante;
using Tabacaria;
namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Escolha qual deseja rodar:\n1 - para Tabacaria\n2 - para Restaurante\nResposta: ");
            string resposta = Console.ReadLine();
            switch(resposta)
            {
                case "1":
                    Tabacaria.TabacariaApp.StartTabacaria();
                break;
                case "2":
                    Restaurante.RestauranteApp.StartRestaurante();
                break;
            }
        }

        

    }

}
