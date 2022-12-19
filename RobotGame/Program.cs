using RobotGame;

Console.WriteLine("Welcome! This game lets you move a robot on a grid.");
Console.WriteLine("To begin, the robot needs to know some instructions.");
var commands = ReadFromFile();
Console.WriteLine("What width and height should the grid be?");
var length = Console.ReadLine();
var robot = new Robot(int.Parse(length));
var handler = new CommandHandler(commands, robot);
handler.ExecuteCommands();
robot.DrawGrid();

List<string> ReadFromFile()
{
    try
    {
        Console.WriteLine("Please enter the location of a .txt file.");
        var filePath = Console.ReadLine();

        var tempPath = "C:\\Users\\tarek\\source\\repos\\RobotGame\\RobotGame\\TestData\\test.txt";
        return File.ReadAllLines(tempPath).ToList();
    }
    catch (Exception)
    {
        Console.WriteLine("The input was not valid.");
        Console.WriteLine("Closing program.");
        Environment.Exit(0);
        return new List<string>();
    }
}