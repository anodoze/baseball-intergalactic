using Basedball;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

Random _random = new Random();

var batter = new Player(Position.CF, _random);

var pitcher = new Player(Position.SP, _random);

Console.WriteLine($"{pitcher.FirstName} {pitcher.LastName} vs. {batter.FirstName} {batter.LastName}");

var pA = PlateAppearance.Simulate(batter, pitcher, _random);

Console.WriteLine(pA.ToString());