using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib {

	public class TextAnalysis {

		public static readonly int LOWEST_VALUE = (int) 'A';
		public static readonly int HIGHEST_VALUE = (int)'Z';
		public static readonly int NUMBER_OF_CHARS = 26;

		private List<double> Probs { get; set; }
		private Dictionary<char, int> Frequency { get; set; }
		private int NumberOfEncryptedChars { get; set; }

		public double Coincidence { get; private set; }
		public string ValidText { get; private set; } // all character which was used for analysis (A-Z)
		public string FullText { get; }

		public TextAnalysis(string text, bool onlyFrequency = false) {
			FullText = text;
			initFrequency();
			if (! onlyFrequency) {
				initProbs();
			}
		}

		private void initFrequency() {
			Frequency = new Dictionary<char, int>();
			for (char c = 'A'; c <= 'Z'; c++) {
				Frequency[c] = 0;
			}

			NumberOfEncryptedChars = 0;
			foreach (char c in FullText) {
				if (Frequency.ContainsKey(c)) {
					Frequency[c]++;
					NumberOfEncryptedChars++;
					ValidText += c;
				}
			}

			Coincidence = CalculateCoincidenceIndex();

		}

		private void initProbs() {
			Probs = new List<double>();
			for (int i = 0; i < NUMBER_OF_CHARS; i++) {
				Probs.Add(0.0);
			}

			foreach (KeyValuePair<char, int> keyValuePair in Frequency) {
				double prob = (double)keyValuePair.Value / (double)NumberOfEncryptedChars;
				SetProb(keyValuePair.Key, prob);
			}
		}

		private double CalculateCoincidenceIndex() {
			double coincidence = 0.0;
			foreach (int frequency in Frequency.Values) {
				double prob1 = (double)frequency / (double)NumberOfEncryptedChars;
				double prob2 = (double)(frequency - 1) / (double)(NumberOfEncryptedChars - 1);
				coincidence += prob1 * prob2;
			}

			return coincidence;
		}

		private int GetIndex(char c) {
			return c - LOWEST_VALUE;
		}

		private void SetProb(char c, double prob) {
			if (prob < 0 || prob > 1) {
				throw new ArgumentException($"Wrong probability: {prob}");
			}

			int index = GetIndex(c);
			if (index < 0 || index >= NUMBER_OF_CHARS){
				throw new ArgumentException($"Wrong char: {c}");
			}

			Probs[index] = prob;
		}

		public double GetProb(char c) {
			int index = GetIndex(c);
			if (0 <= index && index < NUMBER_OF_CHARS) {
				return Probs[index];
			}
			throw new ArgumentException($"Wrong char: {c}");
		}

		public double EuclideanDistance(TextAnalysis otherProbs) {
			double sum = 0;
			for (int i = 0; i < Probs.Count; i++) {
				sum += Math.Pow(Probs[i] - otherProbs.Probs[i], 2);
			}
			return Math.Sqrt(sum);
		}

		public void ShiftToLeft() {
			double firstProb = Probs[0];
			Probs.RemoveAt(0);
			Probs.Add(firstProb);
		}

		public double SumOfProbs() {
			return Probs.Sum();
		}

		public static bool ValidChar(char c) {
			return (LOWEST_VALUE <= c && c <= HIGHEST_VALUE);
		}

		public override string ToString() {
			if (Probs == null || Probs.Count == 0) {
				return "No probabilities";
			}

			string output = "";
			for (char c = 'A'; c <= 'Z'; c++) {
				output += $"{c}: {GetProb(c)}\n";
			}
			return output;
		}
	}
}
