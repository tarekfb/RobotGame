using RobotGame;

Console.WriteLine("Welcome! This game lets you move a robot on a grid.");
Console.WriteLine("To begin, the robot needs to know some instructions.");
var commands = ReadFromFile();
var robot = new Robot();
var handler = new CommandHandler(commands, robot);
handler.ExecuteCommands();

List<string> ReadFromFile()
{
    try
    {
        Console.WriteLine("Please enter the location of a .txt file.");
        var filePath = Console.ReadLine();
        return File.ReadAllLines(filePath).ToList();
    }
    catch (Exception)
    {
        Console.WriteLine("The input was not valid.");
        Console.WriteLine("Closing program.");
        Environment.Exit(0);
        return new List<string>();
    }
}