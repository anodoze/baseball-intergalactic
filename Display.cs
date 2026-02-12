namespace Basedball
{
	public static class Display
	{
		public static void Pitch(PitchResult result, int balls, int strikes)
		{
			Console.WriteLine($"Zone {result.Zone} | {result.Outcome} | Count: {balls}-{strikes}");
		}
	}
}
