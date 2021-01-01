using System;

namespace CryptoApp {
	class Program {
		public static void Main(String[] args) {
			SplitTextToLetters("NMUSMRFJGRWSWVKKDJKYTYTNSVMOJW");
			Console.Read();
		}

		public static void SplitTextToLetters(string text, int newLine = 3) {
			int counter = 0;
			foreach (var leter in text) {
				System.Console.WriteLine(leter);
				if (++counter % newLine == 0) {
					counter = 0;
					System.Console.WriteLine();
				}
			}
		}

		public static void JoinLettersToText(string letters) {

		}

	}
}
