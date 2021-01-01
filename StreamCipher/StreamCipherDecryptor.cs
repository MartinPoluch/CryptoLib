using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;

namespace StreamCipher {

	public class StreamCipherDecryptor {

		public List<TextAnalysis> DecryptedTexts { get; private set; }

		public StreamCipherDecryptor() {
			DecryptedTexts = new List<TextAnalysis>();
		}

		private string EncryptDecryptProcess(string plainText, long key, bool encrypt) {
			RNG rng = new RNG(key);
			string encryptedText = "";
			foreach (char c in plainText) {
				char letter = Char.ToUpper(c);
				if (TextAnalysis.ValidChar(letter)) {
					int inputPos = letter - TextAnalysis.LOWEST_VALUE;
					int generatedKey = rng.Generate();
					int outputPos = (encrypt) ? 
						((inputPos + generatedKey) % TextAnalysis.NUMBER_OF_CHARS) :
						((inputPos + (TextAnalysis.NUMBER_OF_CHARS - generatedKey)) % TextAnalysis.NUMBER_OF_CHARS);

					encryptedText += (char)(TextAnalysis.LOWEST_VALUE + outputPos);
				}
				else {
					encryptedText += letter;
				}
			}

			return encryptedText;
		}

		public string Encrypt(string plainText, long key) {
			return EncryptDecryptProcess(plainText, key, true);
		}

		public string Decrypt(string encryptedText, long key) {
			return EncryptDecryptProcess(encryptedText, key, false);
		}

		public void DoBruteForceAttack(string encryptedText, long maxKey = 217728, double threshold = 0.05) {
			for (int key = 0; key < maxKey; key++) {
				if ((key % 10000) == 0 && (key != 0)) {
					Console.WriteLine($"current key: {key}" );
				}

				string decrypted = Decrypt(encryptedText, key);
				TextAnalysis textAnalysis = new TextAnalysis(decrypted, true);
				if (textAnalysis.Coincidence > threshold) {
					DecryptedTexts.Add(textAnalysis);
					Console.WriteLine($"Added text, " +
					                  $"Coincidence: {textAnalysis.Coincidence} " +
					                  $"key:{key} " +
					                  $"start: {textAnalysis.FullText.Substring(0, 60)}");
				}
			}
			DecryptedTexts = DecryptedTexts.OrderByDescending(text => text.Coincidence).ToList();
		}


		public void PrintDecryptedTexts() {
			int cout = 1;
			foreach (TextAnalysis decryptedText in DecryptedTexts) {
				Console.WriteLine($"order: {cout++} " +
				                  $"coincidence: {decryptedText.Coincidence}" +
				                  $"\n{decryptedText.FullText}\n");
			}
		}
	}
}
