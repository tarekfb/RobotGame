using System.Drawing;

namespace RobotGame
{
    public class Robot
    {
        public Point Position { get; set; }
        public Direction Facing { get; set; }
        Point[] Points { get; set; }
        int _length = 5;

        public Robot()
        {
            //Populate(); // not neccessary code but maybe useful in future
        }
        public Robot(int length)
        {
            _length = length;
            //Populate(); // not neccessary code but maybe useful in future
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
            else Console.WriteLine($"Oh no! You fell off the table. Putting you back at... {Position.X}, {Position.Y} facing {Facing}.");
        }

        bool IsValidPos(Point point) // check if new pos is outside of grid
        {
            int max = _length - 1; // max is 5, but starting from 0 so max = length - 1

            var isAllowed = true;
            if (point.X > max) isAllowed = false;
            else if (point.X < 0) isAllowed = false;
            else if (point.Y > max) isAllowed = false;
            else if (point.Y < 0) isAllowed = false;

            return isAllowed;
        }

        void Populate() // not needed but was fun
        {
            Points = new Point[_length * _length];

            int y = 0;
            int x = 0;
            for (int i = 0; i < _length * _length; i++)
            {
                Points[i] = new Point { X = x, Y = y };
                y++;
                if (y == _length) // determine when to start incrementing x
                {
                    y = 0; // also reset y
                    x++;
                }
                if (x == _length) // i < length * length should mean this never occurs, but to be sure break
                {
                    break;
                }
            }
        }
    }
}
