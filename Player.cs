using Basedball;

public class Player
{
	public string Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public RosterPosition RosterPosition { get; set; }

	public float Durability { get; set; }
	public float Composure { get; set; }

	// Nature
	// BODY
	public float Vision { get; set; }
	public float Awareness { get; set; }
	public float Reaction { get; set; }
	public float Power { get; set; }
	public float Grace { get; set; }
	public float Speed { get; set; }
	public float Stamina { get; set; }

	//MIND
	public float Charisma { get; set; }
	public float Esprit { get; set; }
	public float Aggression { get; set; }
	public float Judgement { get; set; }
	public float Wisdom { get; set; }
	public float Superstition { get; set; }
	public float Grit { get; set; }

	// Skill
	// batting
	public float Discipline { get; set; }
	public float Attack { get; set; }
	public float Contact { get; set; }
	public float Form { get; set; }
	public float Aim { get; set; }
	public float Intimidation { get; set; }

	// pitching
	public float Deception { get; set; }
	public float Control { get; set; }
	public float Mechanics { get; set; }
	public float Velocity { get; set; }
	public float Movement { get; set; }
	public float Presence { get; set; }
	public float Stuff { get; set; }

	// baserunning
	public float Sprint { get; set; }
	public float Performance { get; set; }
	public float Sneak { get; set; }

	// defense
	public float Sense { get; set; }
	public float Agility { get; set; }
	public float Acrobatics { get; set; }
	public float Arm { get; set; }
	public float Dexterity { get; set; }
	public float Precision { get; set; }

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

		Durability = 100; // placeholder
		Composure = 100; // placeholder

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
