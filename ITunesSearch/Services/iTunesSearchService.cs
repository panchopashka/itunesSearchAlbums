using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using ITunesSearch.Models;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace ITunesSearch.Services
{
	/// <summary>
	/// Сервис для общения с сервисом ITunes.
	/// </summary>
	public class iTunesSearchService
	{
		/// <summary>
		/// Базовый url для запросов к сервису itunes.
		/// </summary>
		private string _baseSearchUrl = "https://itunes.apple.com/searc1h";

		/// <summary>
		/// Экземпляр сервиса общения с сервисом ITunes.
		/// </summary>
		protected static iTunesSearchService Instance { get; set; }

		/// <summary>
		/// Конструирует экземпляр сервиса.
		/// </summary>
		protected iTunesSearchService()
		{			
		}

		/// <summary>
		/// Синглтон.
		/// </summary>
		public static iTunesSearchService GetiTunesSearchService()
		{
			if (Instance == null)
				Instance = new iTunesSearchService();
			return Instance;
		}

		/// <summary>
		/// Поиск альбомов по имени артиста.
		/// </summary>
		/// <param name="artistName">Наименование артиста.</param>
		public async Task<AlbumResult> GetAlbumsByArtistIdAsync(string artistName)
		{
			var nvc = HttpUtility.ParseQueryString(string.Empty);
			nvc.Add("term", artistName);
			nvc.Add("entity", "album");
			string apiUrl = string.Concat(_baseSearchUrl, ToQueryString(nvc));
			try
			{
				return await MakeAPICall<AlbumResult>(apiUrl);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Преобразовать словарь в строку запроса. 
		/// </summary>
		private string ToQueryString(NameValueCollection nvc)
		{
			var array = (
				from key in nvc.AllKeys
				from value in nvc.GetValues(key)
				select string.Format(
			"{0}={1}",
			HttpUtility.UrlEncode(key),
			HttpUtility.UrlEncode(value))
				).ToArray();
			return "?" + string.Join("&", array);
		}

		/// <summary>
		/// Совершить вызов api и десериализовать данные.
		/// </summary>
		/// <typeparam name="T">Тип полученныз данных.</typeparam>
		/// <param name="apiCall">Стркоа вызова.</param>
		async private Task<T> MakeAPICall<T>(string apiCall)
		{
			using (var client = new HttpClient())
			{
				var objString = await client.GetStringAsync(apiCall).ConfigureAwait(false);
				return (T)DeserializeObject<T>(objString);
			}
		}

		/// <summary>
		/// Десериализует json формат в необходимый тип объекта.
		/// </summary>
		/// <typeparam name="T">Тип результата.</typeparam>
		/// <param name="objString">Строка в json формате.</param>
		private T DeserializeObject<T>(string objString)
		{
			using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(objString)))
			{
				var serializer = new DataContractJsonSerializer(typeof(T));
				return (T)serializer.ReadObject(stream);
			}
		}
	}
}
