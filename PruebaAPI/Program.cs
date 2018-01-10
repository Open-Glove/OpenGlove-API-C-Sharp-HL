using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGlove_API_C_Sharp_HL;
using OpenGlove_API_C_Sharp_HL.OGServiceReference;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
       static Glove guante;
        static List<Glove> gloves;
         public static OpenGloveAPI api = OpenGloveAPI.GetInstance();
        static int ciclos_prueba;
        static int contador;

        static Semaphore semaphoreObject = new Semaphore(initialCount: 1, maximumCount: 1);

        static StreamWriter archivo = File.CreateText(@"D:\pruebas\csharp\X10flexor.txt");
        // codigo a medir


        int[] regiones;
        
        static int test = 0;
        static int pruebas = 1;
        static TimeSpan stop, start;
        static Stopwatch sw = new Stopwatch();

        static void testFingers(int index, int value)
        {

            if (index == 0 && pruebas <= 1100)
            {
                if (test == 0)
                {
                    // semaphoreObject.WaitOne();
                    sw.Reset();
                    sw.Start();
                    //start = new TimeSpan(DateTime.Now.Ticks);
                    test = 1;
                }
                else
                {
                    sw.Stop();
                    if (pruebas > 100)
                    {
                        archivo.WriteLine(1000 * sw.Elapsed.TotalMilliseconds);
                    }
                   
                    //   stop = new TimeSpan(DateTime.Now.Ticks);
                    //semaphoreObject.Release();
                   // archivo.WriteLine((stop.Subtract(start).TotalMilliseconds));
                    
                    pruebas++;
                    test = 0;
                }

            }
            else
            {
                if (pruebas == 1101)
                {
                    archivo.Close();
                    Console.WriteLine("LISTO");
                    pruebas++;
                }
            }
        }


        public static void Main(string[] args)
        {
            try {
                gloves = api.Devices;
            }catch
            {
                Console.WriteLine("ERROR: El servicio no esta activo");
                Console.ReadKey();
                return;
            }
            
            guante = gloves[3];
            Console.WriteLine("Guante "+guante.Port);
            if(guante.GloveConfiguration == null )
            {
                Console.WriteLine("No Config");
            }else
            {
                Console.WriteLine("Ready!");
                ciclos_prueba = 0;
                contador = 0;
                api.fingersFunction += testFingers;
                api.startCaptureData(guante);
   
            }
            Console.ReadKey();

        }
    }
}
