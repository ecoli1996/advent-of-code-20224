var lines = File.ReadAllLines("../../../input.txt");
var roadblockCoordinates = new List<(int, int)>();
var guardCoordinate = (0, 0);
var currentDirection = CurrentDirection.North;
var rowNumber = 0;
var possibleObstructions = new Stack<(int, int)>();
var lineLength = lines[0].Length;

foreach (var line in lines)
{
    var columnNumber = 0;
    foreach (var possibleRoadBlock in line)
    {
        if (possibleRoadBlock == '#') roadblockCoordinates.Add((columnNumber, rowNumber));
        else if (possibleRoadBlock == '^')
        {
            guardCoordinate = (columnNumber, rowNumber);
            currentDirection = CurrentDirection.North;
        }
        else if (possibleRoadBlock == '>')
        {
            guardCoordinate = (columnNumber, rowNumber);
            currentDirection = CurrentDirection.East;
        }
        else if (possibleRoadBlock == 'v')
        {
            guardCoordinate = (columnNumber, rowNumber);
            currentDirection = CurrentDirection.South;
        }
        else if (possibleRoadBlock == '<')
        {
            guardCoordinate = (columnNumber, rowNumber);
            currentDirection = CurrentDirection.West;
        }
        else
        {
            possibleObstructions.Push((columnNumber, rowNumber));
        }
        columnNumber++;
    }
    rowNumber++;
}

var obsTotal = 0;
var startingCoord = guardCoordinate;
var startingDirection = currentDirection;

while (possibleObstructions.Count > 0)
{
    var escaped = false;
    var determinedInLoop = false;
    var traversedPath = new HashSet<(int, int, CurrentDirection)>
    {
        (startingCoord.Item1, startingCoord.Item2, startingDirection)
    };
    guardCoordinate = startingCoord;
    currentDirection = startingDirection;

    var newRoadBlock = possibleObstructions.Pop();
    var newRoadBlocks = new List<(int, int)>();
    newRoadBlocks.AddRange(roadblockCoordinates);
    newRoadBlocks.Add(newRoadBlock);

    while (!escaped && !determinedInLoop)
    {
        if (currentDirection == CurrentDirection.North)
        {
            var roadblocksInSameColumn = newRoadBlocks.Where(x => x.Item1 == guardCoordinate.Item1 && x.Item2 < guardCoordinate.Item2);
            if (roadblocksInSameColumn?.Any() ?? false)
            {
                var roadblock = roadblocksInSameColumn.MaxBy(x => x.Item2);

                guardCoordinate = (guardCoordinate.Item1, roadblock.Item2 + 1);
                currentDirection = CurrentDirection.East;
            }
            else
            {
                escaped = true;
            }
        }
        else if (currentDirection == CurrentDirection.East)
        {
            var roadblocksInSameRow = newRoadBlocks.Where(x => x.Item2 == guardCoordinate.Item2 && x.Item1 > guardCoordinate.Item1);
            if (roadblocksInSameRow?.Any() ?? false)
            {
                var roadblock = roadblocksInSameRow.MinBy(x => x.Item1);

                guardCoordinate = (roadblock.Item1 - 1, guardCoordinate.Item2);
                currentDirection = CurrentDirection.South;
            }
            else
            {
                escaped = true;
            }
        }
        else if (currentDirection == CurrentDirection.South)
        {
            var roadblocksInSameColumn = newRoadBlocks.Where(x => x.Item1 == guardCoordinate.Item1 && x.Item2 > guardCoordinate.Item2);
            if (roadblocksInSameColumn?.Any() ?? false)
            {
                var roadblock = roadblocksInSameColumn.MinBy(x => x.Item2);

                guardCoordinate = (guardCoordinate.Item1, roadblock.Item2 - 1);
                currentDirection = CurrentDirection.West;
            }
            else
            {
                escaped = true;
            }
        }
        else if (currentDirection == CurrentDirection.West)
        {
            var roadblocksInSameColumn = newRoadBlocks.Where(x => x.Item2 == guardCoordinate.Item2 && x.Item1 < guardCoordinate.Item1);
            if (roadblocksInSameColumn?.Any() ?? false)
            {
                var roadblock = roadblocksInSameColumn.MaxBy(x => x.Item1);

                guardCoordinate = (roadblock.Item1 + 1, guardCoordinate.Item2);
                currentDirection = CurrentDirection.North;
            }
            else
            {
                escaped = true;
            }
        }

        determinedInLoop = !traversedPath.Add((guardCoordinate.Item1, guardCoordinate.Item2, currentDirection));
    }

    if (!escaped) obsTotal++;
}

Console.WriteLine(obsTotal);

enum CurrentDirection
{
    North,
    South,
    East,
    West
}
