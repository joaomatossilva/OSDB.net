using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OSDBnet;

namespace OSDBnet.SimpleTest {
	class Program {
		static void Main(string[] args) {
			var hash1 = HashHelper.ComputeMovieHash(@"D:\Downloads\osdbsamples\breakdance.avi");
			Console.WriteLine("hash1: {0}", HashHelper.ToHexadecimal(hash1));
			var hash2 = HashHelper.ComputeMovieHash(@"D:\Downloads\osdbsamples\dummy.bin");
			Console.WriteLine("hash2: {0}", HashHelper.ToHexadecimal(hash2));

			using (var client = Osdb.Login("pt", "OS Test User Agent")) {
				var subtitles = client.SearchSubtitles(@"D:\Downloads\Eureka.S04E11.Liftoff.HDTV.XviD-FQM.avi");
				var subtitle = subtitles.First();
				client.DownloadSubtitleToPath(@"D:\Downloads", subtitle);
			}

			Console.WriteLine("Logged out.. press 'enter' key to exit");
			Console.ReadLine();
		}
	}
}
