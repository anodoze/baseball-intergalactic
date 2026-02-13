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
	}

	public enum ContactOutcome
	{
		Foul,
		BIP,
	}
}
