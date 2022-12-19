using System.Drawing;

namespace RobotGame
{
    public class Robot
    {
        public Point Position { get; set; }
        public Direction Facing { get; set; }
        Point[] Points { get; set; }
        public int Length { get; set; }

        public Robot()
        {
            Populate();
            Length = 5;
        }
        public Robot(int length)
        {
            Length = length;
            Populate();
        }

        public void DrawGrid()
        {
            Console.Clear();

            int totalCounter = Points.Length - 1;
            for (int i = 0; i < Length; i++) // 5 arrays
            {
                var row = "";

                for (int j = Length; j > 0; j--) // 5 rows, for 5 arrays. 5x5 = 25
                {
                    if (Points[totalCounter].X == Position.X && Points[totalCounter].Y == Position.Y)
                    {
                        var direction = "";
                        switch (Facing)
                        {
                            case Direction.North:
                                direction = " ^ ";
                                break;
                            case Direction.East:
                                direction = " > ";
                                break;
                            case Direction.South:
                                direction = " v ";
                                break;
                            case Direction.West:
                                direction = " < ";
                                break;
                            default:
                                break;
                        }
                        //row += direction;
                        row = row.Insert(0, direction);
                    }
                    else
                    {
                        row = row.Insert(0, $" . ");
                    }
                    totalCounter--;
                }

                Console.WriteLine(row);

            }
            Thread.Sleep(500);
        }

        public void Report()
        {
            Console.WriteLine($"{Position.X},{Position.Y},{Facing}");
        }

        public void Turn(string leftOrRight)
        {
            var newFacing = Facing;

            // assuming casing doesn't matter
            if (leftOrRight.Equals("right", StringComparison.OrdinalIgnoreCase))
                newFacing++;
            else if (leftOrRight.Equals("left", StringComparison.OrdinalIgnoreCase))
                newFacing--;

            Facing = HandleOutsideOfEnumRange(newFacing);
            DrawGrid();
        }

        Direction HandleOutsideOfEnumRange(Direction direction)
        {
            // handle if Direction enum is higher than 3 or lower than 0
            // enums for cardinal directoins are 0-3
            // if 4, intending to face west --> north
            // if -1, intending to face north --> west
            switch ((int)direction)
            {
                case 4:
                    direction = 0;
                    break;
                case -1:
                    direction = (Direction)3;
                    break;
            } // if neither, return as is)

            return direction;
        }

        public void Place(Point point, Direction facing)
        {
            Facing = facing;
            Position = point;
        }

        public void Move()
        {
            Point offset = new Point();
            switch (Facing)
            {
                case Direction.North:
                    offset.Y = 1;
                    break;
                case Direction.East:
                    offset.X = 1;
                    break;
                case Direction.West:
                    offset.X = -1;
                    break;
                case Direction.South:
                    offset.Y = -1;
                    break;
            }

            Point newPos = Position;
            newPos.Offset(offset);

            if (IsValidPos(newPos)) Position = newPos;
            else
            {
                Console.WriteLine($"Oh no! You fell off the table. Putting you back at... {Position.X}, {Position.Y} facing {Facing}.");
                Thread.Sleep(2000);
            }
            DrawGrid();
        }

        bool IsValidPos(Point point) // check if new pos is outside of grid
        {
            int max = Length - 1; // max is 5, but starting from 0 so max = length - 1

            var isAllowed = true;
            if (point.X > max) isAllowed = false;
            else if (point.X < 0) isAllowed = false;
            else if (point.Y > max) isAllowed = false;
            else if (point.Y < 0) isAllowed = false;

            return isAllowed;
        }

        void Populate() // not needed but was fun
        {
            Points = new Point[Length * Length];

            int y = 0;
            int x = 0;
            for (int i = 0; i < Length * Length; i++)
            {
                Points[i] = new Point { X = x, Y = y };
                x++;
                if (x == Length) // determine when to start incrementing y
                {
                    x = 0; // also reset x
                    y++;
                }
                if (y == Length) // i < length * length should mean this never occurs, but to be sure break
                {
                    break;
                }
            }
        }
    }
}
