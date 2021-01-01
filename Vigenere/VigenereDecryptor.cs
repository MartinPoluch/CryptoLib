using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;

namespace Vigenere {
	public class VigenereDecryptor : Decryptor {

		private static readonly string LINE_SEPARATOR = "\n--------------------------------------------------------\n";

		private string OriginalEncryptedText { get; set; } // with characters which are not ecrypted (a-z, numbers, spaces, ...)
		public TextAnalysis LanguageProbs { get; private set; }
		public TextAnalysis EncryptedText { get; set; }

		public VigenereDecryptor(string languageText, string encryptedText) {
			LanguageProbs = new TextAnalysis(languageText);
			EncryptedText = new TextAnalysis(encryptedText);
			OriginalEncryptedText = encryptedText;
		}

		private List<TextAnalysis> SplitEncryptedText(int numOfGroups) {
			List<TextAnalysis> groups = new List<TextAnalysis>();
			for (int i = 0; i < numOfGroups; i++) {
				string groupText = "";
				int j = i;
				while (j < EncryptedText.ValidText.Length) {
					groupText += EncryptedText.ValidText[j];
					j += numOfGroups;
				}
				groups.Add(new TextAnalysis(groupText));
			}
			return groups;
		}

		public double PasswordLengthAnalyzes(int passwordLength) {
			Console.WriteLine($"Password length: {passwordLength}");
			int i = 0;
			double sumOfErrors = 0;
			List<TextAnalysis> groups = SplitEncryptedText(passwordLength);
			foreach (TextAnalysis group in groups) {
				double error = Math.Abs(group.Coincidence - LanguageProbs.Coincidence);
				sumOfErrors += error;
				Console.WriteLine($"{i++}: error: {error} | coincidence: {group.Coincidence}");
			}

			return sumOfErrors;
		}

		public int PasswordLengthAnalyzes(int fromLength, int toLength) {
			Console.WriteLine(LINE_SEPARATOR);
			SortedDictionary<double, int> errorForLength = new SortedDictionary<double, int>();
			for (int length = fromLength; length <= toLength; length++) {
				double error = PasswordLengthAnalyzes(length);
				errorForLength[error] = length;
				Console.WriteLine($"sum of errors: {error}");
				Console.WriteLine();
			}

			Console.WriteLine("Sorted by error sum:");
			foreach (KeyValuePair<double, int> keyValuePair in errorForLength) {
				Console.WriteLine($"error: {keyValuePair.Key} length: {keyValuePair.Value}");
			}

			Console.WriteLine(LINE_SEPARATOR);
			return errorForLength.First().Value;
		}

		public string FindPossiblePassword(int passwordLength) {
			string password = ""; // possible password (not guaranteed)
			int groupIndex = 0;
			foreach (TextAnalysis group in SplitEncryptedText(passwordLength)) {
				double minimum = Double.MaxValue;
				char bestMatch = '-';
				Console.WriteLine($"Position: {groupIndex++}");
				for (char c = 'A'; c <= 'Z'; c++) {
					double distance = group.EuclideanDistance(LanguageProbs);
					Console.WriteLine($"{c}: {distance}");
					group.ShiftToLeft();

					if (distance < minimum) {
						minimum = distance;
						bestMatch = c;
					}
				}

				password += bestMatch;
				Console.WriteLine($"Best match (minimum): {bestMatch} value: {minimum}\n");
			}

			Console.WriteLine($"Found password: {password}\n");
			return password;
		}

		public string Decrypt(string encryptedText, string password) {
			if (password.Length > 1) {
				Console.WriteLine(LINE_SEPARATOR);
				Console.WriteLine($"Password: {password}\n");
			}
			string plainText = "";
			int passPos = 0;
			foreach (char encryptedLetter in encryptedText) {
				if (TextAnalysis.LOWEST_VALUE <= encryptedLetter && encryptedLetter <= TextAnalysis.HIGHEST_VALUE) { // if it is in range A - Z
					int encryptedLetterPos = encryptedLetter - TextAnalysis.LOWEST_VALUE;
					int passwordLetterPos = password[passPos] - TextAnalysis.LOWEST_VALUE;
					int decryptedLetterPos = (encryptedLetterPos + TextAnalysis.NUMBER_OF_CHARS - passwordLetterPos) % TextAnalysis.NUMBER_OF_CHARS;
					char decryptedLetter = (char) (decryptedLetterPos + TextAnalysis.LOWEST_VALUE);
					plainText += decryptedLetter;
					passPos = (passPos == password.Length - 1) ? 0 : passPos + 1;
				}
				else {
					plainText += encryptedLetter;
				}
			}

			return plainText;
		}

		public string Decrypt(int minPasswordLength, int maxPasswordLength) {
			return Decrypt(OriginalEncryptedText, FindPossiblePassword(PasswordLengthAnalyzes(minPasswordLength, maxPasswordLength)));
		}
	}
}
