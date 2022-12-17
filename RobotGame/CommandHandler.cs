using System.Drawing;

namespace RobotGame
{
    public class CommandHandler
    {
        List<string> _commands;
        Robot _robot;

        public CommandHandler(List<string> commands, Robot robot)
        {
            _commands = commands;
            _robot = robot;
        }

        public void ExecuteCommands()
        {
            var indexOfPlaceCommand = _commands.FindIndex(c => c.Contains("place", StringComparison.OrdinalIgnoreCase));
            if (indexOfPlaceCommand == -1)
            {
                Console.WriteLine("Please use atleast 1 place command.");
                Console.WriteLine("Closing program.");
                return;
            }
            else if (IsValidPlaceCommand(_commands[indexOfPlaceCommand])) // if invalid, do nothing (there are prints in method call)
            {
                if (indexOfPlaceCommand > 0)
                {
                    Console.WriteLine($"Place command not first command. Ignoring {indexOfPlaceCommand} commands.");
                    _commands.RemoveRange(0, indexOfPlaceCommand);
                    // "A robot that is not on the table can choose to ignore MOVE, LEFT..."
                    // assuming this means it will ignore
                    // can add readline if option is needed
                }

                foreach (string command in _commands)
                {
                    if (command.Contains("PLACE"))
                    {
                        HandlePlaceCommand(command);
                    }
                    else if (command.Equals("move", StringComparison.OrdinalIgnoreCase))
                    {
                        _robot.Move();
                    }
                    else if (command.Equals("left", StringComparison.OrdinalIgnoreCase) || command.Equals("right", StringComparison.OrdinalIgnoreCase))
                    {
                        _robot.Turn(command);
                    }
                    else if (command.Equals("report", StringComparison.OrdinalIgnoreCase))
                    {
                        _robot.Report();
                    }
                }
            }
        }

        bool IsValidPlaceCommand(string command)
        {
            var values = command[6..];
            var segments = values.Split(',');

            var isValid = true;
            if (segments.Any(item => item.StartsWith("-")))
            {
                Console.WriteLine("Only positive numbers are allowed");
                isValid = false;
            }

            if (int.TryParse(segments[0], out int x)) // not DRY, but leaving as is due to time constraints
            {
                if (x > _robot.Length) // length of 5 is hardcoded, could restructure if dynamic length is needed
                {
                    Console.WriteLine("Only numbers between 0 and 5 is allowed");
                    isValid = false;
                }
            }

            if (int.TryParse(segments[1], out int y)) // not DRY, but leaving as is due to time constraints
            {
                if (y > _robot.Length)  // length of 5 is hardcoded, could restructure if dynamic length is needed
                {
                    Console.WriteLine("Only numbers between 0 and 5 is allowed");
                    isValid = false;
                }
            }

            // format so it matches enum format, and then try parse to see if valid
            var formatted = FormatToEnumString(segments[2]);
            if (!Enum.TryParse(formatted, out Direction direction))
            {
                Console.WriteLine("The place command needs contain one of the following directions:");
                Console.WriteLine("NORTH");
                Console.WriteLine("EAST");
                Console.WriteLine("SOUTH");
                Console.WriteLine("WEST");
                isValid = false;
            }

            if (!isValid)
            {
                Console.WriteLine("Closing program.");
            }

            return isValid;
        }

        string FormatToEnumString(string direction)
        {
            var asLowercase = direction.ToLower();
            var firstUpper = asLowercase[0].ToString().ToUpper();
            return $"{firstUpper}{asLowercase[1..]}";
        }

        void HandlePlaceCommand(string command)
        {
            var values = command[6..];
            var segments = values.Split(',');

            var x = int.Parse(segments[0]);
            var y = int.Parse(segments[1]);
            var point = new Point() { X = x, Y = y };

            _ = Enum.TryParse(FormatToEnumString(segments[2]), out Direction facing);

            _robot.Place(point, facing);
        }

    }
}
