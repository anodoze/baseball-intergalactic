namespace Basedball
{
    public class PlateAppearance
    {
        private Random _random = new Random();

        public static PAOutcome Simulate(Player batter, Player pitcher, Random random)
        {
            int strikeCount = 0;
            int ballCount = 0;
            bool BIP = false;

            while (strikeCount < 3 && ballCount < 4 && BIP == false)
            {
                var outcome = Pitch.ThrowPitch(batter, pitcher, random);
                
                if (outcome == PitchOutcome.StrikeLooking || outcome == PitchOutcome.StrikeSwinging){
                    strikeCount++;
                    Console.WriteLine($"Strike {strikeCount}!");
                    if (strikeCount > 2) { return PAOutcome.Strikeout; }
                } else if (outcome == PitchOutcome.Foul) {
                    if (strikeCount <2 ) strikeCount ++;
                } else if (outcome == PitchOutcome.Ball) {
                    ballCount++;
                    Console.WriteLine($"Ball {ballCount}!");
                    if (ballCount > 3) { return PAOutcome.Walk;}
                } else if (outcome == PitchOutcome.BIP) {
                    return PAOutcome.Play;
                }
            }

            Console.WriteLine("Plate Appearance did not");
            return PAOutcome.SimError;
        }
    }

    public struct ZoneWeights
    {
        public float Ball;
        public float Looking;
        public float Contact;
        public float Swinging;

        public ZoneWeights(float ball, float looking,  float contact, float swinging)
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
    }

    public class Pitch
    {
        private static readonly Dictionary<int, ZoneWeights> DefaultZoneWeights = new()
        {
            [1] = new ZoneWeights(0.00f, 0.40f, 0.35f, 0.25f), //in zone
            [14] = new ZoneWeights(0.25f, 0.00f, 0.25f, 0.25f) // out of zone
        };

        public static PitchOutcome ThrowPitch(Player pitcher, Player batter, Random _random)
        {
            // select pitch
            // balk?
            // select zone 
            int zonePick = new[] { 1, 14 }[_random.Next(2)];
            var zoneWeights = DefaultZoneWeights[zonePick];
            // int cornerPick = new[] { 1, 3, 7, 9 }[_random.Next(4)];
            var batterWeights = new ZoneWeights(
                batter.Discipline,
                - batter.Attack,
                batter.Contact,
                batter.Attack * 0.5f
            );

            var pitcherWeights = new ZoneWeights(
                - pitcher.Movement,
                pitcher.Movement,  // also increases strikes looking when in the zone + eligible
                - pitcher.Velocity,
                pitcher.Movement // always increases strikes swinging
            );

            zoneWeights += batterWeights;
            zoneWeights += pitcherWeights;

            // modify weights to account for zone - no strikes looking out of the zone, no balls in the zone
            if (zonePick == 14)
            {
                zoneWeights.Looking = 0;
            } else {
                zoneWeights.Ball = 0;
            };

            float total = zoneWeights.Ball + zoneWeights.Contact + zoneWeights.Swinging + zoneWeights.Looking;
            float roll = (float)_random.NextDouble() * total;

            if ((roll -= zoneWeights.Ball) < 0) return PitchOutcome.Ball;
            if ((roll -= zoneWeights.Looking) < 0) return PitchOutcome.StrikeLooking;
            if ((roll -= zoneWeights.Contact) < 0) return PitchOutcome.BIP;
            return PitchOutcome.StrikeSwinging;
        }

        // runners may attempt to steal!
        // notice?
        // throw them out?
    }
    
    public enum PitchOutcome
    {
        StrikeLooking,
        StrikeSwinging,
        Ball,
        Foul,
        BIP,
        Balk
    }

    public enum PAOutcome
    {
        Strikeout,
        Walk,
        Play,
        SimError
    }
        //RUNNERS
            // judgement: try to advance? 
            // speed: advancement success
            // performance: lowers defense effectiveness, can sneak under what would otherwise be a tagout
}



