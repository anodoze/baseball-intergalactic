namespace Basedball
{
    public class Contact 
    {
        public static ContactOutcome Simulate(Random random)
        {
            int go = random.Next(2);
            if (go < 1) return ContactOutcome.Foul;
            return ContactOutcome.BIP;
        }

        private static readonly Dictionary<int, DefenderWeights> DefaultDirectionWeights = new()
        {
            // 1 - Left foul line
            [1] = new DefenderWeights(
                pitcher: 0.02f,
                catcher: 0.03f,
                thirdBase: 0.20f,
                shortStop: 0.10f,
                leftField: 0.65f
            ), 
            // 2 - Left-center gap
            [2] = new DefenderWeights(
                pitcher: 0.03f,
                catcher: 0.01f,
                thirdBase: 0.05f,
                shortStop: 0.28f,
                leftField: 0.48f,
                centerField: 0.15f
            ),
            // 3 - Left of center
            [3] = new DefenderWeights(
                pitcher: 0.04f,
                thirdBase: 0.02f,
                secondBase: 0.10f,
                shortStop: 0.38f,
                leftField: 0.28f,
                centerField: 0.18f
            ),
            // 4 - Up the middle
            [4] = new DefenderWeights(
                pitcher: 0.15f,
                catcher: 0.05f,
                firstBase: 0.01f,
                secondBase: 0.23f,
                thirdBase: 0.01f,
                shortStop: 0.23f,
                centerField: 0.32f
            ),
            // 5 - Right of center
            [5] = new DefenderWeights(
                pitcher: 0.04f,
                catcher: 0.02f,
                firstBase: 0.10f,
                secondBase: 0.38f,
                centerField: 0.28f,
                rightField: 0.18f
            ),
            // 6 - Right-center gap
            [6] = new DefenderWeights(
                pitcher: 0.03f,
                catcher: 0.01f,
                firstBase: 0.05f,
                secondBase: 0.28f,
                centerField: 0.15f,
                rightField: 0.48f
            ), 
            // 7 - Right foul line
            [7] = new DefenderWeights(
                pitcher: 0.02f,
                catcher: 0.03f,
                firstBase: 0.20f,
                secondBase: 0.10f,
                rightField: 0.65f
            )
        };
    }

    public struct DirectionWeights
    {
        
    }

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
                RightField = a.RightField + b.RightField
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
                RightField = Math.Max(0, RightField)
            };
        }
    }

    public enum ContactOutcome
    {
        Foul,
        BIP
    }
}