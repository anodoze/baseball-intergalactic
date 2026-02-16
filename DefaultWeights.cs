using Basedball;

public static class FieldDefaults
{
	public static readonly Dictionary<Direction, DefenderWeights> DefaultDefenderWeights = new()
	{
		[Direction.LeftLine] = new DefenderWeights( // 1 - Left foul line
			catcher: 0.03f,
			thirdBase: 0.20f,
			shortStop: 0.10f,
			leftField: 0.65f
		),
		[Direction.LeftGap] = new DefenderWeights( // 2 - Left-center gap
			catcher: 0.01f,
			thirdBase: 0.05f,
			shortStop: 0.28f,
			leftField: 0.48f,
			centerField: 0.15f
		),
		[Direction.LeftCenter] = new DefenderWeights( // 3 - Left of center
			pitcher: 0.04f,
			thirdBase: 0.02f,
			secondBase: 0.10f,
			shortStop: 0.38f,
			leftField: 0.28f,
			centerField: 0.18f
		),
		[Direction.Center] = new DefenderWeights( // 4 - Up the middle
			pitcher: 0.15f,
			catcher: 0.05f,
			firstBase: 0.01f,
			secondBase: 0.23f,
			thirdBase: 0.01f,
			shortStop: 0.23f,
			centerField: 0.32f
		),
		[Direction.RightCenter] = new DefenderWeights( // 5 - Right of center
			pitcher: 0.04f,
			catcher: 0.02f,
			firstBase: 0.10f,
			secondBase: 0.38f,
			centerField: 0.28f,
			rightField: 0.18f
		),
		[Direction.RightGap] = new DefenderWeights( // 6 - Right-center gap
			catcher: 0.01f,
			firstBase: 0.05f,
			secondBase: 0.28f,
			centerField: 0.15f,
			rightField: 0.48f
		),
		[Direction.RightLine] = new DefenderWeights( // 7 - Right foul line
			catcher: 0.03f,
			firstBase: 0.20f,
			secondBase: 0.10f,
			rightField: 0.65f
		),
	};

	public static readonly Dictionary<Handedness, DirectionWeights> DefaultDirectionWeights = new()
	{
		[Handedness.Lefty] = new DirectionWeights(
			leftLine: 1.2f,
			leftGap: 1.1f,
			leftCenter: 0.95f,
			center: 0.85f,
			rightCenter: 0.75f,
			rightGap: 0.65f,
			rightLine: 0.50f
		),
		[Handedness.Righty] = new DirectionWeights(
			leftLine: 0.50f,
			leftGap: 0.65f,
			leftCenter: 0.75f,
			center: 0.85f,
			rightCenter: 0.95f,
			rightGap: 1.1f,
			rightLine: 1.2f
		),
	};

	public static readonly Dictionary<Direction, AngleWeights> DefaultAngleWeights = new()
	{
		[Direction.LeftLine] = new AngleWeights(ground: 0.8f, line: 0.9f, fly: 1.2f, popup: 1.1f),
		[Direction.LeftGap] = new AngleWeights(ground: 1.0f, line: 1.2f, fly: 1.1f, popup: 0.8f),
		[Direction.LeftCenter] = new AngleWeights(ground: 0.9f, line: 1.1f, fly: 1.3f, popup: 0.7f),
		[Direction.Center] = new AngleWeights(ground: 1.1f, line: 1.0f, fly: 1.2f, popup: 0.8f),
		[Direction.RightCenter] = new AngleWeights(
			ground: 0.9f,
			line: 1.1f,
			fly: 1.3f,
			popup: 0.7f
		),
		[Direction.RightGap] = new AngleWeights(ground: 1.0f, line: 1.2f, fly: 1.1f, popup: 0.8f),
		[Direction.RightLine] = new AngleWeights(ground: 0.8f, line: 0.9f, fly: 1.2f, popup: 1.1f),
	};

	public static readonly Dictionary<Direction, ForceWeights> DefaultForceWeights = new()
	{
		[Direction.LeftLine] = new ForceWeights(weak: 1.3f, clean: 0.9f, blast: 0.8f),
		[Direction.LeftGap] = new ForceWeights(weak: 0.8f, clean: 1.3f, blast: 0.9f),
		[Direction.LeftCenter] = new ForceWeights(weak: 0.9f, clean: 1.1f, blast: 1.0f),
		[Direction.Center] = new ForceWeights(weak: 1.0f, clean: 1.0f, blast: 1.0f),
		[Direction.RightCenter] = new ForceWeights(weak: 0.9f, clean: 1.1f, blast: 1.0f),
		[Direction.RightGap] = new ForceWeights(weak: 0.8f, clean: 1.3f, blast: 0.9f),
		[Direction.RightLine] = new ForceWeights(weak: 1.3f, clean: 0.9f, blast: 0.8f),
	};

	public static readonly Dictionary<Angle, FieldingOutcomeWeights> DefaultFieldingWeights = new()
	{
		[Angle.Ground] = new FieldingOutcomeWeights(
			foul: 0f,
			caughtOut: 0f, // can't catch what's on the ground
			fielded: 1.5f,
			miss: 0.5f
		),
		[Angle.Line] = new FieldingOutcomeWeights(
			foul: 0f,
			caughtOut: 0.8f,
			fielded: 0.4f,
			miss: 0.8f
		),
		[Angle.Fly] = new FieldingOutcomeWeights(
			foul: 0f,
			caughtOut: 1.3f,
			fielded: 0.1f,
			miss: 0.6f
		),
		[Angle.Popup] = new FieldingOutcomeWeights(
			foul: 0f,
			caughtOut: 1.6f,
			fielded: 0.05f,
			miss: 0.35f
		),
	};

	public static readonly Dictionary<Force, FieldingOutcomeWeights> ForceFieldingModifiers = new()
	{
		[Force.Weak] = new FieldingOutcomeWeights(
			foul: 0f,
			caughtOut: 0.4f,
			fielded: 0.3f,
			miss: -0.3f
		),
		[Force.Clean] = new FieldingOutcomeWeights(),
		[Force.Blast] = new FieldingOutcomeWeights(
			foul: 0f,
			caughtOut: -0.4f,
			fielded: -0.4f,
			miss: 0.5f
		),
	};
}

public enum Direction
{
	LeftLine,
	LeftGap,
	LeftCenter,
	Center,
	RightCenter,
	RightGap,
	RightLine,
}

public enum Angle
{
	Ground,
	Line,
	Fly,
	Popup,
}

public enum Force
{
	Weak,
	Clean,
	Blast,
}

public enum FieldPosition
{
	Pitcher,
	Catcher,
	FirstBase,
	SecondBase,
	ThirdBase,
	ShortStop,
	LeftField,
	CenterField,
	RightField,
	Dugout,
}

public enum Handedness
{
	Lefty,
	Righty,
}

public enum FieldingOutcome
{
	Foul,
	CaughtOut,
	Fielded,
	Bobbled,
	Miss,
}
