namespace Basedball
{
    public class Player
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Position Position { get; set; }

        public int Durability { get; set; }    // player's overall health
        public int Composure { get; set; }     // player's ingame mental state

        // Nature
        public int Vision { get; set; }        // ball tracking 
        public int Awareness { get; set; }     // situational awareness
        public int Reaction { get; set; }      // reaction speed
        public int Power { get; set; }         // hard throwing/batting/sprint acceleration
        public int Speed { get; set; }         // SPEED
        public int Stamina { get; set; }       // mostly affects pitchers and catchers as pitch count goes up
   
        public int Charisma { get; set; }      // player interaction with fans
        public int Esprit { get; set; }        // player interaction with teammates
        public int Aggression { get; set; }    // interaction with opposing players - desire to steal bases, hit agressively, will charge the mound 
        public int Judgement { get; set; }     // player's ability to notice and hold back from high-risk plays

        public int Luck { get; set; }          // has a small effect on everything
        public int Superstition { get; set; }  // how much effect Luck has
        public int Grit { get; set; }          // how well a player can weather Unlucky events

        // Skill  

        // batting 
        public int Prediction { get; set; }    // Batter's ability to call the next pitch, catcher's ability to read the batter
        public int Discipline { get; set; }    // Batter's ability to hold back outside the strike zone
        public int Attack { get; set; }        // Batter's ability to reliably attack inside the strike zone
        public int Contact { get; set; }       // Batter's ability to Contact the ball when swinging
        public int Form { get; set; }          // Batter's ability to leverage their Power
        public int Aim { get; set; }           // Batter's ability to hit the ball to an auspicious location

        // pitching
        public int Deception { get; set; }     // Pitcher's ability to hide their next pitch. on Catchers, improves framing ability
        public int Mechanics { get; set; }     // Pitcher's ability to leverage their Power
        public int Velocity { get; set; }      // Pitcher's ability to throw fast, increasing strikes
        public int Control { get; set; }       // Pitcher's ability to control their pitch, throwing it to difficult zones
        public int Movement { get; set; }      // Pitcher's ability to create deceptive movement in a pitch

        // baserunning
        public int Sprint { get; set; }        // Batter's ability to leverage their Speed on the basepaths - faster acceleration   
        public int Performance { get; set; }   // Batter's ability to induce fielding mistakes. can sometimes dodge under what would be an out.
        public int Sneak { get; set; }         // Batter's ability to steal bases

        // defense
        public int Sense { get; set; }         // Defender's Ability to understand the game state and make good decisions

        public int Agility { get; set; }       // Ability to get in position to catch the baseball
        public int Acrobatics { get; set; }    // Ability to chase and catch the baseball when in range

        public int Arm { get; set; }           // Ability to throw quickly, especially over long distances
        public int Precision { get; set; }     // Ability to throw with precision to other teammates


        public List<PitchType> Pitches { get; set; } = new List<PitchType>();

        public Player(Position position, Random random)
        {
            Id = Guid.NewGuid().ToString();
            FirstName = NameData.firstNames[random.Next(NameData.firstNames.Length)];
            LastName = NameData.lastNames[random.Next(NameData.lastNames.Length)];
            Position = position;
            
            var allPitches = Enum.GetValues<PitchType>();
            var numPitches = random.Next(1, 5);
            var shuffled = allPitches.OrderBy(x => random.Next()).Take(numPitches);
            Pitches.AddRange(shuffled);
            
            Durability = 100;
            Composure = random.Next(0, 101);

            Vision = random.Next(0, 101);
            Awareness = random.Next(0, 101);
            Reaction = random.Next(0, 101);
            Power = random.Next(0, 101);
            Speed = random.Next(0, 101);
            Stamina = random.Next(0, 101);

            Charisma = random.Next(0, 101);
            Esprit = random.Next(0, 101);
            Aggression = random.Next(0, 101);
            Judgement = random.Next(0, 101);

            Luck = random.Next(0, 101);
            Superstition = random.Next(0, 101);
            Grit = random.Next(0, 101);

            Prediction = random.Next(0, 101);
            Discipline = random.Next(0, 101);
            Attack = random.Next(0, 101);
            Contact = random.Next(0, 101);
            Form = random.Next(0, 101);
            Aim = random.Next(0, 101);

            Deception = random.Next(0, 101);
            Mechanics = random.Next(0, 101);
            Velocity = random.Next(0, 101);
            Control = random.Next(0, 101);
            Movement = random.Next(0, 101); 

            Sprint = random.Next(0, 101);
            Performance = random.Next(0, 101);
            Sneak = random.Next(0, 101);

            Sense = random.Next(0, 101);
            Agility = random.Next(0, 101);
            Acrobatics = random.Next(0, 101);
            Arm = random.Next(0, 101);
            Precision = random.Next(0, 101);
        }
    }

    public enum PitchType
    {
        Fastball,
        Changeup,
        Curveball,
        Slider
    }

    public enum Position
    {
        SP, RP, CL,
        CA, _1B, _2B, _3B,
        LF, CF, RF, DH
    }
}