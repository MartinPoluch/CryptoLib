using BigInteger = Extreme.Mathematics.BigInteger;

namespace RSA {
	public class CustomPKEngine : PrivateKeysEngine {

		public CustomPKEngine() : base() {
		}

		private void AddPrivateKey(BigInteger n, BigInteger privateKey) {
			Keys[0] = privateKey;
			Keys[1] = BigInteger.Divide(n, privateKey);
		}

		public override void FindPrivateKeys(BigInteger n) {
			Keys[0] = EmptyKey;
			Keys[1] = EmptyKey;
			long i = 1;
			while (true) {
				long number = 6 * i - 1; // 6 * i + 5

				if ((n % number).IsZero) {
					AddPrivateKey(n, number);
					break;
				}

				number = 6 * i + 1;
				if ((n % number).IsZero) {
					AddPrivateKey(n, number);
					break;
				}
				i++;
			}
		}
	}
}
