namespace Basedball
{
	public class Game
	{
		public Team HomeTeam { get; }
		public Team AwayTeam { get; }
		private Dictionary<FieldPosition, int> _homeDefenseIndices;
		private Dictionary<FieldPosition, int> _awayDefenseIndices;
		private int _homePitcherIndex;
		private int _awayPitcherIndex;
		private Random _random;

		public Game(Team home, Team away, Random random)
		{
			HomeTeam = home;
			AwayTeam = away;
			_random = random;
			_homeDefenseIndices = InitializeDefenseIndices(home);
			_awayDefenseIndices = InitializeDefenseIndices(away);
			_homePitcherIndex = 0; // todo: choose starters based on stamina and ingame day
			_awayPitcherIndex = 0;
		}

		public void Play()
		{
			// TODO: full 9 innings with allowance for extras
			var inning = new Inning(
				AwayTeam,
				HomeTeam,
				_awayDefenseIndices,
				_awayPitcherIndex,
				_homeDefenseIndices,
				_homePitcherIndex,
				_random
			);
			inning.Play();
		}

		private Dictionary<FieldPosition, int> InitializeDefenseIndices(Team team)
		{
			var indices = new Dictionary<FieldPosition, int>();

			for (int i = 0; i < 8; i++)
			{
				var fieldPos = RosterToFieldPosition(team.PositionPlayers[i].RosterPosition);
				indices[fieldPos] = i;
			}

			return indices;
		}

		private static FieldPosition RosterToFieldPosition(RosterPosition roster)
		{
			return roster switch
			{
				RosterPosition.Catcher => FieldPosition.Catcher,
				RosterPosition.FirstBase => FieldPosition.FirstBase,
				RosterPosition.SecondBase => FieldPosition.SecondBase,
				RosterPosition.ThirdBase => FieldPosition.ThirdBase,
				RosterPosition.ShortStop => FieldPosition.ShortStop,
				RosterPosition.LeftField => FieldPosition.LeftField,
				RosterPosition.CenterField => FieldPosition.CenterField,
				RosterPosition.RightField => FieldPosition.RightField,
				_ => throw new ArgumentException($"No field position for {roster}"),
			};
		}
	}

	public class Inning
	{
		private Team _awayTeam;
		private Team _homeTeam;
		private Random _random;

		// TODO: bases, outs, score state

		private Dictionary<FieldPosition, int> _awayDefenseIndices;
		private int _awayPitcherIndex;
		private Dictionary<FieldPosition, int> _homeDefenseIndices;
		private int _homePitcherIndex;

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
			int outs = 0;
			int lineupIndex = 0;

			while (outs < 3)
			{
				var batter = batting.PositionPlayers[batting.BattingLineup[lineupIndex]];
				lineupIndex = (lineupIndex + 1) % 9;

				var pitcher = fielding.Pitchers[pitcherIndex];

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
			}
		}
	}
}
