namespace Basedball
{
	public class Contact
	{
		public static ContactOutcome Simulate(Player batter, Random random)
		{
            var directionWeights = FieldDefaults.DefaultDirectionWeights[Handedness.Righty];
            var batterDirectionWeights = new DirectionWeights(
                leftLine: -(batter.Aim - 0.5f) * 1.5f,     // low aim = more fouls
                leftGap: (batter.Aim - 0.5f) * 2.4f,       // aim for the money spots
                leftCenter: (batter.Aim - 0.5f) * 1.0f,
                center: 0f,                                 // center is always neutral
                rightCenter: (batter.Aim - 0.5f) * 1.0f,
                rightGap: (batter.Aim - 0.5f) * 2.4f,
                rightLine: -(batter.Aim - 0.5f) * 1.5f
            );
            directionWeights += batterDirectionWeights;
            var direction = directionWeights.RollDice(random);

            var angleWeights = FieldDefaults.DefaultAngleWeights[direction];
            var batterAngleWeights = new AngleWeights(
                ground: -(batter.Form - 0.5f) * 1.8f,      // bad form tops the ball
                line: (batter.Form - 0.5f) * 2.4f,         // clean barrel work
                fly: (batter.Form - 0.5f) * 1.6f,          // elevate properly
                popup: -(batter.Form - 0.5f) * 1.2f        // mishit under it
            );
            angleWeights += batterAngleWeights;
            var angle = angleWeights.RollDice(random);

            var forceWeights = FieldDefaults.DefaultForceWeights[direction];
            var batterForceWeights = new ForceWeights(
                weak: -(batter.Power - 0.5f) * 2.0f,       // strong guys rarely dribble
                clean: (batter.Power - 0.5f) * 1.0f,       // moderate scaling
                blast: (batter.Power - 0.5f) * 2.4f        // power shows up here
            );
            forceWeights += batterForceWeights;
            var force = forceWeights.RollDice(random);

            int go = random.Next(2);
            if (go < 1)
                return ContactOutcome.Foul;
            return ContactOutcome.BIP;
        }

        public static FieldingAttempt PrepareFieldingAttempt(ContactInfo contact, Player[] fielders, Random random) //todo: add fielders to main game roster handling
        {
            var defenderWeights = FieldDefaults.DefaultDefenderWeights[contact.Direction];
            
            var angleModifier = contact.Angle switch
            {
                Angle.Ground => new DefenderWeights(
                    pitcher: 0.05f,
                    catcher: 0.15f,      // scramble after dribblers
                    firstBase: 0.10f,
                    secondBase: 0.10f,
                    thirdBase: 0.10f,
                    shortStop: 0.10f,
                    leftField: -0.25f,   // outfielders less involved
                    centerField: -0.25f,
                    rightField: -0.25f
                ),
                Angle.Line => new DefenderWeights(), // lines use base weights
                Angle.Fly => new DefenderWeights(
                    pitcher: -0.10f,
                    catcher: -0.15f,     // not going after fly balls
                    firstBase: -0.05f,
                    secondBase: -0.05f,
                    thirdBase: -0.05f,
                    shortStop: -0.05f,
                    leftField: 0.20f,    // outfielders' job
                    centerField: 0.20f,
                    rightField: 0.20f
                ),
                Angle.Popup => new DefenderWeights(
                    pitcher: 0.05f,
                    catcher: 0.20f,      // popups near the plate
                    firstBase: 0.05f,
                    secondBase: 0.05f,
                    thirdBase: 0.05f,
                    shortStop: 0.05f,
                    leftField: -0.15f,
                    centerField: -0.15f,
                    rightField: -0.15f
                ),
                _ => new DefenderWeights()
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
                Force.Clean => new DefenderWeights(), // clean uses base
                Force.Blast => new DefenderWeights(
                    pitcher: -0.10f,
                    catcher: -0.15f,
                    firstBase: -0.05f,
                    secondBase: -0.05f,
                    thirdBase: -0.05f,
                    shortStop: -0.05f,
                    leftField: 0.15f,    // deep to the outfield
                    centerField: 0.15f,
                    rightField: 0.15f
                ),
                _ => new DefenderWeights()
            };
        
            defenderWeights += angleModifier + forceModifier;

            var primaryPosition = defenderWeights.RollDice(random);
            var secondaryPosition = GetBackupFielder(primaryPosition, contact.Direction, contact.Angle);
            
            var primaryFielder = Array.Find(fielders, f => f.FieldPosition == primaryPosition);
            var secondaryFielder = Array.Find(fielders, f => f.FieldPosition == secondaryPosition);

            if (primaryFielder == null)
                throw new InvalidOperationException($"No fielder found at {primaryPosition}");
            
            return new FieldingAttempt
            {
                PrimaryFielder = primaryFielder,
                SecondaryFielder = secondaryFielder,
                Contact = contact
                // Difficulty and skills TBD
            };
        }

