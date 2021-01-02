using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extreme.Mathematics;

namespace RSA {
	public class Exercise {

		public BigInteger Modulus { get; set; }

		public BigInteger Exponent { get; set; }

		public BigInteger EncryptedMessage { get; set; }

		public bool Easy { get; set; }

		public Exercise(BigInteger modulus, BigInteger exponent, BigInteger encryptedMessage) {
			Modulus = modulus;
			Exponent = exponent;
			EncryptedMessage = encryptedMessage;
		}

		public Exercise(string modulus, string exponent, string encryptedMessage) {
			Modulus = BigInteger.Parse(modulus);
			Exponent = BigInteger.Parse(exponent);
			EncryptedMessage = BigInteger.Parse(encryptedMessage);
			Easy = false;
		}

		public Exercise(string modulus, string exponent, string encryptedMessage, bool easy) {
			Modulus = BigInteger.Parse(modulus);
			Exponent = BigInteger.Parse(exponent);
			EncryptedMessage = BigInteger.Parse(encryptedMessage);
			Easy = easy;
		}

		public override string ToString() {
			return $"n = {Modulus}; e = {Exponent}; y = {EncryptedMessage}";
		}
	}
}
