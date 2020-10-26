using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Godot;
using Newtonsoft.Json.Linq;
using File = System.IO.File;

namespace Labyrinth.Objects
{
	public class Globals : Node
	{
		public int PlayerScore = 0;
		public string DiscordTag;

		private static readonly string _apiEndpoint = "https://discord.com/api/v8";

		private string _scope = "identify";
		private string _clientId = "770277582460551178";
		private string _clientSecret = "VlHspxFEloh69ye2IA3_ss33X0bAkGkf";
		private string _token;
		private readonly HTTPRequest _request = new HTTPRequest();

		public override async void _Ready()
		{
			OS.WindowMinimized = true;
			AddChild(_request);
			await DiscordAuthAsync("http://localhost:8910/callback/");
			OS.WindowMaximized = true;
			OS.WindowFullscreen = true;
			GetTree().CurrentScene.GetNode<Control>("Pause/Control").Visible = false;
		}

		public async Task DiscordAuthAsync(string prefix)
		{
			Process.Start(
				"https://discord.com/api/oauth2/authorize?client_id=770277582460551178&redirect_uri=http%3A%2F%2Flocalhost%3A8910%2Fcallback%2F&response_type=code&scope=identify");

			// Create a listener.
			HttpListener listener = new HttpListener();

			listener.Prefixes.Add(prefix);

			// Start listening
			listener.Start();

			HttpListenerContext context = await listener.GetContextAsync();

			// Get request from context
			HttpListenerRequest request = context.Request;


			// Get token from code
			GetAccessToken(request.QueryString["code"]);
			while (_token is null)
			{
				// Slow polling
				await Task.Delay(25);
			}

			GetCurrentUser();
			while (DiscordTag is null)
			{
				// Slow polling
				await Task.Delay(25);
			}

			// Obtain a response object.
			HttpListenerResponse response = context.Response;

			// Construct a response.
			byte[] buffer = File.ReadAllBytes("Assets/Labyrinth - Authorized.html");

			// Get a response stream and write the response to it.
			response.ContentLength64 = buffer.Length;
			await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
			response.OutputStream.Close();

			listener.Close();
		}

		private void GetAccessToken(string code)
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>
			{
				{"client_id",_clientId},
				{"client_secret", _clientSecret},
				{"grant_type", "authorization_code"},
				{"redirect_uri", "http://localhost:8910/callback/"},
				{"code", code},
				{"scope", _scope}
			};

			_request.Connect("request_completed", this, nameof(GetAccessTokenRequestCompleted));
			_request.Request($"{_apiEndpoint}/oauth2/token", new[] { "Content-Type: application/x-www-form-urlencoded" }, true, HTTPClient.Method.Post,
				string.Join("&",
					parameters.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}")));
		}

		private void GetAccessTokenRequestCompleted(int result, int responseCode, string[] headers, byte[] body)
		{
			string json = Encoding.UTF8.GetString(body);
			_token = JObject.Parse(json)["access_token"]?.ToObject<string>();
			GD.Print(_token);
			_request.Disconnect("request_completed", this, nameof(GetAccessTokenRequestCompleted));
		}

		private void GetCurrentUser()
		{
			_request.Connect("request_completed", this, nameof(GetCurrentUserRequestCompleted));
			_request.Request($"{_apiEndpoint}/users/@me", new[] { $"Authorization: Bearer {_token}" });
		}

		private void GetCurrentUserRequestCompleted(int result, int responseCode, string[] headers, byte[] body)
		{
			string json = Encoding.UTF8.GetString(body);
			JObject jObject = JObject.Parse(json);
			DiscordTag = $"{jObject["username"]?.ToObject<string>()}#{jObject["discriminator"]?.ToObject<string>()}";
			GD.Print(DiscordTag);
			_request.Disconnect("request_completed", this, nameof(GetCurrentUserRequestCompleted));
		}
	}
}
