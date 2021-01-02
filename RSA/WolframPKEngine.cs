using System;
using System.Linq;
using System.Threading.Tasks;
using Wolfram.Alpha;
using Wolfram.Alpha.Models;
using BigInteger = Extreme.Mathematics.BigInteger;

namespace RSA {
	public class WolframPKEngine : PrivateKeysEngine {

		public static readonly string Appid = "6VKL83-9EKQ6UEV9U";
		private WolframAlphaService _wolframAlpha;

		public WolframPKEngine() : base() {
			_wolframAlpha = new WolframAlphaService(Appid);
		}

		private async Task Compute(BigInteger n) {
			try {
				WolframAlphaRequest request = new WolframAlphaRequest($"factor {n}");
				WolframAlphaResult result = await _wolframAlpha.Compute(request);
				var pod = result.QueryResult.Pods[1];
				var subPod = pod.SubPods[0];
				Keys = subPod.Plaintext
					.Split(' ')[0]
					.Split('×')
					.Select(BigInteger.Parse)
					.ToArray();
			}
			catch (Exception e) {
				Console.WriteLine($"Cannot find private keys: {e}");
			}
		}

		public override void FindPrivateKeys(BigInteger n) {
			Keys[0] = EmptyKey;
			Keys[1] = EmptyKey;
			Compute(n).Wait();
		}
	}
}
