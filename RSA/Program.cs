using System;
using BigInteger = Extreme.Mathematics.BigInteger;

namespace RSA {
	class Program {
		static void Main(string[] args) {
			RSADecryptor decryptor = new RSADecryptor(true);
			decryptor.Decrypt(BigInteger.Parse("13169004533"), 65537, 6029832903);
			Console.Read();
		


		}
	}
}
