﻿using System.ComponentModel;

namespace ITunesSearch.ViewModels
{
	/// <summary>
	/// Бзаовый класс для ViewModel.
	/// </summary>
	public class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Событие, которое активируется, когда какое то свойство меняет значение.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
	}
}
