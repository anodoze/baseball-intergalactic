namespace Basedball
{
	public class Contact
	{
		public static ContactOutcome Simulate(Random random)
		{
			//direction
			//hit type
			//force modification
			//defender modification
			//for now, just random roll between foul, caught, and hit. oh brother is field logic going to be intense

			int go = random.Next(2);
			if (go < 1)
				return ContactOutcome.Foul;
			return ContactOutcome.BIP;
		}

		private static readonly Dictionary<int, DefenderWeights> DefaultDefenderWeights = new()
		{
			[1] = new DefenderWeights( // 1 - Left foul line
				pitcher: 0.02f,
				catcher: 0.03f,
				thirdBase: 0.20f,
				shortStop: 0.10f,
				leftField: 0.65f
			),
			[2] = new DefenderWeights( // 2 - Left-center gap
				pitcher: 0.03f,
				catcher: 0.01f,
				thirdBase: 0.05f,
				shortStop: 0.28f,
				leftField: 0.48f,
				centerField: 0.15f
			),
			[3] = new DefenderWeights( // 3 - Left of center
				pitcher: 0.04f,
				thirdBase: 0.02f,
				secondBase: 0.10f,
				shortStop: 0.38f,
				leftField: 0.28f,
				centerField: 0.18f
			),
			[4] = new DefenderWeights( // 4 - Up the middle
				pitcher: 0.15f,
				catcher: 0.05f,
				firstBase: 0.01f,
				secondBase: 0.23f,
				thirdBase: 0.01f,
				shortStop: 0.23f,
				centerField: 0.32f
			),
			[5] = new DefenderWeights( // 5 - Right of center
				pitcher: 0.04f,
				catcher: 0.02f,
				firstBase: 0.10f,
				secondBase: 0.38f,
				centerField: 0.28f,
				rightField: 0.18f
			),
			[6] = new DefenderWeights( // 6 - Right-center gap
				pitcher: 0.03f,
				catcher: 0.01f,
				firstBase: 0.05f,
				secondBase: 0.28f,
				centerField: 0.15f,
				rightField: 0.48f
			),
			[7] = new DefenderWeights( // 7 - Right foul line
				pitcher: 0.02f,
				catcher: 0.03f,
				firstBase: 0.20f,
				secondBase: 0.10f,
				rightField: 0.65f
			),
		};

		private static readonly Dictionary<int, DirectionWeights> DefaultDirectionWeights = new()
		{
			[1] = new DirectionWeights( // righty
				leftLine: 1.2f,
				leftField: 1.1f,
				leftCenterField: 0.95f,
				center: 0.85f,
				rightCenterField: 0.75f,
				rightField: 0.65f,
				rightLine: 0.50f
			),
			[2] = new DirectionWeights( //lefty
				leftLine: 0.50f,
				leftField: 0.65f,
				leftCenterField: 0.75f,
				center: 0.85f,
				rightCenterField: 0.95f,
				rightField: 1.1f,
				rightLine: 1.2f
			),
		};
	}

	public enum ContactOutcome
	{
		Foul,
		BIP,
	}
}
