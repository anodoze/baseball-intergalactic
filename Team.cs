namespace Basedball
{
	public class Team
	{
		public string Name { get; }
		public Player[] PositionPlayers { get; }
		public Player[] Pitchers { get; }
		public int[] BattingLineup { get; set; }

		public Team(string name, Player[] positionPlayers, Player[] pitchers)
		{
			Name = name;
			PositionPlayers = positionPlayers;
			Pitchers = pitchers;
			BattingLineup = [0, 1, 2, 3, 4, 5, 6, 7, 8];
		}
	}

	public static class TeamGenerator
	{
		public static Team GenerateTeam(string name, Random random)
		{
			var positionPlayers = new Player[14];
			var pitchers = new Player[14];

			var positions = new[]
			{
				RosterPosition.Catcher,
				RosterPosition.FirstBase,
				RosterPosition.SecondBase,
				RosterPosition.ThirdBase,
				RosterPosition.ShortStop,
				RosterPosition.LeftField,
				RosterPosition.CenterField,
				RosterPosition.RightField,
				RosterPosition.DesignatedHitter,
			};

			for (int i = 0; i < 9; i++)
			{
				positionPlayers[i] = new Player(positions[i], random);
			}

			for (int i = 9; i < 14; i++)
			{
				positionPlayers[i] = new Player(RosterPosition.BenchBatter, random);
			}

			for (int i = 0; i < 5; i++)
			{
				pitchers[i] = new Player(RosterPosition.StartingPitcher, random);
			}

			for (int i = 5; i < 9; i++)
			{
				pitchers[i] = new Player(RosterPosition.ReliefPitcher, random);
			}

			for (int i = 9; i < 14; i++)
			{
				pitchers[i] = new Player(RosterPosition.BenchPitcher, random);
			}

			return new Team(name, positionPlayers, pitchers);
		}
	}
}
