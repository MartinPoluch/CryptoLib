using System;
using BigInteger = Extreme.Mathematics.BigInteger;

namespace RSA {
	public class RSADecryptor {

		public PrivateKeysEngine PrivateKeysEngine { get; set; }

		public RSADecryptor(bool customEngine = false) {
			if (customEngine) {
				PrivateKeysEngine = new CustomPKEngine();
			}
			else {
				PrivateKeysEngine = new WolframPKEngine();
			}
		}

		/**
		 * (n, e) = verejny kluc
		 * n = sucin dvoch velkych prvocisiel (p * q)
		 * e = male prvocislo, verejny exponent
		 * y = zasifrovana sprava
		 */
		public BigInteger Decrypt(BigInteger n, BigInteger e, BigInteger y) {
			PrivateKeysEngine.FindPrivateKeys(n); // najde dva privatne kluce (dve velke prvocisla)
			Console.WriteLine();
			Console.WriteLine($"p= {PrivateKeysEngine.Keys[0]}");
			Console.WriteLine($"q= {PrivateKeysEngine.Keys[1]}");

			BigInteger pMinus1 = BigInteger.Subtract(PrivateKeysEngine.Keys[0], 1); // p - 1
			BigInteger qMinus1 = BigInteger.Subtract(PrivateKeysEngine.Keys[1], 1); // q - 1
			BigInteger fiN = BigInteger.Multiply(pMinus1, qMinus1); // hodnota Eulerovej funkcie fi(n)
			Console.WriteLine($"fiN= {fiN}");

			BigInteger d = BigInteger.ModularInverse(e, fiN); // privatny exponent
			Console.WriteLine($"d= {d}");
			Console.WriteLine($"e*d % fiN = {BigInteger.Mod(BigInteger.Multiply(e, d), fiN)} (should be 1)");
			Console.WriteLine($"Private key (n, d) = ({n}, {d})");

			BigInteger decrypted = BigInteger.ModularPow(y, d, n); // desifrovana sprava
			Console.WriteLine($"decrypted message= {decrypted}");
			return decrypted;
		}

		public BigInteger Encrypt(BigInteger n, BigInteger e, BigInteger x) {
			return BigInteger.ModularPow(x, e, n);
		}
	}
}
