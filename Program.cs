using Basedball;

Random _random = new Random();

var batter = new Player(Position.CF, _random);
var pitcher = new Player(Position.SP, _random);

Console.WriteLine(
	$"{pitcher.FirstName} {pitcher.LastName} vs. {batter.FirstName} {batter.LastName}"
);

for (int i = 0; i < 5; i++)
{
	var pA = PlateAppearance.Simulate(batter, pitcher, _random);
	Console.WriteLine(pA.ToString());
}
