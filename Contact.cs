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
    }


    public enum ContactOutcome
    {
        Foul,
        BIP
    }
}