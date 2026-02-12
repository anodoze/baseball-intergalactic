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
	}

	public struct DirectionWeights
	{
		public float LeftLine { get; set; }
		public float LeftField { get; set; }
		public float LeftCenterField { get; set; }
		public float Center { get; set; }
		public float RightCenterField { get; set; }
		public float RightField { get; set; }
		public float RightLine { get; set; }

		public DirectionWeights(
			float leftLine = 0f,
			float leftField = 0f,
			float leftCenterField = 0f,
			float center = 0f,
			float rightCenterField = 0f,
			float rightField = 0f,
			float rightLine = 0f
		)
		{
			LeftLine = leftLine;
			LeftField = leftField;
			LeftCenterField = leftCenterField;
			Center = center;
			RightCenterField = rightCenterField;
			RightField = rightField;
			RightLine = rightLine;
		}

		public static DirectionWeights operator +(DirectionWeights a, DirectionWeights b)
		{
			return new DirectionWeights(
				a.LeftLine + b.LeftLine,
				a.LeftField + b.LeftField,
				a.LeftCenterField + b.LeftCenterField,
				a.Center + b.Center,
				a.RightCenterField + b.RightCenterField,
				a.RightField + b.RightField,
				a.RightLine + b.RightLine
			);
		}

		public DirectionWeights WithNegativesZeroed()
		{
			return new DirectionWeights(
				Math.Max(0, LeftLine),
				Math.Max(0, LeftField),
				Math.Max(0, LeftCenterField),
				Math.Max(0, Center),
				Math.Max(0, RightCenterField),
				Math.Max(0, RightField),
				Math.Max(0, RightLine)
			);
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
	}

	public struct HitTypeWeights
	{
		public float Ground { get; set; }
		public float Line { get; set; }
		public float Fly { get; set; }
		public float Popup { get; set; }

		public HitTypeWeights(float ground = 0f, float line = 0f, float fly = 0f, float popup = 0f)
		{
			Ground = ground;
			Line = line;
			Fly = fly;
			Popup = popup;
		}

		public static HitTypeWeights operator +(HitTypeWeights a, HitTypeWeights b)
		{
			return new HitTypeWeights(
				a.Ground + b.Ground,
				a.Line + b.Line,
				a.Fly + b.Fly,
				a.Popup + b.Popup
			);
		}

		public HitTypeWeights WithNegativesZeroed()
		{
			return new HitTypeWeights(
				Math.Max(0, Ground),
				Math.Max(0, Line),
				Math.Max(0, Fly),
				Math.Max(0, Popup)
			);
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
	}
}
