using BigInteger = Extreme.Mathematics.BigInteger;

namespace RSA {
	public abstract class PrivateKeysEngine {

		public BigInteger[] Keys { get; set; }
		public static readonly BigInteger EmptyKey = BigInteger.Zero;

		protected PrivateKeysEngine() {
			Keys = new BigInteger[2] { EmptyKey, EmptyKey };
		}

		public abstract void FindPrivateKeys(BigInteger n);
	}
}
