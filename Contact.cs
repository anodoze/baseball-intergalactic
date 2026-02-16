using Basedball;

public class Contact
{
	public static ContactInfo GenerateContactInfo(Player batter, Random random)
	{
		var directionWeights = FieldDefaults.DefaultDirectionWeights[Handedness.Righty];
		var batterDirectionWeights = new DirectionWeights(
			leftLine: -(batter.Aim - 0.5f) * 1.5f,
			leftGap: (batter.Aim - 0.5f) * 2.4f,
			leftCenter: (batter.Aim - 0.5f) * 1.0f,
			center: 0f,
			rightCenter: (batter.Aim - 0.5f) * 1.0f,
			rightGap: (batter.Aim - 0.5f) * 2.4f,
			rightLine: -(batter.Aim - 0.5f) * 1.5f
		);
		directionWeights += batterDirectionWeights;
		var direction = directionWeights.RollDice(random);

		var angleWeights = FieldDefaults.DefaultAngleWeights[direction];
		var batterAngleWeights = new AngleWeights(
			ground: -(batter.Form - 0.5f) * 1.8f,
			line: (batter.Form - 0.5f) * 2.4f,
			fly: (batter.Form - 0.5f) * 1.6f,
			popup: -(batter.Form - 0.5f) * 1.2f
		);
		angleWeights += batterAngleWeights;
		var angle = angleWeights.RollDice(random);

		var forceWeights = FieldDefaults.DefaultForceWeights[direction];
		var batterForceWeights = new ForceWeights(
			weak: -(batter.Power - 0.5f) * 2.0f,
			clean: (batter.Power - 0.5f) * 1.0f,
			blast: (batter.Power - 0.5f) * 2.4f
		);
		forceWeights += batterForceWeights;
		var force = forceWeights.RollDice(random);

		return new ContactInfo(direction, angle, force);
	}

	public static ContactOutcome ResolveFielding(
		FieldingAttempt fieldingAttempt,
		Team fielding,
		Dictionary<FieldPosition, int> defenseIndices,
		Random random
	)
	{
		var primaryOutcome = RollFieldingOutcome(fieldingAttempt, random);

		// TODO: Display fielding flavor based on primaryOutcome here eventually

		return primaryOutcome switch
		{
			FieldingOutcome.Foul => ContactOutcome.Foul,
			FieldingOutcome.CaughtOut => ContactOutcome.Out,
			FieldingOutcome.Fielded => ResolveThrow(
				fieldingAttempt,
				fielding,
				defenseIndices,
				random
			),
			FieldingOutcome.Miss when fieldingAttempt.SecondaryFielder != null =>
				ResolveSecondaryFielder(fieldingAttempt, fielding, defenseIndices, random),
			FieldingOutcome.Miss => ContactOutcome.Safe,
			_ => throw new InvalidOperationException(
				$"Unexpected fielding outcome: {primaryOutcome}"
			),
		};
	}

	private static FieldingOutcome RollFieldingOutcome(FieldingAttempt attempt, Random random)
	{
		var weights = FieldDefaults.DefaultFieldingWeights[attempt.Contact.Angle];
		var forceModifier = FieldDefaults.ForceFieldingModifiers[attempt.Contact.Force];
		weights += forceModifier;
		if (
			attempt.Contact.Direction != Direction.LeftLine
			&& attempt.Contact.Direction != Direction.RightLine
		)
		{
			weights = new FieldingOutcomeWeights(
				foul: 0f,
				caughtOut: weights.CaughtOut,
				fielded: weights.Fielded,
				miss: weights.Miss
			);
		}
		// TODO: check fielder attribute modifiers (Agility, Acrobatics, etc.)
		return weights.RollDice(random);
	}

	public static FieldingAttempt PrepareFieldingAttempt(
		ContactInfo contact,
		Team fielders,
		Dictionary<FieldPosition, int> defenseIndices,
		Random random
	)
	{
		var defenderWeights = FieldDefaults.DefaultDefenderWeights[contact.Direction];

		var angleModifier = contact.Angle switch
		{
			Angle.Ground => new DefenderWeights(
				pitcher: 0.05f,
				catcher: 0.15f,
				firstBase: 0.10f,
				secondBase: 0.10f,
				thirdBase: 0.10f,
				shortStop: 0.10f,
				leftField: -0.25f,
				centerField: -0.25f,
				rightField: -0.25f
			),
			Angle.Line => new DefenderWeights(),
			Angle.Fly => new DefenderWeights(
				pitcher: -0.10f,
				catcher: -0.15f,
				firstBase: -0.05f,
				secondBase: -0.05f,
				thirdBase: -0.05f,
				shortStop: -0.05f,
				leftField: 0.20f,
				centerField: 0.20f,
				rightField: 0.20f
			),
			Angle.Popup => new DefenderWeights(
				pitcher: 0.05f,
				catcher: 0.20f,
				firstBase: 0.05f,
				secondBase: 0.05f,
				thirdBase: 0.05f,
				shortStop: 0.05f,
				leftField: -0.15f,
				centerField: -0.15f,
				rightField: -0.15f
			),
			_ => new DefenderWeights(),
		};

		var forceModifier = contact.Force switch
		{
			Force.Weak => new DefenderWeights(
				pitcher: 0.10f,
				catcher: 0.15f,
				firstBase: 0.05f,
				secondBase: 0.05f,
				thirdBase: 0.05f,
				shortStop: 0.05f,
				leftField: -0.15f,
				centerField: -0.15f,
				rightField: -0.15f
			),
			Force.Clean => new DefenderWeights(),
			Force.Blast => new DefenderWeights(
				pitcher: -0.10f,
				catcher: -0.15f,
				firstBase: -0.05f,
				secondBase: -0.05f,
				thirdBase: -0.05f,
				shortStop: -0.05f,
				leftField: 0.15f,
				centerField: 0.15f,
				rightField: 0.15f
			),
			_ => new DefenderWeights(),
		};

		defenderWeights += angleModifier + forceModifier;

		var primaryPosition = defenderWeights.RollDice(random);
		var secondaryPosition = GetBackupFielder(primaryPosition, contact.Direction, contact.Angle);

		var primaryFielder = GetFielderAtPosition(fielders, defenseIndices, primaryPosition);
		var secondaryFielder = secondaryPosition.HasValue
			? GetFielderAtPosition(fielders, defenseIndices, secondaryPosition.Value)
			: null;

		if (primaryFielder == null)
			throw new InvalidOperationException($"No fielder found at {primaryPosition}");

		return new FieldingAttempt
		{
			PrimaryFielder = primaryFielder,
			SecondaryFielder = secondaryFielder,
			Contact = contact,
		};
	}

