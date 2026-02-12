namespace Basedball
{
	public class Contact
	{
		public static ContactOutcome Simulate(Random random)
		{
			// roll for Direction. There are base weights depending on handedness (we're in all-righties world for now), Batters with high Aim are more likely to hit through the gaps. Batters with low Form are more scattershot, liable to foul off. Direction will set the base weights for which fielders might handle the ball.
            // roll for Angle - batters with high Form are more likely to hit lines and flies, players with low Form will hit grounders and popups. This modifies the field weights - your catcher is probably not going after fly balls. The hit type determines what skills are needed to field the ball, and is a major part of determining how hard it is to field.
            // roll for Force (trying to avoid namespace collision with Power, the BODY attribute here). This is determined almost entirely by the batter's Power. This will make one last push on the responsible-fielder weights -- is this fly ball hovering over the shortstop or the CF -- or is it out of the park? this also impacts the difficulty of fielding substantially - the difference between a dribbler and a sharp hop to the outfield. (thought --for airborne stuff, having more power seems just good, but for ground balls, shitty two-foot dribblers and long-rolling outfield hits both seem better for the batter than a "clean" hit right to the shortstop, does that make sense to include?)
            // we now have:
            // the weights to pick the primary fielder (and know the secondary if necessary)
            // the difficulty of the fielding attempt (hit type, power)
            // The context to determine outcomes (from direction: mainly relevant for directions 1 and 7, where potential fouls need to be taken into account)
            // possible fielding results:
            // Catch out (weighted 0 on ground ball) - straightforward check against defense attributes
            // Foul (weighted 0 for directions outside 1 and 7) - most checks here should be easy, but I do want to involve Awareness and Sense here. A defender should be more eager to catch a foul with empty bases than with a guy on third who might try to run in if you make that ball live.
            // Fielded (for now we'll just coinflip hit/out, we will add misses that have to get picked up by other defenders later, and we can incorporate the throwing attributes as the last step before we start tracking baserunners, since getting a guy out in part depends on how fast he can run)
			
            int go = random.Next(2);
			if (go < 1)
				return ContactOutcome.Foul;
			return ContactOutcome.BIP;
		}

		private static readonly Dictionary<int, DefenderWeights> DefaultDefenderWeights = new()
		{
			[1] = new DefenderWeights( // 1 - Left foul line
				catcher: 0.03f,
				thirdBase: 0.20f,
				shortStop: 0.10f,
				leftField: 0.65f
			),
			[2] = new DefenderWeights( // 2 - Left-center gap
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
				catcher: 0.01f,
				firstBase: 0.05f,
				secondBase: 0.28f,
				centerField: 0.15f,
				rightField: 0.48f
			),
			[7] = new DefenderWeights( // 7 - Right foul line
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
				leftGap: 1.1f,
				leftCenter: 0.95f,
				center: 0.85f,
				rightCenter: 0.75f,
				rightGap: 0.65f,
				rightLine: 0.50f
			),
			[2] = new DirectionWeights( //lefty
				leftLine: 0.50f,
				leftGap: 0.65f,
				leftCenter: 0.75f,
				center: 0.85f,
				rightCenter: 0.95f,
				rightGap: 1.1f,
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
