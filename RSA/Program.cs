using System;
using System.Collections.Generic;
using CryptoLib;
using BigInteger = Extreme.Mathematics.BigInteger;

namespace RSA {
	public class Program {

		private static List<Exercise> exercises;

		static void Main(string[] args) {
			CreateExercises();

			int index = 0;
			while (index != -1) { 
				index = UserInput.ReadUserIntInput("\nChoose exercise: ");
				while ((index == UserInput.INVALID_VALUE) || (-1 > index || index >= exercises.Count)) {
					Console.WriteLine("Invalid input !!!");
					index = UserInput.ReadUserIntInput("Choose exercise: ");
				}

				Exercise exercise = exercises[index];
				RSADecryptor decryptor = new RSADecryptor(exercise.Easy);
				BigInteger result = decryptor.Decrypt(exercise.Modulus, exercise.Exponent, exercise.EncryptedMessage);
				Console.WriteLine($"Result is OK: {decryptor.Encrypt(exercise.Modulus, exercise.Exponent, result) == exercise.EncryptedMessage}");
			}
			Console.Read();
		}

		private static void CreateExercises() {
			string e = "65537";
			exercises = new List<Exercise>() {
				new Exercise("13169004533", e, "6029832903", true),
				new Exercise("1690428486610429", e, "22496913456008", true),
				new Exercise("56341958081545199783", e, "17014716723435111315", true),
				new Exercise("6120215756887394998931731", e, "5077587957348826939798388"),
				new Exercise("514261067785300163931552303017", e, "357341101854457993054768343508"),
				new Exercise("21259593755515403367535773703117421", e, "18829051270422357250395121195166553"),
				new Exercise("1371108864054663830856429909460283182291", e, "35962927026249687666434209737424754460"),
			};

			Console.WriteLine("Exercises:");
			foreach (Exercise exercise in exercises) {
				Console.WriteLine(exercise);
			}
		}
	}
}
