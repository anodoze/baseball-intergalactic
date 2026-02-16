namespace Basedball
{
	public class Player
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public RosterPosition RosterPosition { get; set; }

		public float Durability { get; set; } // player's overall health/career state
		public float Composure { get; set; } // player's ingame mental state

		// Nature
		public float Vision { get; set; } // ball tracking
		public float Awareness { get; set; } // situational awareness/decisionmaking
		public float Reaction { get; set; } // raw reaction speed
		public float Power { get; set; } // hard throwing/batting/sprint acceleration
		public float Grace { get; set; } // clean movement and form. reduces injury chance.
		public float Speed { get; set; } // SPEED
		public float Stamina { get; set; } // mostly affects pitchers and catchers as pitch count goes up. if a game goes to extras... Watch Out

		public float Charisma { get; set; } // player interaction with fans
		public float Esprit { get; set; } // player interaction with teammates
		public float Aggression { get; set; } // interaction with opposing players - desire to steal bases, hit aggressively, attack fielding opportunities. will charge the mound. aggressive players draw out aggressive behavior in others
		public float Judgement { get; set; } // player's ability to notice and hold back from high-risk plays they can't execute, hold back from rising to others' Aggression
		public float Wisdom { get; set; } // player's ability to learn throughout the game
		public float Superstition { get; set; } // how much effect Luck has, in both directions
		public float Grit { get; set; } // how well a player can weather Unlucky events, improves performance at low Stamina

		// Skill
		// batting
		public float Discipline { get; set; } // Batter's ability to hold back outside the strike zone
		public float Attack { get; set; } // Batter's willingness to swing
		public float Contact { get; set; } // Batter's ability to Contact the ball when swinging
		public float Form { get; set; } // Batter's ability to leverage their Power into clean hits
		public float Aim { get; set; } // Batter's ability to hit the ball to an auspicious location
		public float Intimidation { get; set; } // Batter's ability to scare the Pitcher, throwing off their control to pitch to easier zones

		// pitching
		public float Deception { get; set; } // Pitcher's ability to hide their next pitch. on Catchers, improves framing ability. leverages Wisdom.
		public float Control { get; set; } // Pitcher's ability to control their pitch, throwing it to difficult zones
		public float Mechanics { get; set; } // Pitcher's ability to further leverage their Power and Grace in other skills
		public float Velocity { get; set; } // Pitcher's ability to throw fast, decreasing contact. leverages Power
		public float Movement { get; set; } // Pitcher's ability to create deceptive movement in a pitch, increasing strikes swinging out-of-zone and strikes looking generally Leverages Grace
		public float Presence { get; set; } // Pitcher's ability to scare the Batter, reducing the Power of hits
		public float Stuff { get; set; } // Pitcher's ability to induce poor-quality contact, reducing line drives and clean fly balls

		// baserunning
		public float Sprint { get; set; } // Batter's ability to leverage their Speed on the basepaths
		public float Performance { get; set; } // Batter's ability to induce fielding mistakes. clean slides. can sometimes dodge what would be an out. Leverages Reaction and Grace
		public float Sneak { get; set; } // Batter's ability to steal bases, leverages Power and Speed

		// defense
		public float Sense { get; set; } // Defender's Ability to understand the game state and make good decisions. Leverages Vision/Awareness
		public float Agility { get; set; } // Ability to get in position to catch the baseball, when hit or thrown. leverages SPEED/Reaction
		public float Acrobatics { get; set; } // Ability to chase and catch the baseball when in range. leverages Reaction. can compensate for low teammate Precision
		public float Arm { get; set; } // Ability to throw quickly, especially over long distances
		public float Dexterity { get; set; } // Ability to field squirrely ground balls, and tag out slippery baserunners
		public float Precision { get; set; } // Ability to throw with precision to other teammates

		public List<PitchType> Pitches { get; set; } = [];

		public Player(RosterPosition position, Random random)
		{
			Id = Guid.NewGuid().ToString();
			FirstName = NameData.firstNames[random.Next(NameData.firstNames.Length)];
			LastName = NameData.lastNames[random.Next(NameData.lastNames.Length)];
			RosterPosition = position;

			var allPitches = Enum.GetValues<PitchType>();
			var numPitches = random.Next(1, 5);
			var shuffled = allPitches.OrderBy(x => random.Next()).Take(numPitches);
			Pitches.AddRange(shuffled);

			Durability = 100;
			Composure = random.NextSingle() * 0.1f;

			Vision = random.NextSingle() * 0.1f;
			Awareness = random.NextSingle() * 0.1f;
			Reaction = random.NextSingle() * 0.1f;
			Power = random.NextSingle() * 0.1f;
			Grace = random.NextSingle() * 0.1f;
			Speed = random.NextSingle() * 0.1f;
			Stamina = random.NextSingle() * 0.1f;

			Charisma = random.NextSingle() * 0.1f;
			Esprit = random.NextSingle() * 0.1f;
			Aggression = random.NextSingle() * 0.1f;
			Judgement = random.NextSingle() * 0.1f;
			Wisdom = random.NextSingle() * 0.1f;
			Superstition = random.NextSingle() * 0.1f;
			Grit = random.NextSingle() * 0.1f;

			Discipline = random.NextSingle() * 0.1f;
			Attack = random.NextSingle() * 0.1f;
			Contact = random.NextSingle() * 0.1f;
			Form = random.NextSingle() * 0.1f;
			Aim = random.NextSingle() * 0.1f;
			Intimidation = random.NextSingle() * 0.1f;

			Deception = random.NextSingle() * 0.1f;
			Mechanics = random.NextSingle() * 0.1f;
			Velocity = random.NextSingle() * 0.1f;
			Control = random.NextSingle() * 0.1f;
			Movement = random.NextSingle() * 0.1f;
			Stuff = random.NextSingle() * 0.1f;
			Presence = random.NextSingle() * 0.1f;

			Sprint = random.NextSingle() * 0.1f;
			Performance = random.NextSingle() * 0.1f;
			Sneak = random.NextSingle() * 0.1f;

			Sense = random.NextSingle() * 0.1f;
			Agility = random.NextSingle() * 0.1f;
			Acrobatics = random.NextSingle() * 0.1f;
			Arm = random.NextSingle() * 0.1f;
			Dexterity = random.NextSingle() * 0.1f;
			Precision = random.NextSingle() * 0.1f;
		}
	}

	public enum RosterPosition
	{
		StartingPitcher,
		ReliefPitcher,
		Closer,
		Catcher,
		FirstBase,
		SecondBase,
		ThirdBase,
		ShortStop,
		LeftField,
		CenterField,
		RightField,
		DesignatedHitter,
		BenchBatter,
		BenchPitcher,
	}

	public enum PitchType
	{
		Fastball,
		Changeup,
		Curveball,
		Slider,
	}
}
