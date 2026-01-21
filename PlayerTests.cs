using Xunit;

namespace Basedball.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_Constructor_SetsAllProperties()
        {
            var random = new Random(42); // fixed seed = reproducible
            var player = new Player(Position.CF, random);
            
            Assert.NotNull(player.Id);
            Assert.IsType<string>(player.FirstName);
            Assert.IsType<string>(player.LastName);
            Assert.Equal(Position.CF, player.Position);

            Assert.Equal(100, player.Durability);
            Assert.InRange(player.Composure, 0, 100);

            Assert.InRange(player.Vision, 0, 100);
            Assert.InRange(player.Awareness, 0, 100);
            Assert.InRange(player.Reaction, 0, 100);
            Assert.InRange(player.Power, 0, 100);
            Assert.InRange(player.Speed, 0, 100);
            Assert.InRange(player.Stamina, 0, 100);

            Assert.InRange(player.Charisma, 0, 100);
            Assert.InRange(player.Esprit, 0, 100);
            Assert.InRange(player.Aggression, 0, 100);
            Assert.InRange(player.Judgement, 0, 100);

            Assert.InRange(player.Superstition, 0, 100);
            Assert.InRange(player.Grit, 0, 100);

            Assert.InRange(player.Prediction, 0, 100);
            Assert.InRange(player.Discipline, 0, 100);
            Assert.InRange(player.Attack, 0, 100);
            Assert.InRange(player.Contact, 0, 100);
            Assert.InRange(player.Form, 0, 100);
            Assert.InRange(player.Aim, 0, 100);

            Assert.InRange(player.Deception, 0, 100);
            Assert.InRange(player.Mechanics, 0, 100);
            Assert.InRange(player.Velocity, 0, 100);
            Assert.InRange(player.Control, 0, 100);
            Assert.InRange(player.Movement, 0, 100);

            Assert.InRange(player.Sprint, 0, 100);
            Assert.InRange(player.Performance, 0, 100);
            Assert.InRange(player.Sneak, 0, 100);

            Assert.InRange(player.Sense, 0, 100);
            Assert.InRange(player.Agility, 0, 100);
            Assert.InRange(player.Acrobatics, 0, 100);
            Assert.InRange(player.Arm, 0, 100);
            Assert.InRange(player.Precision, 0, 100);
            
            Assert.True(player.Pitches.Count >= 1 && player.Pitches.Count <= 4);
        }
    }
}