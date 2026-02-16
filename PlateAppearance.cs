using Basedball;

public class PlateAppearance
{
	public static PAOutcome Simulate(
		Player batter,
		Player pitcher,
		Team fielding,
		Dictionary<FieldPosition, int> defenseIndices,
		Random random
	)
	{
		Display.NowBatting(batter);

		int ballCount = 0;
		int strikeCount = 0;

		while (strikeCount < 3 && ballCount < 4)
		{
			var pitchResult = Pitch.ThrowPitch(batter, pitcher, random);

			if (pitchResult.Outcome is PitchOutcome.StrikeLooking or PitchOutcome.StrikeSwinging)
			{
				strikeCount++;
				Display.Pitch(pitchResult, ballCount, strikeCount);
				if (strikeCount >= 3)
				{
					Display.Strikeout(batter);
					return PAOutcome.Strikeout;
				}
			}
			else if (pitchResult.Outcome == PitchOutcome.Ball)
			{
				ballCount++;
				Display.Pitch(pitchResult, ballCount, strikeCount);
				if (ballCount >= 4)
				{
					Display.Walk(batter);
					return PAOutcome.Walk;
				}
			}
			else if (pitchResult.Outcome is PitchOutcome.Contact)
			{
				var contactInfo = Contact.GenerateContactInfo(batter, random);
				var fieldingAttempt = Contact.PrepareFieldingAttempt(
					contactInfo,
					fielding,
					defenseIndices,
					random
				);
				var outcome = Contact.ResolveFielding(
					fieldingAttempt,
					fielding,
					defenseIndices,
					random
				);

				if (outcome == ContactOutcome.Foul)
				{
					if (strikeCount < 2)
						strikeCount++;
					Display.Foul(strikeCount);
				}
				else
				{
					Display.ContactResult(batter, contactInfo, fieldingAttempt, outcome);
					Console.WriteLine();

					if (outcome == ContactOutcome.Out)
						return PAOutcome.Out;
					else if (outcome == ContactOutcome.Safe)
						return PAOutcome.Safe;
				}
			}
		}

		Display.Error("Plate Appearance loop exited without resolution");
		return PAOutcome.SimError;
	}
}

public class Pitch
{
	private static readonly Dictionary<int, ZoneWeights> DefaultZoneWeights = new()
	{
		// Heart of the plate (middle-middle) - most contact
		[5] = new ZoneWeights(looking: 0.15f, contact: 0.70f, swinging: 0.15f),
		// Middle edges - good contact zones
		[2] = new ZoneWeights(looking: 0.20f, contact: 0.60f, swinging: 0.20f),
		[4] = new ZoneWeights(looking: 0.25f, contact: 0.55f, swinging: 0.20f),
		[6] = new ZoneWeights(looking: 0.25f, contact: 0.55f, swinging: 0.20f),
		[8] = new ZoneWeights(looking: 0.20f, contact: 0.60f, swinging: 0.20f),
		// Corners - harder to hit, more whiffs and looking strikes
		[1] = new ZoneWeights(looking: 0.35f, contact: 0.40f, swinging: 0.25f),
		[3] = new ZoneWeights(looking: 0.30f, contact: 0.45f, swinging: 0.25f),
		[7] = new ZoneWeights(looking: 0.30f, contact: 0.45f, swinging: 0.25f),
		[9] = new ZoneWeights(looking: 0.35f, contact: 0.40f, swinging: 0.25f),
		// Chase zones - outside but tempting
		[11] = new ZoneWeights(ball: 0.40f, contact: 0.35f, swinging: 0.25f), // High
		[12] = new ZoneWeights(ball: 0.50f, contact: 0.30f, swinging: 0.20f), // Away (RHH vs RHP)
		[13] = new ZoneWeights(ball: 0.45f, contact: 0.35f, swinging: 0.20f), // Low
		[14] = new ZoneWeights(ball: 0.55f, contact: 0.25f, swinging: 0.20f), // Inside (RHH vs RHP)
	};

	public static PitchResult ThrowPitch(Player pitcher, Player batter, Random random)
	{
		// select pitch
		// balk?
		// select zone
		// int cornerPick = new[] { 1, 3, 7, 9 }[_random.Next(4)];
		int zonePick = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14 }[random.Next(13)];
		var zoneWeights = DefaultZoneWeights[zonePick];
		var batterWeights = new ZoneWeights(
			batter.Discipline * 2.0f,
			-batter.Attack,
			batter.Contact,
			batter.Attack * 0.5f
		);

		var pitcherWeights = new ZoneWeights(
			-(pitcher.Movement * 0.5f),
			pitcher.Movement,
			-pitcher.Velocity,
			pitcher.Movement
		);

		zoneWeights += batterWeights;
		zoneWeights += pitcherWeights;

		if (zonePick is 11 or 12 or 13 or 14)
		{
			zoneWeights.Looking = 0;
		}
		else
		{
			zoneWeights.Ball = 0;
		}

		var outcome = zoneWeights.RollDice(random);
		return new PitchResult { Outcome = outcome, Zone = zonePick };
	}
	// todo: add foul tips at some point?
	// runners may attempt to steal!
	// notice?
	// throw them out?
}

public enum PitchOutcome
{
	StrikeLooking,
	StrikeSwinging,
	Ball,
	Contact,
	Balk,
}

public struct PitchResult
{
	public PitchOutcome Outcome { get; init; }
	public int Zone { get; init; }
	// public PitchType PitchType { get; init; }
	// pitch speed
}

public enum PAOutcome
{
	Strikeout,
	Walk,
	SimError,
	Out,
	Safe,
}
//RUNNERS
// judgement: try to advance?
// speed: advancement success
// performance: lowers defense effectiveness, can sneak under what would otherwise be a tagout
