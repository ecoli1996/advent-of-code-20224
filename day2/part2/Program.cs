using System;

var lines = File.ReadAllLines("../../../input.txt");
var totalLinesSafe = 0;

foreach (var line in lines)
{
    // Console.WriteLine(line);
    // Console.WriteLine();

    var lineData = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    int? indexToSkip = null;
    var numberOfColumns = lineData.Length;
    var lineIsSafe = false;

    while (!lineIsSafe && (indexToSkip ?? 0) < numberOfColumns)
    {
        lineIsSafe = LineIsSafe(lineData, indexToSkip);
        indexToSkip = indexToSkip == null ? 0 : indexToSkip + 1;
    }

    if (lineIsSafe) totalLinesSafe++;
}

Console.WriteLine(totalLinesSafe);

static bool LineIsSafe(string[] lineData, int? indexToSkip)
{
    var currentIndex = 0;
    var nextIndex = 0;
    var currentDirection = CurrentDirection.None;

    var lineLength = lineData.Length;
    var lineIsSafe = true;

    while (lineIsSafe && currentIndex < (lineLength - 1))
    {
        if (currentIndex == indexToSkip)
        {
            currentIndex++;
        }
        nextIndex = currentIndex + 1;
        if (nextIndex == indexToSkip)
        {
            nextIndex++;
        }

        if (nextIndex >= lineLength) break;

        var currentNumber = int.Parse(lineData[currentIndex]);
        var nextNumber = int.Parse(lineData[nextIndex]);

        var numberSafetyCheckResults = NeighborNumbersAreSafe(new LineSafetyCheckOptions
        {
            CurrentDirection = currentDirection,
            CurrentNumber = currentNumber,
            NextNumber = nextNumber
        });
        lineIsSafe = numberSafetyCheckResults.LineIsSafe;
        currentDirection = numberSafetyCheckResults.CurrentDirection;

        currentIndex++;
    }

    return lineIsSafe;
}

static LineSafetyCheckResults NeighborNumbersAreSafe(LineSafetyCheckOptions lineSafetyCheckOptions)
{
    var difference = lineSafetyCheckOptions.CurrentNumber - lineSafetyCheckOptions.NextNumber;
    var results = new LineSafetyCheckResults
    {
        LineIsSafe = true,
        CurrentDirection = CurrentDirection.None
    };

    if (difference == 0 || Math.Abs(difference) > 3)
    {
        results.LineIsSafe = false;
    }

    if (difference < 0)
    {
        results.CurrentDirection = CurrentDirection.Decreasing;
        if (lineSafetyCheckOptions.CurrentDirection == CurrentDirection.Increasing)
        {
            results.LineIsSafe = false;
        }
    }
    else if (difference > 0)
    {
        results.CurrentDirection = CurrentDirection.Increasing;
        if (lineSafetyCheckOptions.CurrentDirection == CurrentDirection.Decreasing)
        {
            results.LineIsSafe = false;
        }
    }

    /*
        Console.WriteLine($"Current Number: {lineSafetyCheckOptions.CurrentNumber}");
        Console.WriteLine($"Next Number: {lineSafetyCheckOptions.NextNumber}");
        Console.WriteLine($"Difference: {difference}");
        Console.WriteLine($"Current Dir: {lineSafetyCheckOptions.CurrentDirection}");
        Console.WriteLine($"New Dir: {results.CurrentDirection}");
        Console.WriteLine($"Line Is Safe: {results.LineIsSafe}");
        Console.WriteLine();
    */

    return results;
}

enum CurrentDirection
{
    None,
    Increasing,
    Decreasing
}

class LineSafetyCheckOptions
{
    public int CurrentNumber { get; set; }
    public int NextNumber { get; set; }
    public CurrentDirection CurrentDirection { get; set; }
}

class LineSafetyCheckResults
{
    public CurrentDirection CurrentDirection { get; set; }
    public bool LineIsSafe { get; set; }
}
