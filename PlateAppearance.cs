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

    public class Pitch
    {

        public static PitchOutcome ThrowPitch(Player pitcher, Player batter, Random _random)
        {
            // select pitch

            // throw pitch
            // balk?
            int balkRoll = _random.Next(1, 1001 + (pitcher.Composure * 5));
            if (balkRoll == 1) { return PitchOutcome.Balk; }

            // select zone 
            int cornerPick = new[] { 1, 3, 7, 9 }[_random.Next(4)];
            int zonePick = cornerPick;
            int controlRoll = _random.Next(1, pitcher.Control);
            if (controlRoll < 20)
            {
                zonePick = _random.Next(1, 15);
            } 
            // add logic later for near-misses

            int visionRoll = _random.Next(1, batter.Vision);
            int movementRoll = _random.Next(1, pitcher.Movement);
            int contactRoll = _random.Next(1, pitcher.Contact);

            if (visionRoll > movementRoll)
            {
                return PitchOutcome.Ball;
            } else {
                if (contactRoll > controlRoll)
                    {
                        return PitchOutcome.BIP;
                    } else
                {
                    return PitchOutcome.StrikeSwinging;
                }
            }
        }
        //PITCHER
            // throw pitch
                // balk?
            // select zone
                // control roll
                // if control fails, select another zone
            // motion roll
            // velocity roll

        // runners may attempt to steal!
        // notice?
        // throw them out?

        //BATTER
        // guess pitch
        // track ball
            // in/out of zone?
            // swing?
            // contact?
        // hit quality
            // angle
            // distance
            // location
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



