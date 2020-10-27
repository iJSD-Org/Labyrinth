using System;
using DiscordRPC;
using Godot;
using Labyrinth.NamedPipeClient;

namespace Labyrinth.Objects
{
	class RichPresence : Node
	{
		public DiscordRpcClient Client;

		public override void _Ready()
		{
			Client = new DiscordRpcClient("770277582460551178", -1, null, true, new UnityNamedPipe())
			{
				ShutdownOnly = true
			};

			Client.OnReady += (o, e) =>
			{
				GD.Print($"Received Ready from user {Client.CurrentUser}");
			};

			Client.Initialize();

			DiscordRPC.RichPresence rp = new DiscordRPC.RichPresence
			{
				Details = "https://github.com/iJSD-Org/Labyrinth",
				Assets = new Assets()
				{
					SmallImageKey = "godot",
					SmallImageText = "Made with Godot Engine",
					LargeImageKey = "minotaur",
				},
				Timestamps = new Timestamps(DateTime.UtcNow)
			};
			Client.SetPresence(rp);
		}

		public override void _ExitTree()
		{
			Client.Dispose();
		}
	}
}
