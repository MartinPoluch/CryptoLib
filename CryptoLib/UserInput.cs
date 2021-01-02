using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib {
	public static class UserInput {

		public static readonly int INVALID_VALUE = int.MinValue;

		public static string ReadUserInput(string message) {
			Console.Write(message);
			return Console.ReadLine();
		}

		public static int ReadUserIntInput(string message) {
			Console.Write(message);
			string input = Console.ReadLine();
			if (Int32.TryParse(input, out int number)) {
				return number;
			}
			else {
				return INVALID_VALUE;
			}
		}
	}
}
