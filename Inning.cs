using Basedball;

public class Inning
{
	private Team _awayTeam;
	private Team _homeTeam;
	private Random _random;

	private Dictionary<FieldPosition, int> _awayDefenseIndices;
	private int _awayPitcherIndex;
	private Dictionary<FieldPosition, int> _homeDefenseIndices;
	private int _homePitcherIndex;

	private Dictionary<Base, int> _runners;

	public Inning(
		Team away,
		Team home,
		Dictionary<FieldPosition, int> awayDefense,
		int awayPitcher,
		Dictionary<FieldPosition, int> homeDefense,
		int homePitcher,
		Random random
	)
	{
		_awayTeam = away;
		_homeTeam = home;
		_awayDefenseIndices = awayDefense;
		_awayPitcherIndex = awayPitcher;
		_homeDefenseIndices = homeDefense;
		_homePitcherIndex = homePitcher;
		_random = random;
		_runners = new Dictionary<Base, int>();
	}

	public void Play()
	{
		PlayHalf(_awayTeam, _homeTeam, _homeDefenseIndices, _homePitcherIndex); // top
		PlayHalf(_homeTeam, _awayTeam, _awayDefenseIndices, _awayPitcherIndex); // bottom
	}

	private void PlayHalf(
		Team batting,
		Team fielding,
		Dictionary<FieldPosition, int> defenseIndices,
		int pitcherIndex
	)
	{
		var pitcher = fielding.Pitchers[pitcherIndex];
		Display.HalfInningStart(batting.Name, pitcher);

		int outs = 0;
		int lineupIndex = 0;
		_runners = new Dictionary<Base, int>();

		while (outs < 3)
		{
			var batterIndex = batting.BattingLineup[lineupIndex];
			var batter = batting.PositionPlayers[batterIndex];
			lineupIndex = (lineupIndex + 1) % 9;

			var paOutcome = PlateAppearance.Simulate(
				batter,
				pitcher,
				fielding,
				defenseIndices,
				_random
			);

			if (paOutcome == PAOutcome.Strikeout || paOutcome == PAOutcome.Out)
			{
				outs++;
			}
			else if (paOutcome == PAOutcome.Safe)
			{
				// todo: update PAOutcomes to return rich field information including base state. Fielding logic lives in Contact.cs
			}
		}
	}
}

public enum Base
{
	First,
	Second,
	Third,
}