        public static FieldPosition? GetBackupFielder(FieldPosition primary, Direction direction, Angle angle)
        {
            if (primary is FieldPosition.LeftField or FieldPosition.CenterField or FieldPosition.RightField)
                return null;
            
            if (primary == FieldPosition.Catcher)
            {
                if (angle == Angle.Ground)
                {
                    return direction switch
                    {
                        Direction.LeftLine or Direction.LeftGap or Direction.LeftCenter => FieldPosition.ThirdBase,
                        Direction.Center => FieldPosition.FirstBase, // todo: add choice when there are baserunners
                        Direction.RightCenter or Direction.RightGap or Direction.RightLine => FieldPosition.FirstBase,
                        _ => null
                    };
                }
                return null;
            }

            return (primary, direction) switch
            {
                (FieldPosition.ThirdBase, _) => FieldPosition.LeftField,
                (FieldPosition.ShortStop, Direction.LeftLine or Direction.LeftGap) => FieldPosition.LeftField,
                (FieldPosition.ShortStop, Direction.LeftCenter or Direction.Center) => FieldPosition.CenterField,
                (FieldPosition.SecondBase, Direction.RightLine or Direction.RightGap) => FieldPosition.RightField,
                (FieldPosition.SecondBase, Direction.RightCenter or Direction.Center) => FieldPosition.CenterField,
                (FieldPosition.FirstBase, _) => FieldPosition.RightField,
                (FieldPosition.Pitcher, _) => FieldPosition.CenterField,
                _ => null
            };
        }
            // the difficulty of the fielding attempt (hit type, power)
            // The context to determine outcomes (from direction: mainly relevant for directions 1 and 7, where potential fouls need to be taken into account)
            // possible fielding results:
            // Catch out (weighted 0 on ground ball) - straightforward check against defense attributes
            // Foul (weighted 0 for directions outside 1 and 7) - most checks here should be easy, but I do want to involve Awareness and Sense here. A defender should be more eager to catch a foul with empty bases than with a guy on third who might try to run in if you make that ball live.
            // Fielded (for now we'll just coinflip hit/out, we will add misses that have to get picked up by other defenders later, and we can incorporate the throwing attributes as the last step before we start tracking baserunners, since getting a guy out in part depends on how fast he can run)
	}

    public struct ContactInfo
    {
        public Direction Direction { get; init; }
        public Angle Angle { get; init; }
        public Force Force { get; init; }
        
        // Optional enrichment for better descriptions
        public float ExitVelocity { get; init; } // mph for flavor text
        public float HangTime { get; init; } // seconds for fly balls
        
        // public override string ToString(); // todo: display helper
    }

    public struct FieldingAttempt
    {
        public Player PrimaryFielder { get; init; }
        public Player? SecondaryFielder { get; init; } // if it gets past primary
        
        public float Difficulty { get; init; } // combined score
        public bool CanBeFoul { get; init; } // directions 1/7 only
        
        // Keep the contact info for context
        public ContactInfo Contact { get; init; }
    }

	public enum ContactOutcome
	{
		Foul,
		BIP,
	}
};
