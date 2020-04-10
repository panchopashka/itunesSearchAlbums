using ITunesSearch.Models;
using ITunesSearch.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ITunesSearch.ViewModels
{
	/// <summary>
	/// View model для отображения основного окна поиска альбомов.
	/// </summary>
	public class SearchViewModel : BaseViewModel
	{
		/// <summary>
		/// Сервис для работы с api itunes.
		/// </summary>
		private readonly iTunesSearchService _searchService;

		/// <summary>
		/// Ссылка на диспетчер для управления основным потоком.
		/// </summary>
		private readonly Dispatcher _dispatcher;

		/// <summary>
		/// Конструирует view model  для отображения основного окна поиска альбомов.
		/// </summary>
		/// <param name="dispatcher">Ссылка на диспетчер для управления основным потоком.</param>
		/// <param name="telegramService">Сервис для работы с itunes.</param>
		public SearchViewModel(Dispatcher dispatcher, iTunesSearchService searchService)
		{
			Albums = new ObservableCollection<Album>();
			_dispatcher = dispatcher;
			_searchService = searchService;
		}

		/// <summary>
		/// Строка для поиска.
		/// </summary>
		public string SearchString { get; set; }

		/// <summary>
		/// Список альбомов.
		/// </summary>
		public ObservableCollection<Album> Albums { get; set; }

		private RelayCommand searchcommand;
		public RelayCommand SearchCommand
		{
			get
			{
				return searchcommand ??
				  (searchcommand = new RelayCommand(obj =>
				  {
					  Task.Run(async () =>
					  {
						  var albumsResult = await _searchService.GetAlbumsByArtistIdAsync(SearchString);
						  if (albumsResult != null)
						  {
							  AlbumsDao.AddOrUpdate(SearchString, albumsResult);
							  Albums = new ObservableCollection<Album>(albumsResult.Albums);
							  return;
						  }
						  var result = AlbumsDao.GetAlbumResult(SearchString);
						  if (result != null)
						  {
							  Albums = new ObservableCollection<Album>(result.Albums);
							  return;
						  }
						  Albums = new ObservableCollection<Album>();
					  });
				  }, obj => !string.IsNullOrEmpty(SearchString)));
			}
		}
	}
}
