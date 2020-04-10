using ITunesSearch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITunesSearch.Services
{
	/// <summary>
	/// DAO для обращения к хранилищу списка альбомов.
	/// </summary>
	public static class AlbumsDao
	{
		/// <summary>
		/// Название файла для хранения результатов запросов.
		/// </summary>
		private const string fileName = "albums.json";

		/// <summary>
		/// Сопостовление строки запроса к результату выполнения.
		/// </summary>
		public static Dictionary<string, AlbumResult> AlbumsMapper = new Dictionary<string, AlbumResult>();

		/// <summary>
		/// Получить результат поиска альбома.
		/// </summary>
		/// <param name="searchString">Строка запроса.</param>
		public static AlbumResult GetAlbumResult(string searchString)
		{
			AlbumResult albumResult;
			if (AlbumsMapper.TryGetValue(searchString, out albumResult))
				return albumResult;
			return null;
		}

		/// <summary>
		/// Добавить или обновить результат поиска альбома.
		/// </summary>
		/// <param name="searchString">Строка поиска.</param>
		/// <param name="result">Результат поиска.</param>
		public static void AddOrUpdate(string searchString, AlbumResult result)
		{
			AlbumResult albumResult;
			if (AlbumsMapper.TryGetValue(searchString, out albumResult))
			{
				AlbumsMapper[searchString] = albumResult;
			}
			else
			{
				AlbumsMapper.Add(searchString, result);
			}
		}

		public static void SerializeAlbums()
		{

			var result = JsonConvert.SerializeObject(AlbumsMapper);
			File.WriteAllText(fileName, result);
		}

		public static void DeserilizeAlbums()
		{
			if (!File.Exists(fileName))
				return;
			var loaded = File.ReadAllText(fileName);
			AlbumsMapper = JsonConvert.DeserializeObject<Dictionary<string, AlbumResult>>(loaded);
		}
	}
}
