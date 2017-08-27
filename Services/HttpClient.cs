using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace DinnerTime.Api.Services
{
	public class HttpClient
	{
        public T Get<T>(string url) where T : class
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine("GET " + url);
			Console.ResetColor();

			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "application/json";
				request.Accept = "application/json";
				request.Timeout = 10000;
				request.KeepAlive = true;

				var response = (HttpWebResponse)request.GetResponse();

				string result;

				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					result = reader.ReadToEnd();
					reader.Close();
				}

                return JsonConvert.DeserializeObject<T>(result);
			}
			catch (WebException)
			{
				return null;
			}
		}
	}
}
