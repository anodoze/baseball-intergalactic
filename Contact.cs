namespace Basedball
{
	public class Contact
	{
		public static ContactOutcome Simulate(Player batter, Random random)
		{
			// roll for Direction.
            var directionWeights = FieldDefaults.DefaultDirectionWeights[Handedness.Righty]; // todo: check batter handedness and assign here
            var batterDirectionWeights = new DirectionWeights(
                // leftLine: unsure
                leftGap: batter.Aim * 1.2f,
                leftCenter: batter.Aim * 0.5f,
                center: batter.Aim * 0.5f,
                rightCenter: batter.Aim * 0.5f,
                rightGap: batter.Aim * 1.2f
                //rightLine: unsure
            );
            directionWeights += batterDirectionWeights;
            var direction = directionWeights.RollDice(random);
            // roll for Angle
            var angleWeights = FieldDefaults.DefaultAngleWeights[direction];
            var batterAngleWeights = new AngleWeights(
                // grounder: unsure
                line: batter.Form * 1.2f,
                fly: batter.Form * 0.8f
                // popup: unsure
            );
            angleWeights += batterAngleWeights;
            var angle = angleWeights.RollDice(random);
            // roll for Force 
            var forceWeights = FieldDefaults.DefaultForceWeights[direction];
            var batterForceWeights = new ForceWeights(
                // weak: unsure
                clean: batter.Power * 0.5f,
                blast: batter.Power * 1.2f
            );
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
