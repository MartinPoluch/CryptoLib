using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vigenere {


	class VigenereApp {

		private static readonly string FILES_FOLDER = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\files\";

		static void Main(string[] args) {
			VigenereDecryptor decryptor = ConfigureDecryptor(out string encryptedText);
			int passwordLength = decryptor.PasswordLengthAnalyzes(20, 29);
			StringBuilder password = new StringBuilder(decryptor.FindPossiblePassword(passwordLength));
			string decryptedText = decryptor.Decrypt(encryptedText, password.ToString());
			Console.WriteLine(decryptedText);
			string actionMessage = "Choose action (OK or position:letter): ";
			string action = ReadUserInput(actionMessage);
			while (action.ToUpper() != "OK") {
				string[] split = action.Split(':');
				if (split.Length == 2 && int.TryParse(split[0], out int position)) {
					char replacement = split[1].ToUpper()[0];
					password[position % passwordLength] = decryptor.Decrypt(decryptor.EncryptedText.ValidText[position].ToString(), replacement.ToString())[0];
					decryptedText = decryptor.Decrypt(encryptedText, password.ToString());
					Console.WriteLine("\n" + decryptedText + "\n");
					action = ReadUserInput(actionMessage);
				}
				else {
					Console.WriteLine("Wrong input!");
				}
			}



			Console.WriteLine("END OF PROGRAM");
			Console.Read();
		}

		private static VigenereDecryptor ConfigureDecryptor(out string encryptedText) {
			string language = ReadUserInput("Choose language (sk/en): ");
			string languageFileName = (language == "en") ? "lang_en.txt" : "lang_sk.txt";
			string languageText = File.ReadAllText(FILES_FOLDER + languageFileName);
			string textFileName = ReadUserInput("Type file name with encrypted text: ");
			while (! File.Exists(FILES_FOLDER + textFileName)) {
				textFileName = ReadUserInput("Type file name with encrypted text: ");
			}
			encryptedText = File.ReadAllText(FILES_FOLDER + textFileName);
			return new VigenereDecryptor(languageText, encryptedText);
		}

		private static string ReadUserInput(string message) {
			Console.Write(message);
			return Console.ReadLine();
		}

	}
}
