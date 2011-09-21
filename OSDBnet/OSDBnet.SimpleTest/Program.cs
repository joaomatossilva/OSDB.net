using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OSDBnet;

namespace OSDBnet.SimpleTest {
	class Program {
		static void Main(string[] args) {
			var client = new OSDBClient();
			var info = client.ServerInfo();
			Console.ReadLine();
		}
	}
}
