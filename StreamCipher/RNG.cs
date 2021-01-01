using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;

namespace StreamCipher {

	public class RNG {

		private const long _a = 84589;
		private const long _b = 45989;
		private const long _m = 217728;
		private long _randx;

		public RNG(long seed) {
			_randx = seed;
		}

		private double GenerateDouble() {
			_randx = (_a * _randx + _b) % _m;
			return (double) _randx / (double) _m;
		}

		/**
		 * Generate number between 0-25
		 */
		public int Generate() {
			return (int) (TextAnalysis.NUMBER_OF_CHARS * GenerateDouble());
		}
	}
}
