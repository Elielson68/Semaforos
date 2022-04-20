using System;
using System.Threading;
namespace Tabacaria
{
    class TabacariaApp
    {
        static Semaphore TabacoSem = new Semaphore(1,1);
        static Semaphore PapelSem = new Semaphore(1,1);
        static Semaphore FosforoSem = new Semaphore(1,1);

        static bool TabacoBool = false;
        static bool PapelBool = false;
        static bool FosforoBool = false;

        public static void StartTabacaria()
        {
            Console.WriteLine("A Tabacaria se inicia com os 3 fumantes: Carlos, Rafael e Miguel\nCarlos possui 1 tabaco\nRafael possui 1 papel\nMiguel possui 1 fósforo");
            Thread Carlos = new Thread(new ThreadStart(() => Fumante(true, false, false, "Carlos")));
            Thread Rafael = new Thread(new ThreadStart(() => Fumante(false, true, false, "Rafael")));
            Thread Miguel = new Thread(new ThreadStart(() => Fumante(false, false, true, "Miguel")));
            
            Carlos.Start();
            Rafael.Start();
            Miguel.Start();
            Vendedor();
        }

        static void Vendedor()
        {
            int tabacoInt = new Random().Next(2);
            int papelInt = new Random().Next(2);
            if(TabacoBool)
            {
                TabacoBool = false;
            }
                
            if(PapelBool)
            {
                PapelBool = false;
            }
                
            if(FosforoBool)
            {
                FosforoBool = false;
            }
                
            if(tabacoInt==1 && papelInt==1)
            {
                Console.WriteLine($"Vendedor: Acordei! e vou liberar agora: Tabaco e Papel");
                TabacoBool = true;
                PapelBool = true;
                return;
            }
            if(tabacoInt==0 || papelInt == 0)
            {
                if(tabacoInt==0 && papelInt == 0)
                {
                    if(new Random().Next(2) == 1)
                    {
                        TabacoBool = true;
                    }
                    else
                    {
                        PapelBool = true;
                    }
                }
                else
                {
                    if(tabacoInt == 1)
                    {
                        TabacoBool = true;
                    }
                    else
                    {
                        PapelBool = true;
                    }
                }
                FosforoBool = true;
            }
            Console.WriteLine($"Vendedor: Acordei! e vou liberar agora: Fosforo e {(TabacoBool ? "Tabaco ":"")}{(PapelBool ? "Papel ":"")}");

        }

        static void Fumante(bool tabaco, bool papel, bool fosforo, string nome)
        {
            bool tab = tabaco;
            bool  pap = papel;
            bool fos = fosforo;
            while(true)
            {
                if((tab && PapelBool && FosforoBool) || (pap && TabacoBool && FosforoBool) || (fos && PapelBool && TabacoBool))
                {
                    TabacoSem.WaitOne();
                    PapelSem.WaitOne();
                    FosforoSem.WaitOne();
                    Console.WriteLine($"Eu {nome} to fumando!");
                    Thread.Sleep(5000);
                    Vendedor();
                    TabacoSem.Release();
                    PapelSem.Release();
                    FosforoSem.Release();
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }

}
