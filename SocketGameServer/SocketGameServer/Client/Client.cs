using SocketGameServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketGameServer.Client
{
	public  class Client
	{
		public static int DATA_BUFFER_SIZE = 4096;
		public int clientId;
		public TCP tcp;
		public Client(int clientId)
		{
			this.clientId = clientId;
			tcp = new TCP(clientId);
		}
		public class TCP
		{
			public TcpClient socket;
			public Packet buffer;
			private readonly int _id;
			private byte[] _receiveBuffer;
			private NetworkStream _stream;
			public TCP(int id) 
			{
				_id = id;
			}

			public void Connect(TcpClient client)
			{
				socket = client;
				socket.ReceiveBufferSize = DATA_BUFFER_SIZE;
				socket.SendBufferSize = DATA_BUFFER_SIZE;
				_stream = socket.GetStream();
				_receiveBuffer = new byte[DATA_BUFFER_SIZE];
				_stream.BeginRead(_receiveBuffer, 0, DATA_BUFFER_SIZE, OnReceiveData, null);


				//TODO: Send Welcome Messege
			}

			private void OnReceiveData(IAsyncResult asyncResult)
			{
				try
				{
					int length = _stream.EndRead(asyncResult);

					if(length <= 0)
					{
						return;
					}
					byte[] bytes = new byte[length];
					Array.Copy(_receiveBuffer,bytes,bytes.Length);
					//TODO : handle data
					_stream.BeginRead(_receiveBuffer,0,bytes.Length, OnReceiveData, null);
				}
				catch (Exception ex)
				{
					CloseConnction();
					Console.WriteLine(ex.Message);
					return;
					//TODO: Disconnect the client
				}
			}
			private void CloseConnction()
			{
				Console.WriteLine($"Connection from '{0}' has been terminated", socket.Client.RemoteEndPoint?.ToString());
				socket.Close();
			}


		}

	}
}
