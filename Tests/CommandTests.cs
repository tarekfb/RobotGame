using Moq;
using RobotGame;
using System.Drawing;
using Xunit;

namespace Tests
{
    public class CommandTests
    {
        // TODO: ignore invalid place command

        [Fact]
        public void IgnoreTooGreatPlacement()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 6,0,WEST",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            var point = new Point() { X = 6, Y = 0 };
            Assert.NotEqual(point, robot.Position);
            Assert.NotEqual(Direction.West, robot.Facing);
        }

        [Fact]
        public void IgnoreNegativePlacement()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE -6,0,WEST",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            var point = new Point() { X = -6, Y = 0 };
            Assert.NotEqual(point, robot.Position);
            Assert.NotEqual(Direction.West, robot.Facing);
        }

        [Fact]
        public void MultiplePlacements()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 0,0,NORTH",
                "MOVE",
                "PLACE 5,5,SOUTH",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            var expectedPoint = new Point() { X = 5, Y = 5 };
            Assert.Equal(expectedPoint, robot.Position);
            Assert.Equal(Direction.South, robot.Facing);
        }

        [Fact]
        public void Turn()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 0,0,NORTH",
                "RIGHT",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            Assert.Equal(Direction.East, robot.Facing);
        }

        [Fact]
        public void IgnoreMoveIfInvalid()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 0,0,WEST",
                "MOVE",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            var expectedPoint = new Point() { X = 0, Y = 0 };
            Assert.Equal(expectedPoint, robot.Position);
            Assert.Equal(Direction.West, robot.Facing);
        }

        [Fact]
        public void AllowContinueIfInvalidMove()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 0,0,WEST",
                "MOVE",
                "RIGHT",
                "MOVE"
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            var expectedPoint = new Point() { X = 0, Y = 1 };
            Assert.Equal(expectedPoint, robot.Position);
            Assert.Equal(Direction.North, robot.Facing);
        }

        [Fact]
        public void TurnLeftWhenNorth()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 0,0,NORTH",
                "LEFT",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            Assert.Equal(Direction.West, robot.Facing);
        }

        [Fact]
        public void TurnRightWhenWest()
        {
            // Arrange
            var commands = new List<string>
            {
                "PLACE 0,0,WEST",
                "RIGHT",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            Assert.Equal(Direction.North, robot.Facing);
        }

        [Fact]
        public void IgnoreIfNotPlaced()
        {
            // Arrange
            var commands = new List<string>
            {
                "MOVE",
                "RIGHT",
            };
            var robot = new Robot();
            var handler = new CommandHandler(commands, robot);

            // Act
            handler.ExecuteCommands();

            // Assert
            var point = new Point() { X= 0, Y = 1 };
            Assert.NotEqual(point, robot.Position); // defualt is 0,0. Meaning robot will move one step along y (landing on 0,1) if executed 
        }
    }
}