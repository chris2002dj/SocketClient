using System;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string strIpAddress = "", strPort = ""; // Leggono da tastiera
            IPAddress ipAddress = null;
            int nPort;

            try
            {
                Console.WriteLine("IP del server: ");
                strIpAddress = Console.ReadLine();

                Console.WriteLine("Porta del server: ");
                strPort = Console.ReadLine();

                /*(TryParse)Prova a copiare lstringa alla variabile "ipAddress"*/
                if (!IPAddress.TryParse(strIpAddress.Trim(), out ipAddress)) // True se ci riesce
                {
                    Console.WriteLine("IP non valido.");
                    return;
                }
                if (!int.TryParse(strPort, out nPort))
                {
                    Console.WriteLine("Porta non valida");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
