namespace Basedball
{
	public class Contact
	{
		public static ContactOutcome Simulate(Player batter, Random random)
		{
            // roll for Direction.
            var directionWeights = FieldDefaults.DefaultDirectionWeights[Handedness.Righty];
            var batterDirectionWeights = new DirectionWeights(
                leftLine: -(batter.Aim - 0.5f) * 1.5f,     // low aim = more fouls
                leftGap: (batter.Aim - 0.5f) * 2.4f,       // aim for the money spots
                leftCenter: (batter.Aim - 0.5f) * 1.0f,
                center: 0f,                                 // center is always neutral
                rightCenter: (batter.Aim - 0.5f) * 1.0f,
                rightGap: (batter.Aim - 0.5f) * 2.4f,
                rightLine: -(batter.Aim - 0.5f) * 1.5f
            );
            directionWeights += batterDirectionWeights;
            var direction = directionWeights.RollDice(random);

            // roll for Angle
            var angleWeights = FieldDefaults.DefaultAngleWeights[direction];
            var batterAngleWeights = new AngleWeights(
                ground: -(batter.Form - 0.5f) * 1.8f,      // bad form tops the ball
                line: (batter.Form - 0.5f) * 2.4f,         // clean barrel work
                fly: (batter.Form - 0.5f) * 1.6f,          // elevate properly
                popup: -(batter.Form - 0.5f) * 1.2f        // mishit under it
            );
            angleWeights += batterAngleWeights;
            var angle = angleWeights.RollDice(random);

            // roll for Force 
            var forceWeights = FieldDefaults.DefaultForceWeights[direction];
            var batterForceWeights = new ForceWeights(
                weak: -(batter.Power - 0.5f) * 2.0f,       // strong guys rarely dribble
                clean: (batter.Power - 0.5f) * 1.0f,       // moderate scaling
                blast: (batter.Power - 0.5f) * 2.4f        // power shows up here
            );
            forceWeights += batterForceWeights;
            var force = forceWeights.RollDice(random);
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
	}

	public enum ContactOutcome
	{
		Foul,
		BIP,
	}
}
