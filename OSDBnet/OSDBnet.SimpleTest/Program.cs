using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OSDBnet;

namespace OSDBnet.SimpleTest {
	class Program {
		static void Main(string[] args) {
			using (var client = Osdb.Login("pt")) {
				Console.WriteLine("Logged In.. press 'enter' key to logout");
				Console.ReadLine();
			}

			Console.WriteLine("Logged out.. press 'enter' key to exit");
			Console.ReadLine();
		}
	}
}
