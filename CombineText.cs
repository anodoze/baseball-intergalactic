public class CombineText
{
	public void Grab()
	{
		var files = new[]
		{
			"claude prompt.md",
			"Game.cs",
			"Inning.cs",
			"Team.CS",
			"Player.CS",
			"PlateAppearance.cs",
			"Contact.cs",
		};
		File.Delete("combined.txt");
		var workingDir = Directory.GetCurrentDirectory();

		foreach (var file in files)
		{
			var fullPath = Path.GetFullPath(file);
			var displayPath = fullPath.Replace(workingDir + Path.DirectorySeparatorChar, "");
			File.AppendAllText("combined.txt", $"// === {displayPath} ===\n");
			File.AppendAllText("combined.txt", File.ReadAllText(fullPath));
			File.AppendAllText("combined.txt", "\n");
		}
	}
}
