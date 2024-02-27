// See https://aka.ms/new-console-template for more information


namespace app
{
	public class Recoil
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Game Server, Recoil!");
			SocketGameServer.Server.Server.Start(10,26965);
			Console.ReadKey();
		}

	}
}