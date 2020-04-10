using System;
using System.Diagnostics;
using System.Windows;

namespace ITunesSearch
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var service = Services.iTunesSearchService.GetiTunesSearchService();
			var searchWindow = new View.SearchView();
			Services.AlbumsDao.DeserilizeAlbums();
			searchWindow.Closed += Close;
			searchWindow.DataContext = new ViewModels.SearchViewModel(searchWindow.Dispatcher, service);
			searchWindow.Show();
		}

		private void Close(object sender, EventArgs e)
		{
			Services.AlbumsDao.SerializeAlbums();
			Process.GetCurrentProcess().Kill();
		}
	}
}
