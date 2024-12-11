var lines = File.ReadAllLines("../../../input.txt");
var currentRowIndex = 0;
var currentLine = lines[currentRowIndex];
var rules = new Dictionary<int, HashSet<int>>();

while (!string.IsNullOrWhiteSpace(currentLine))
{
    var beforeAndAfter = currentLine.Split("|");
    var before = int.Parse(beforeAndAfter[0]);
    var after = int.Parse(beforeAndAfter[1]);

    var added = rules.TryAdd(before, new HashSet<int> { after });
    if (!added) rules[before].Add(after);

    currentRowIndex++;
    currentLine = lines[currentRowIndex];
}

currentRowIndex++;
var sum = 0;

while (currentRowIndex < lines.Length)
{
    currentLine = lines[currentRowIndex];
    var pages = currentLine.Split(",");
    var checkedPages = new HashSet<int>();

    var middleNumberIndex = pages.Length / 2;
    var middleNumber = 0;
    var currentIndex = 0;
    var lineIsSafe = true;

    foreach (var page in pages)
    {
        var pageNumber = int.Parse(page);
        if (currentIndex == middleNumberIndex) middleNumber = pageNumber;
        if (rules.TryGetValue(pageNumber, out var requiredAfterPages))
        {
            if (checkedPages.Overlaps(requiredAfterPages))
            {
                lineIsSafe = false;
                break;
            }
        }

        checkedPages.Add(pageNumber);
        currentIndex++;
    }

    if (lineIsSafe) sum += middleNumber;
    currentRowIndex++;
}

Console.WriteLine(sum);

