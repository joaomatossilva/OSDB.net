using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using System.IO;
using OSDBnet;

namespace OSDBcmd {
	class Program {

		private const string EXECUTABLENAME = "OSDB";

		static void Main(string[] args) {
			bool lucky = false;
			bool showHelp = false;
			string movieName = null;
			List<string> languages = new List<string>();

			OptionSet opts = new OptionSet() { 
				{ "help", "Help Me!", v => showHelp = v != null },
				{ "m|movie=", "Movie file", v => movieName = v },
				{ "lucky", "Feeling lucky", v => lucky = v != null },
				{ "l|languages=", "Languages", v => languages.Add(v) }
			};

			List<string> extra;
			try {
				extra = opts.Parse(args);
			} catch (OptionException e) {
				ShowError(e.Message);
				Console.WriteLine("Try `{0} --help' for more information.", EXECUTABLENAME);
				return;
			}

			if (showHelp) {
				ShowHelp(opts);
				return;
			}

			if (string.IsNullOrEmpty(movieName)) {
				ShowError("You must specify a movie file name");
				return;
			}

			if (!Path.IsPathRooted(movieName)) {
				movieName = Path.Combine(Environment.CurrentDirectory, movieName);
			}
			if (!File.Exists(movieName)) {
				ShowError(string.Format("Could not find the movie file '{0}'", movieName));
				return;
			}
			try {
				DownloadSubtitle(movieName, lucky, languages);
			} catch (Exception ex) {
				Console.WriteLine("ops.... something went wrong.");
				Console.WriteLine("{0} : {1}", ex.GetType().Name, ex.Message);
			}
		}

		static void DownloadSubtitle(string movieFileName, bool lucky, IList<string> languages) {
			var systemLanguage = GetSystemLanguage();			
			using (var osdb = Osdb.Login(systemLanguage)) {
				var subtitles = osdb.SearchSubtitles(movieFileName);

				int subtitlesCount = subtitles.Count();
				if (subtitlesCount == 0) {
					Console.WriteLine("Sorry, no subtitles found for your movie");
					return;
				}

				var selectedSubtitle = subtitles.First();
				if (!lucky) {
					var canceled = !PromptForSubtitle(subtitles, ref selectedSubtitle);
					if (canceled) {
						return;
					}
				}

				string subtitleFile = osdb.DownloadSubtitleToPath(Path.GetDirectoryName(movieFileName), selectedSubtitle);
				Console.WriteLine("Subtitle downloaded to '{0}'", subtitleFile);
			}
		}

		private static bool PromptForSubtitle(IEnumerable<Subtitle> subtitles, ref Subtitle selectedSubtitle) {
			//TODO: prompt for subtitles
			return true;
		}

		static void ShowError(string message) {
			Console.WriteLine(message);
		}

		static void ShowHelp(OptionSet p) {
			Console.WriteLine("Usage: {0} -m filename [OPTIONS]+", EXECUTABLENAME);
			Console.WriteLine("A subtitle dowloader utility");
			Console.WriteLine();
			Console.WriteLine("Options:");
			p.WriteOptionDescriptions(Console.Out);
		}

		static string GetSystemLanguage() {
			var currentCulture = System.Globalization.CultureInfo.CurrentUICulture;
			return currentCulture.TwoLetterISOLanguageName.ToLower();
		}
	}
}
