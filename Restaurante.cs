using System;
using System.Diagnostics;
using System.Threading;
namespace Restaurante
{
    class RestauranteApp
    {
        static Semaphore BuffetSem = new Semaphore(10,10);
        static Semaphore MessasSem = new Semaphore(48,48);
        static Semaphore FilaEspera = new Semaphore(12,12);
        static Semaphore RestauranteSem = new Semaphore(70,70);

        public static void StartRestaurante()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("A Tabacaria se inicia com os 3 fumantes: Carlos, Rafael e Miguel\nCarlos possui 1 tabaco\nRafael possui 1 papel\nMiguel possui 1 fósforo");
            for(int t=0; t<500; t++)
            {
                Thread cliente = new Thread(new ThreadStart(() => Cliente($"Thread {t}")));
                cliente.Start();
            }
            stopwatch.Stop();
            Console.WriteLine($"Tempo de execução: {stopwatch.Elapsed}"); 
        }
        /*
        
                Uma pessoa leva de 25 a 30 unidades de tempo para comer a salada e o prato
            principal e 5 a 6 unidades de tempo para comer a sobremesa. O tempo que uma
            pessoa gasta para se servir é de 4 a 6 unidades de tempo. Crie 500 threads que
            representem os clientes. A cada movimentação de um cliente, imprima uma
            mensagem na tela.
        */
        public static void Cliente(string name)
        {
            FilaEspera.WaitOne();
            Console.WriteLine($"{name} - Entrei na fila");
            Thread.Sleep(10);
            
            RestauranteSem.WaitOne();
            Console.WriteLine($"{name} - Entrei no restaurante");
            Console.WriteLine($"{name} - Sai da fila");
            FilaEspera.Release();
            Thread.Sleep(10);

            MessasSem.WaitOne();
            Console.WriteLine($"{name} - Sentei na mesa");
            Console.WriteLine($"{name} - Estou me servindo");
            Thread.Sleep(new Random().Next(4, 7));

            Console.WriteLine($"{name} - Estou comendo a salada e o prato principal");
            Thread.Sleep(new Random().Next(25, 31));

            Console.WriteLine($"{name} - Estou me servindo");
            Thread.Sleep(new Random().Next(4, 7));

            Console.WriteLine($"{name} - Estou comendo a sobremesa");
            Thread.Sleep(new Random().Next(5, 7));

            Console.WriteLine($"{name} - Estou saindo da mesa");
            Thread.Sleep(new Random().Next(5, 10));
            MessasSem.Release();

            Console.WriteLine($"{name} - Estou saindo do restaurante");
            Thread.Sleep(new Random().Next(5, 10));
            RestauranteSem.Release();
        }
    }

}
