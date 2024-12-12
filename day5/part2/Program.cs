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
var linesMaybeMadeSafe = new List<List<int>>();

while (currentRowIndex < lines.Length)
{
    currentLine = lines[currentRowIndex];
    var pages = currentLine.Split(",");
    var pageLength = pages.Length;
    var checkedPages = new HashSet<int>();
    var possiblySafeList = new List<int>();
    var lineIsSafe = true;
    Console.WriteLine();
    for (var i=0; i < pageLength; i++)
    {
        var pageNumber = int.Parse(pages[i]);
        if (rules.TryGetValue(pageNumber, out var requiredAfterPages))
        {
            if (checkedPages.Overlaps(requiredAfterPages))
            {
                Console.WriteLine($"found issue with line {currentRowIndex} - {currentLine}\n");
                var shouldBeAfter = possiblySafeList.Intersect(requiredAfterPages);
                var fineBefore = possiblySafeList.Except(shouldBeAfter);
                var newLst = new List<int>();
                newLst.AddRange(fineBefore);
                newLst.Add(pageNumber);
                newLst.AddRange(shouldBeAfter);
                possiblySafeList = newLst;

                Console.Write(string.Join(",", newLst));
                Console.WriteLine();
                lineIsSafe = false;
            }
            else possiblySafeList.Add(pageNumber);
        }
        else possiblySafeList.Add(pageNumber);

        checkedPages.Add(pageNumber);
    }
    if (!lineIsSafe) linesMaybeMadeSafe.Add(possiblySafeList);

    currentRowIndex++;
}

foreach(var possiblySafeList in linesMaybeMadeSafe)
{
    Console.WriteLine($"checking new line {string.Join(",", possiblySafeList)}\n");
    var middleNumberIndex = possiblySafeList.Count / 2;
    var middleNumber = 0;
    var currentIndex = 0;
    var checkedPages = new HashSet<int>();
    var lineIsSafe = true;
    Console.WriteLine();
    foreach (var pageNumber in possiblySafeList)
    {
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
}

Console.WriteLine(sum);

