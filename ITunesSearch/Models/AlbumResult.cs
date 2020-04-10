using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ITunesSearch.Models
{
	[DataContract]
	public class AlbumResult
	{
		[DataMember(Name = "resultCount")]
		public int Count { get; set; }

		[DataMember(Name = "results")]
		public List<Album> Albums { get; set; }
	}
}
