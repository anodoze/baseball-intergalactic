using System.Text;

Random random = new Random();
string awayName = "Louisville Sluggers";
string homeName = "New York Aliens";
var awayTeam = TeamGenerator.GenerateTeam(awayName, random);
var homeTeam = TeamGenerator.GenerateTeam(homeName, random);
var game = new Game(homeTeam, awayTeam, random);
var summary = new CombineText();

game.Play();
summary.Grab();
