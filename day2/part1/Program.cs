var lines = File.ReadAllLines("../../../input.txt");
var totalLinesSafe = 0;

foreach (var line in lines)
{
    var lineData = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var index = 0;
    var dataLength = lineData.Length;

    var currentlyIncreasing = false;
    var currentlyDecreasing = false;
    var lineIsSafe = true;

    while (index < dataLength - 1 && lineIsSafe)
    {
        var currentNumber = int.Parse(lineData[index]);
        var nextNumber = int.Parse(lineData[index + 1]);

        var difference = currentNumber - nextNumber;

        if (difference == 0) lineIsSafe = false;
        else if (Math.Abs(difference) > 3) lineIsSafe = false;
        else if (difference < 0)
        {
            if (currentlyIncreasing) lineIsSafe = false;
            else currentlyDecreasing = true;
        }
        else
        {
            if (currentlyDecreasing) lineIsSafe = false;
            else currentlyIncreasing = true;
        }

        index++;
    }

    if (lineIsSafe) totalLinesSafe++;
}

Console.WriteLine(totalLinesSafe);
