using Basedball;

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
