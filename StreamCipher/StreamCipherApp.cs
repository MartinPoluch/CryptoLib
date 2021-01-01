using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCipher {
	/**
	 * keys:
	 *	text1.txt => 77777
	 *  text2.txt => 78901
	 *  text3.txt => 89012
	 *	text4.txt => 98765
	 */
	class StreamCipherApp {

		private static readonly string FILES_FOLDER = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\files\";
		private static readonly int DECRYPTION_MODE = 0;
		private static readonly int BRUTEFORCE_MODE = 1;
		private static readonly int INVALID_VALUE = -1;

		static void Main(string[] args) {
			try {
				string encryptedText = ReadEncryptedFile();
				int mode = ReadMode();

				StreamCipherDecryptor decryptor = new StreamCipherDecryptor();
				if (mode == DECRYPTION_MODE) {
					int key = ReadUserIntInput("Enter key for decryption: ");
					Console.WriteLine(decryptor.Decrypt(encryptedText, key));
				}
				else {
					Console.WriteLine("Processing all possible keys ...");
					decryptor.DoBruteForceAttack(encryptedText);
					decryptor.PrintDecryptedTexts();
				}
			}
			catch (Exception e) {
				Console.WriteLine("Exception occurred");
				Console.WriteLine(e);
			}

			Console.Read();
		}

		private static string ReadEncryptedFile() {
			string filename = ReadUserInput("Type file name with encrypted text: ");
			while (!File.Exists(FILES_FOLDER + filename)) {
				filename = ReadUserInput("File does not exist\nType file name with encrypted text: ");
			}
			string encryptedText = File.ReadAllText(FILES_FOLDER + filename);
			return encryptedText;
		}

		private static int ReadMode() {
			string modeMessage = $"Choose mode ({DECRYPTION_MODE} = decrypt, {BRUTEFORCE_MODE} = bruteforce): ";
			int mode = ReadUserIntInput(modeMessage);
			while (! (mode == DECRYPTION_MODE || mode == BRUTEFORCE_MODE)) {
				mode = ReadUserIntInput("Wrong input!\n" + modeMessage);
			}

			return mode;
		}

		private static string ReadUserInput(string message) {
			Console.Write(message);
			return Console.ReadLine();
		}

		private static int ReadUserIntInput(string message) {
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
