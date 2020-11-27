using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
                if (nPort <= 0 || nPort >= 65535)
                {
                    Console.WriteLine("Porta non valida");
                    return;
                }

                Console.WriteLine("Endpint del server: " + ipAddress.ToString() + " " + nPort);

                client.Connect(ipAddress, nPort);

                // Server
                byte[] sendbuffer = new byte[128];
                byte[] recvbuffer = new byte[128];
                string sendString = "";
                string receiveString = "";
                int receivedBytes = 0;

                while (true)
                {
                    Console.WriteLine("Manda un messaggio: ");

                    sendString = Console.ReadLine();

                    sendbuffer = Encoding.ASCII.GetBytes(sendString);

                    client.Send(sendbuffer);

                    if (sendString.ToUpper().Trim() == "QUIT")
                    {
                        break;
                    }

                    Array.Clear(recvbuffer, 0, recvbuffer.Length);
                    
                    receivedBytes = client.Receive(recvbuffer);

                    receiveString = Encoding.ASCII.GetString(recvbuffer, 0, receivedBytes);

                    Console.WriteLine("S: " + receiveString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                /*In ogni sessione chiudo la connessione in sicurezza*/
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both); // disabilita la send e receive
                    }
                    client.Close();
                    client.Dispose();
                }
            }
        }
    }
}