	private static ContactOutcome ResolveThrow(
		FieldingAttempt attempt,
		Team fielding,
		Dictionary<FieldPosition, int> defenseIndices,
		Random random
	)
	{
		// TODO: PLACEHOLDER - implement actual throw mechanics based on distance, Arm, Precision
		var isInfield =
			attempt.PrimaryFielder.RosterPosition
			is RosterPosition.Catcher
				or RosterPosition.FirstBase
				or RosterPosition.SecondBase
				or RosterPosition.ThirdBase
				or RosterPosition.ShortStop;

		return isInfield ? ContactOutcome.Out : ContactOutcome.Safe;
	}

	private static ContactOutcome ResolveSecondaryFielder(
		FieldingAttempt attempt,
		Team fielding,
		Dictionary<FieldPosition, int> defenseIndices,
		Random random
	)
	{
		var weights = FieldDefaults.DefaultFieldingWeights[attempt.Contact.Angle];

		// Backup is scrambling - much harder to field cleanly
		var backupModifier = new FieldingOutcomeWeights(
			foul: 0f, // no fouls on secondary attempt - do we need to formally clamp this?
			caughtOut: -0.3f,
			fielded: -0.5f,
			miss: 0.4f
		);
		weights += backupModifier;

		var outcome = weights.RollDice(random);

		return outcome switch
		{
			FieldingOutcome.CaughtOut => ContactOutcome.Out,
			FieldingOutcome.Fielded => ContactOutcome.Safe, // too late for the out
			_ => ContactOutcome.Safe, // missed
		};
	}

	public static FieldPosition? GetBackupFielder(
		FieldPosition primary,
		Direction direction,
		Angle angle
	)
	{
		if (
			primary
			is FieldPosition.LeftField
				or FieldPosition.CenterField
				or FieldPosition.RightField
		)
			return null;

		if (primary == FieldPosition.Catcher)
		{
			if (angle == Angle.Ground)
			{
				return direction switch
				{
					Direction.LeftLine or Direction.LeftGap or Direction.LeftCenter =>
						FieldPosition.ThirdBase,
					Direction.Center => FieldPosition.FirstBase, // TODO: add choice when there are baserunners
					Direction.RightCenter or Direction.RightGap or Direction.RightLine =>
						FieldPosition.FirstBase,
					_ => null,
				};
			}
			return null;
		}

		return (primary, direction) switch
		{
			(FieldPosition.ThirdBase, _) => FieldPosition.LeftField,
			(FieldPosition.ShortStop, Direction.LeftLine or Direction.LeftGap) =>
				FieldPosition.LeftField,
			(FieldPosition.ShortStop, Direction.LeftCenter or Direction.Center) =>
				FieldPosition.CenterField,
			(FieldPosition.SecondBase, Direction.RightLine or Direction.RightGap) =>
				FieldPosition.RightField,
			(FieldPosition.SecondBase, Direction.RightCenter or Direction.Center) =>
				FieldPosition.CenterField,
			(FieldPosition.FirstBase, _) => FieldPosition.RightField,
			(FieldPosition.Pitcher, _) => FieldPosition.CenterField,
			_ => null,
		};
	}

	private static Player GetFielderAtPosition(
		Team team,
		Dictionary<FieldPosition, int> defenseIndices,
		FieldPosition position
	)
	{
		if (position == FieldPosition.Pitcher)
			return team.Pitchers[0]; // TODO: choose pitcher index at Game start and pass in

		if (!defenseIndices.TryGetValue(position, out int index))
			throw new InvalidOperationException($"No fielder assigned to {position}");

		return team.PositionPlayers[index];
	}
}

public struct ContactInfo
{
	public Direction Direction { get; init; }
	public Angle Angle { get; init; }
	public Force Force { get; init; }

	public ContactInfo(Direction direction, Angle angle, Force force)
	{
		Direction = direction;
		Angle = angle;
		Force = force;
	}
}

public struct FieldingAttempt
{
	public Player PrimaryFielder { get; init; }
	public Player? SecondaryFielder { get; init; }
	public ContactInfo Contact { get; init; }
}

public enum ContactOutcome
{
	Foul,
	Out,
	Safe,
	Homerun, // not yet implemented
}
