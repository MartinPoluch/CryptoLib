using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib {
	public interface Decryptor {

		string Decrypt(string encryptedText, string password);
	}
}
