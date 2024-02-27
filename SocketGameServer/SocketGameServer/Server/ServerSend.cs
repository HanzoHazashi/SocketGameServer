using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketGameServer.Server
{
	class ServerSend
	{

		public static void Welcome(int toClient,string msg)
		{
			using (Packet packet = new Packet())
			{
				packet.WriteString(msg);
				packet.WriteInt(toClient);

				SendTCPData(toClient, packet);
			}
		}

		private static void SendTCPData(int toClient, Packet packet)
		{

			packet.WriteLen
		}
	}
}
