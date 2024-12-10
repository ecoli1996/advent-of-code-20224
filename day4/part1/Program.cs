using System.Text.RegularExpressions;

var lines = File.ReadAllLines("../../../input.txt");
var totalLines = lines.Length;

var forwardPattern = "XMAS";
var backwardPattern = "SAMX";

var total = 0;
var currentRowIndex = 0;

foreach (var line in lines)
{
    var matchedForwards = Regex.Matches(line, forwardPattern).ToList();
    var matchedBackwards = Regex.Matches(line, backwardPattern);

    total += matchedForwards.Count + matchedBackwards.Count;

    var currentColIndex = 0;
    foreach (var character in line)
    {
        if (character != 'X')
        {
            currentColIndex++;
            continue;
        }

        // find diagonal to the bottom left
        if (currentColIndex >= 3 && currentRowIndex <= (totalLines - 4))
        {
            var mSouthwest = (currentRowIndex + 1, currentColIndex - 1);
            var aSouthwest = (currentRowIndex + 2, currentColIndex - 2);
            var sSouthwest = (currentRowIndex + 3, currentColIndex - 3);
            FoundXMASAtCoordinates(mSouthwest, aSouthwest, sSouthwest);
        }

        // find diagonal to the bottom right
        if (currentColIndex <= (line.Length - 4) && currentRowIndex <= (totalLines - 4))
        {
            var mSoutheast = (currentRowIndex + 1, currentColIndex + 1);
            var aSoutheast = (currentRowIndex + 2, currentColIndex + 2);
            var sSoutheast = (currentRowIndex + 3, currentColIndex + 3);
            FoundXMASAtCoordinates(mSoutheast, aSoutheast, sSoutheast);
        }

        // find diagonal to the top left
        if (currentColIndex >= 3 && currentRowIndex >= 3)
        {
            var mNortheast = (currentRowIndex - 1, currentColIndex - 1);
            var aNortheast = (currentRowIndex - 2, currentColIndex - 2);
            var sNortheast = (currentRowIndex - 3, currentColIndex - 3);
            FoundXMASAtCoordinates(mNortheast, aNortheast, sNortheast);
        }

        // find diagonal to the top right
        if (currentColIndex <= (line.Length - 4) && currentRowIndex >= 3)
        {
            var mNorthwest = (currentRowIndex - 1, currentColIndex + 1);
            var aNorthwest = (currentRowIndex - 2, currentColIndex + 2);
            var sNorthwest = (currentRowIndex - 3, currentColIndex + 3);
            FoundXMASAtCoordinates(mNorthwest, aNorthwest, sNorthwest);
        }

        // find straight up
        if (currentRowIndex >= 3)
        {
            var mNorth = (currentRowIndex - 1, currentColIndex);
            var aNorth = (currentRowIndex - 2, currentColIndex);
            var sNorth = (currentRowIndex - 3, currentColIndex);
            FoundXMASAtCoordinates(mNorth, aNorth, sNorth);
        }

        // find straight down
        if (currentRowIndex <= (totalLines - 4))
        {
            var mSouth = (currentRowIndex + 1, currentColIndex);
            var aSouth = (currentRowIndex + 2, currentColIndex);
            var sSouth = (currentRowIndex + 3, currentColIndex);
            FoundXMASAtCoordinates(mSouth, aSouth, sSouth);
        }

        currentColIndex++;
    }

    currentRowIndex++;
}
Console.WriteLine(total);

bool FoundXMASAtCoordinates((int, int) m, (int, int) a, (int, int) s)
{
    if (lines[m.Item1][m.Item2] == 'M' &&
        lines[a.Item1][a.Item2] == 'A' &&
        lines[s.Item1][s.Item2] == 'S')
    {
        total++;
        return true;
    }

    return false;
}
