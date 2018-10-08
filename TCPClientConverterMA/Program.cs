using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TCPClientConverterMA
{
    class Program
    {
        private static TcpClient _clientSocket = null;
        private static Stream _nstream = null;
        private static StreamWriter _sWriter = null;
        private static StreamReader _sReader = null;

        static void Main(string[] args)
        {
            try
            {
                using (_clientSocket = new TcpClient("127.0.0.1", 7777))
                {
                    while (_clientSocket != null)
                    {
                        using (_nstream = _clientSocket.GetStream())
                        {

                            while (_nstream != null)
                            {
                                _sWriter = new StreamWriter(_nstream) { AutoFlush = true };

                                Console.WriteLine("Client ready to send bytes of data to server...");
                                Console.WriteLine("Kindly write If you wants grams or pounds ");

                                string selection = Console.ReadLine();
                                                                
                                if (selection == "grams")
                                {
                                    Console.WriteLine("Please write your grams value here and press enter");

                                    string clientValue = Console.ReadLine();
                                    string stringConcat = String.Concat("g=", clientValue);
                                    _sWriter.WriteLine(stringConcat);

                                }
                                else if (selection == "pounds")
                                {
                                    Console.WriteLine("Please write your pounds value here and press enter");
                                    string clientValue = Console.ReadLine();
                                    string stringConcat = String.Concat("o=", clientValue);
                                    _sWriter.WriteLine(stringConcat);
                                }
                                else
                                {
                                    Console.WriteLine("Please write grams or pounds");
                                }                                 
                                                       
                                _sReader = new StreamReader(_nstream);

                                string rdMsgFromServer = _sReader.ReadLine();

                                if (rdMsgFromServer != null)
                                {
                                    Console.WriteLine(".....................................................");
                                    Console.WriteLine("Client recieved Message from Server:" + rdMsgFromServer);
                                    Console.WriteLine(".....................................................");
                                }
                                else
                                {
                                    Console.WriteLine("Client recieved null message from Server ");
                                }
                            }

                        }
                    }

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
