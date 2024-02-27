using SocketGameServer.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SocketGameServer.Client;

namespace SocketGameServer.Server
{
	public class Server
	{
		public static int MaxPlayer { get; set; }
		
		public static int Port{ get; set; }

		public static Dictionary<int, Client.Client> clients = new Dictionary<int, Client.Client>();

		private static TcpListener _tcpListener;
		
		public Server() { }	

		public static void Start(int maxPlayers,int port) {

			MaxPlayer = maxPlayers;
			Port = port;
			Console.WriteLine("Starting Server...");
			InitializeServerData();
			_tcpListener = new TcpListener(IPAddress.Any, Port);
			_tcpListener.Start();
			_tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
			Console.WriteLine($"Server Started on {Port}");
		}

		private static void TCPConnectCallback(IAsyncResult result)
		{
			TcpClient client = _tcpListener.EndAcceptTcpClient(result);
			_tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback),null);
			Console.WriteLine($"Incomming connection from {client.Client.RemoteEndPoint}");

			for (int i = 1; i <= clients.Count; i++) {
				if (clients[i].tcp.socket == null)
				{
					clients[i].tcp.Connect(client);
					return;
				}
			}

			Console.WriteLine($"{client.Client.RemoteEndPoint} failed to connect : Server Full");
		}

		public static void InitializeServerData()
		{
			for(int i = 1; i <= MaxPlayer; i++)
			{
				clients.Add(i,new Client.Client(i));
			}
		}
	}
}
