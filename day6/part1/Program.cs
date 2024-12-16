var lines = File.ReadAllLines("../../../input.txt");
var roadblockCoordinates = new List<(int, int)>();
var guardCoordinate = (0, 0);
var currentDirection = CurrentDirection.North;
var rowNumber = 0;
var traversedPath = new HashSet<(int, int)>();
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
        columnNumber++;
    }
    rowNumber++;
}

var escaped = false;
var movedPositions = 0;
traversedPath.Add(guardCoordinate);

while (!escaped)
{
    if (currentDirection == CurrentDirection.North)
    {
        var roadblocksInSameColumn = roadblockCoordinates.Where(x => x.Item1 == guardCoordinate.Item1 && x.Item2 < guardCoordinate.Item2);
        if (roadblocksInSameColumn?.Any() ?? false)
        {
            var roadblock = roadblocksInSameColumn.MaxBy(x => x.Item2);
            TraversePathNorth(guardCoordinate.Item2, roadblock.Item2);
            
            guardCoordinate = (guardCoordinate.Item1, roadblock.Item2 + 1);
            currentDirection = CurrentDirection.East;
        }
        else
        {
            TraversePathNorth(guardCoordinate.Item2 - 1, 0);
            escaped = true;
        }
    }
    else if (currentDirection == CurrentDirection.East)
    {
        var roadblocksInSameRow = roadblockCoordinates.Where(x => x.Item2 == guardCoordinate.Item2 && x.Item1 > guardCoordinate.Item1);
        if (roadblocksInSameRow?.Any() ?? false)
        {
            var roadblock = roadblocksInSameRow.MinBy(x => x.Item1);
            TraversePathEast(guardCoordinate.Item1, roadblock.Item1);
            
            guardCoordinate = (roadblock.Item1 - 1, guardCoordinate.Item2);
            currentDirection = CurrentDirection.South;
        }
        else
        {
            TraversePathEast(guardCoordinate.Item1, lineLength);
            escaped = true;
        }
    }
    else if (currentDirection == CurrentDirection.South)
    {
        var roadblocksInSameColumn = roadblockCoordinates.Where(x => x.Item1 == guardCoordinate.Item1 && x.Item2 > guardCoordinate.Item2);
        if (roadblocksInSameColumn?.Any() ?? false)
        {
            var roadblock = roadblocksInSameColumn.MinBy(x => x.Item2);
            TraversePathSouth(guardCoordinate.Item2, roadblock.Item2);

            guardCoordinate = (guardCoordinate.Item1, roadblock.Item2 - 1);
            currentDirection = CurrentDirection.West;
        }
        else
        {
            TraversePathSouth(guardCoordinate.Item2, lines.Length);
            escaped = true;
        }
    }
    else if (currentDirection == CurrentDirection.West)
    {
        var roadblocksInSameColumn = roadblockCoordinates.Where(x => x.Item2 == guardCoordinate.Item2 && x.Item1 < guardCoordinate.Item1);
        if (roadblocksInSameColumn?.Any() ?? false)
        {
            var roadblock = roadblocksInSameColumn.MaxBy(x => x.Item1);
            TraversePathWest(guardCoordinate.Item1, roadblock.Item1);

            guardCoordinate = (roadblock.Item1 + 1, guardCoordinate.Item2);
            currentDirection = CurrentDirection.North;
        }
        else
        {
            TraversePathWest(guardCoordinate.Item1, 0);
            escaped = true;
        }
    }
}

void TraversePathNorth(int startingValue, int endingValue)
{
    for (var i = startingValue; i > endingValue; i--)
    {
        var newCoord = traversedPath.Add((guardCoordinate.Item1, i));
        if (newCoord) movedPositions++;
    }
}

void TraversePathSouth(int startingValue, int endingValue)
{
    for (var i = startingValue; i < endingValue; i++)
    {
        var newCoord = traversedPath.Add((guardCoordinate.Item1, i));
        if (newCoord) movedPositions++;
    }
}

void TraversePathEast(int startingValue, int endingValue)
{
    for (var i = startingValue; i < endingValue; i++)
    {
        var newCoord = traversedPath.Add((i, guardCoordinate.Item2));
        if (newCoord) movedPositions++;
    }
}

void TraversePathWest(int startingValue, int endingValue)
{
    for (var i = startingValue; i > endingValue; i--)
    {
        var newCoord = traversedPath.Add((i, guardCoordinate.Item2));
        if (newCoord) movedPositions++;
    }
}

Console.WriteLine(movedPositions + 1);

enum CurrentDirection
{
    North,
    South,
    East,
    West
}
