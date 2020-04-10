﻿using System.Runtime.Serialization;

namespace ITunesSearch.Models
{
	[DataContract]
	public class Album
	{
		[DataMember(Name = "artistId")]
		public long ArtistId { get; set; }

		[DataMember(Name = "collectionId")]
		public long CollectionId { get; set; }

		[DataMember(Name = "amgArtistId")]
		public long AMGArtistId { get; set; }

		[DataMember(Name = "artistName")]
		public string ArtistName { get; set; }

		[DataMember(Name = "collectionName")]
		public string CollectionName { get; set; }

		[DataMember(Name = "collectionCensoredName")]
		public string CollectionCensoredName { get; set; }

		[DataMember(Name = "artistViewUrl")]
		public string ArtistViewUrl { get; set; }

		[DataMember(Name = "collectionViewUrl")]
		public string CollectionViewUrl { get; set; }

		[DataMember(Name = "artworkUrl60")]
		public string ArtworkUrl60 { get; set; }

		[DataMember(Name = "artworkUrl100")]
		public string ArtworkUrl100 { get; set; }

		[DataMember(Name = "collectionPrice")]
		public double CollectionPrice { get; set; }

		[DataMember(Name = "releaseDate")]
		public string ReleaseDate { get; set; }

		[DataMember(Name = "collectionExplicitness")]
		public string CollectionExplicitness { get; set; }

		[DataMember(Name = "trackCount")]
		public int TrackCount { get; set; }

		[DataMember(Name = "country")]
		public string Country { get; set; }

		[DataMember(Name = "currency")]
		public string Currency { get; set; }

		[DataMember(Name = "primaryGenreName")]
		public string PrimaryGenreName { get; set; }

		[DataMember(Name = "copyright")]
		public string Copyright { get; set; }
	}
}
