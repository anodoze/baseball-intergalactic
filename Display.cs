using Basedball;

public static class Display
{
	public static void HalfInningStart(string battingTeam, Player pitcher)
	{
		Console.WriteLine();
		Console.WriteLine(
			$"=== {battingTeam} Batting | {pitcher.FirstName} {pitcher.LastName} Pitching ==="
		);
		Console.WriteLine();
	}

	public static void NowBatting(Player batter)
	{
		Console.WriteLine($"Now Batting: {batter.FirstName} {batter.LastName}");
	}

	public static void Pitch(PitchResult result, int balls, int strikes)
	{
		string outcome = result.Outcome switch
		{
			PitchOutcome.Ball => "Ball",
			PitchOutcome.StrikeLooking => "Strike, looking",
			PitchOutcome.StrikeSwinging => "Strike, swinging",
			PitchOutcome.Contact => "Contact", // shouldn't display this one directly
			_ => result.Outcome.ToString(),
		};

		Console.WriteLine($"{outcome}. {balls}-{strikes}");
	}

	public static void Foul(int strikes)
	{
		Console.WriteLine($"Foul ball. {strikes} strikes");
	}

	public static void ContactResult(
		Player batter,
		ContactInfo contact,
		FieldingAttempt attempt,
		ContactOutcome outcome
	)
	{
		// First line: what happened with the bat
		string angleDesc = contact.Angle switch
		{
			Angle.Ground => "grounds",
			Angle.Line => "lines",
			Angle.Fly => "hits a fly ball",
			Angle.Popup => "pops up",
			_ => "hits",
		};

		string directionDesc = contact.Direction switch
		{
			Direction.LeftLine => "down the left line",
			Direction.LeftGap => "into the left-center gap",
			Direction.LeftCenter => "to left-center",
			Direction.Center => "up the middle",
			Direction.RightCenter => "to right-center",
			Direction.RightGap => "into the right-center gap",
			Direction.RightLine => "down the right line",
			_ => "",
		};

		Console.WriteLine($"{batter.FirstName} {batter.LastName} {angleDesc} {directionDesc}.");

		// Second line: what happened with the defense
		if (outcome == ContactOutcome.Foul)
		{
			Console.WriteLine("Foul ball.");
			return;
		}

		string fielderPos = attempt.PrimaryFielder.RosterPosition switch
		{
			RosterPosition.Catcher => "C",
			RosterPosition.FirstBase => "1B",
			RosterPosition.SecondBase => "2B",
			RosterPosition.ThirdBase => "3B",
			RosterPosition.ShortStop => "SS",
			RosterPosition.LeftField => "LF",
			RosterPosition.CenterField => "CF",
			RosterPosition.RightField => "RF",
			RosterPosition.StartingPitcher
			or RosterPosition.ReliefPitcher
			or RosterPosition.Closer => "P",
			_ => attempt.PrimaryFielder.RosterPosition.ToString(),
		};

		if (outcome == ContactOutcome.Out)
		{
			string outType = contact.Angle switch
			{
				Angle.Ground => "grounds out",
				Angle.Line => "lines out",
				Angle.Fly => "flies out",
				Angle.Popup => "pops out",
				_ => "is out",
			};
			Console.WriteLine(
				$"{batter.FirstName} {batter.LastName} {outType} to {fielderPos} {attempt.PrimaryFielder.FirstName} {attempt.PrimaryFielder.LastName}."
			);
		}
		else if (outcome == ContactOutcome.Safe)
		{
			Console.WriteLine($"{batter.FirstName} {batter.LastName} reaches safely."); // TODO: single/double/triple once we track that
		}
	}

	public static void Strikeout(Player batter)
	{
		Console.WriteLine($"{batter.FirstName} {batter.LastName} strikes out.");
		Console.WriteLine();
	}

	public static void Walk(Player batter)
	{
		Console.WriteLine($"{batter.FirstName} {batter.LastName} walks.");
		Console.WriteLine();
	}

	public static void Error(string message)
	{
		Console.WriteLine($"ERROR: {message}");
	}
}
