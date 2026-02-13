namespace Basedball
{
	public struct DefenderWeights
	{
		public float Pitcher { get; set; }
		public float Catcher { get; set; }
		public float FirstBase { get; set; }
		public float SecondBase { get; set; }
		public float ThirdBase { get; set; }
		public float ShortStop { get; set; }
		public float LeftField { get; set; }
		public float CenterField { get; set; }
		public float RightField { get; set; }

		public DefenderWeights(
			float pitcher = 0f,
			float catcher = 0f,
			float firstBase = 0f,
			float secondBase = 0f,
			float thirdBase = 0f,
			float shortStop = 0f,
			float leftField = 0f,
			float centerField = 0f,
			float rightField = 0f
		)
		{
			Pitcher = pitcher;
			Catcher = catcher;
			FirstBase = firstBase;
			SecondBase = secondBase;
			ThirdBase = thirdBase;
			ShortStop = shortStop;
			LeftField = leftField;
			CenterField = centerField;
			RightField = rightField;
		}

		public static DefenderWeights operator +(DefenderWeights a, DefenderWeights b)
		{
			return new DefenderWeights
			{
				Pitcher = a.Pitcher + b.Pitcher,
				Catcher = a.Catcher + b.Catcher,
				FirstBase = a.FirstBase + b.FirstBase,
				SecondBase = a.SecondBase + b.SecondBase,
				ThirdBase = a.ThirdBase + b.ThirdBase,
				ShortStop = a.ShortStop + b.ShortStop,
				LeftField = a.LeftField + b.LeftField,
				CenterField = a.CenterField + b.CenterField,
				RightField = a.RightField + b.RightField,
			};
		}

		public DefenderWeights WithNegativesZeroed()
		{
			return new DefenderWeights
			{
				Pitcher = Math.Max(0, Pitcher),
				Catcher = Math.Max(0, Catcher),
				FirstBase = Math.Max(0, FirstBase),
				SecondBase = Math.Max(0, SecondBase),
				ThirdBase = Math.Max(0, ThirdBase),
				ShortStop = Math.Max(0, ShortStop),
				LeftField = Math.Max(0, LeftField),
				CenterField = Math.Max(0, CenterField),
				RightField = Math.Max(0, RightField),
			};
		}

		public FieldPosition RollDice(Random random)
		{
			var zeroed = WithNegativesZeroed();
			float total = zeroed.Pitcher + zeroed.Catcher + zeroed.FirstBase + 
			              zeroed.SecondBase + zeroed.ThirdBase + zeroed.ShortStop +
			              zeroed.LeftField + zeroed.CenterField + zeroed.RightField;
			float roll = random.NextSingle() * total;
			
			return (roll -= zeroed.Pitcher) < 0 ? FieldPosition.Pitcher
				: (roll -= zeroed.Catcher) < 0 ? FieldPosition.Catcher
				: (roll -= zeroed.FirstBase) < 0 ? FieldPosition.FirstBase
				: (roll -= zeroed.SecondBase) < 0 ? FieldPosition.SecondBase
				: (roll -= zeroed.ThirdBase) < 0 ? FieldPosition.ThirdBase
				: (roll -= zeroed.ShortStop) < 0 ? FieldPosition.ShortStop
				: (roll -= zeroed.LeftField) < 0 ? FieldPosition.LeftField
				: (roll -= zeroed.CenterField) < 0 ? FieldPosition.CenterField
				: FieldPosition.RightField;
		}
	}

	public struct DirectionWeights
	{
		public float LeftLine { get; set; }
		public float LeftGap { get; set; }
		public float LeftCenter { get; set; }
		public float Center { get; set; }
		public float RightCenter { get; set; }
		public float RightGap { get; set; }
		public float RightLine { get; set; }

		public DirectionWeights(
			float leftLine = 0f,
			float leftGap = 0f,
			float leftCenter = 0f,
			float center = 0f,
			float rightCenter = 0f,
			float rightGap = 0f,
			float rightLine = 0f
		)
		{
			LeftLine = leftLine;
			LeftGap = leftGap;
			LeftCenter = leftCenter;
			Center = center;
			RightCenter = rightCenter;
			RightGap = rightGap;
			RightLine = rightLine;
		}

		public static DirectionWeights operator +(DirectionWeights a, DirectionWeights b)
		{
			return new DirectionWeights(
				a.LeftLine + b.LeftLine,
				a.LeftGap + b.LeftGap,
				a.LeftCenter + b.LeftCenter,
				a.Center + b.Center,
				a.RightCenter + b.RightCenter,
				a.RightGap + b.RightGap,
				a.RightLine + b.RightLine
			);
		}

		public DirectionWeights WithNegativesZeroed()
		{
			return new DirectionWeights(
				Math.Max(0, LeftLine),
				Math.Max(0, LeftGap),
				Math.Max(0, LeftCenter),
				Math.Max(0, Center),
				Math.Max(0, RightCenter),
				Math.Max(0, RightGap),
				Math.Max(0, RightLine)
			);
		}

		public Direction RollDice(Random random)
		{
			var zeroed = WithNegativesZeroed();
			float total = zeroed.LeftLine + zeroed.LeftGap + zeroed.LeftCenter + 
			              zeroed.Center + zeroed.RightCenter + zeroed.RightGap + 
			              zeroed.RightLine;
			float roll = random.NextSingle() * total;
			
			return (roll -= zeroed.LeftLine) < 0 ? Direction.LeftLine
				: (roll -= zeroed.LeftGap) < 0 ? Direction.LeftGap
				: (roll -= zeroed.LeftCenter) < 0 ? Direction.LeftCenter
				: (roll -= zeroed.Center) < 0 ? Direction.Center
				: (roll -= zeroed.RightCenter) < 0 ? Direction.RightCenter
				: (roll -= zeroed.RightGap) < 0 ? Direction.RightGap
				: Direction.RightLine;
		}
	}
	
	public struct ForceWeights
	{
		public float Weak { get; set; }
		public float Clean { get; set; }
		public float Blast { get; set; }

		public ForceWeights(float weak = 0f, float clean = 0f, float blast = 0f)
		{
			Weak = weak;
			Clean = clean;
			Blast = blast;
		}

		public static ForceWeights operator +(ForceWeights a, ForceWeights b)
		{
			return new ForceWeights(a.Weak + b.Weak, a.Clean + b.Clean, a.Blast + b.Blast);
		}

		public ForceWeights WithNegativesZeroed()
		{
			return new ForceWeights(Math.Max(0, Weak), Math.Max(0, Clean), Math.Max(0, Blast));
		}

		public Force RollDice(Random random)
		{
			var zeroed = WithNegativesZeroed();
			float total = zeroed.Weak + zeroed.Clean + zeroed.Blast;
			float roll = random.NextSingle() * total;
			
			return (roll -= zeroed.Weak) < 0 ? Force.Weak
				: (roll -= zeroed.Clean) < 0 ? Force.Clean
				: Force.Blast;
		}
	}

	public struct AngleWeights
	{
		public float Ground { get; set; }
		public float Line { get; set; }
		public float Fly { get; set; }
		public float Popup { get; set; }

		public AngleWeights(float ground = 0f, float line = 0f, float fly = 0f, float popup = 0f)
		{
			Ground = ground;
			Line = line;
			Fly = fly;
			Popup = popup;
		}

		public static AngleWeights operator +(AngleWeights a, AngleWeights b)
		{
			return new AngleWeights(
				a.Ground + b.Ground,
				a.Line + b.Line,
				a.Fly + b.Fly,
				a.Popup + b.Popup
			);
		}

		public AngleWeights WithNegativesZeroed()
		{
			return new AngleWeights(
				Math.Max(0, Ground),
				Math.Max(0, Line),
				Math.Max(0, Fly),
				Math.Max(0, Popup)
			);
		}

		public Angle RollDice(Random random)
		{
			var zeroed = WithNegativesZeroed();
			float total = zeroed.Ground + zeroed.Line + zeroed.Fly + zeroed.Popup;
			float roll = random.NextSingle() * total;
			
			return (roll -= zeroed.Ground) < 0 ? Angle.Ground
				: (roll -= zeroed.Line) < 0 ? Angle.Line
				: (roll -= zeroed.Fly) < 0 ? Angle.Fly
				: Angle.Popup;
		}
	}

	public struct ZoneWeights
	{
		public float Ball { get; set; }
		public float Looking { get; set; }
		public float Contact { get; init; }
		public float Swinging { get; init; }

		public ZoneWeights(
			float ball = 0f,
			float looking = 0f,
			float contact = 0f,
			float swinging = 0f
		)
		{
			Ball = ball;
			Looking = looking;
			Contact = contact;
			Swinging = swinging;
		}

		public static ZoneWeights operator +(ZoneWeights a, ZoneWeights b)
		{
			return new ZoneWeights(
				a.Ball + b.Ball,
				a.Looking + b.Looking,
				a.Contact + b.Contact,
				a.Swinging + b.Swinging
			);
		}

		public ZoneWeights WithNegativesZeroed()
		{
			return new ZoneWeights(
				Math.Max(0, Ball),
				Math.Max(0, Looking),
				Math.Max(0, Contact),
				Math.Max(0, Swinging)
			);
		}

		public PitchOutcome RollDice(Random random)
		{
			var zeroed = WithNegativesZeroed();
			float total = zeroed.Ball + zeroed.Looking + zeroed.Contact + zeroed.Swinging;
			float roll = random.NextSingle() * total;
			
			return (roll -= zeroed.Ball) < 0 ? PitchOutcome.Ball
				: (roll -= zeroed.Looking) < 0 ? PitchOutcome.StrikeLooking
				: (roll -= zeroed.Contact) < 0 ? PitchOutcome.Contact
				: PitchOutcome.StrikeSwinging;
		}
	}
}