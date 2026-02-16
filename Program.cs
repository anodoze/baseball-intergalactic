using Basedball;

Random random = new Random();
string awayName = "Louisville Sluggers";
string homeName = "New York Aliena";
var awayTeam = TeamGenerator.GenerateTeam(awayName, random);
var homeTeam = TeamGenerator.GenerateTeam(homeName, random);

var game = new Game(homeTeam, awayTeam, random);

game.Play();
