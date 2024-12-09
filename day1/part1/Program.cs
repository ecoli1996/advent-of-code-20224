var lines = File.ReadAllLines("../../../input.txt");
var distancesCol1Group1 = new SortedDictionary<int, int>();
var distancesCol1Group2 = new SortedDictionary<int, int>();
var distancesCol1Group3 = new SortedDictionary<int, int>();
var distancesCol1Group4 = new SortedDictionary<int, int>();
var distancesCol1Group5 = new SortedDictionary<int, int>();
var distancesCol1Group6 = new SortedDictionary<int, int>();
var distancesCol1Group7 = new SortedDictionary<int, int>();
var distancesCol1Group8 = new SortedDictionary<int, int>();
var distancesCol1Group9 = new SortedDictionary<int, int>();

var distancesCol2Group1 = new SortedDictionary<int, int>();
var distancesCol2Group2 = new SortedDictionary<int, int>();
var distancesCol2Group3 = new SortedDictionary<int, int>();
var distancesCol2Group4 = new SortedDictionary<int, int>();
var distancesCol2Group5 = new SortedDictionary<int, int>();
var distancesCol2Group6 = new SortedDictionary<int, int>();
var distancesCol2Group7 = new SortedDictionary<int, int>();
var distancesCol2Group8 = new SortedDictionary<int, int>();
var distancesCol2Group9 = new SortedDictionary<int, int>();

foreach (var line in lines)
{
    var distances = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var distance1 = distances[0];
    var distance2 = distances[1];

    AddToCol1(distance1);
    AddToCol2(distance2);
}

var allDistancesInCol1Sorted = new Queue<int>();
var allDistancesInCol2Sorted = new Queue<int>();

SortDistancesFromGroup(distancesCol1Group1, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group2, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group3, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group4, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group5, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group6, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group7, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group8, allDistancesInCol1Sorted);
SortDistancesFromGroup(distancesCol1Group9, allDistancesInCol1Sorted);

SortDistancesFromGroup(distancesCol2Group1, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group2, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group3, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group4, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group5, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group6, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group7, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group8, allDistancesInCol2Sorted);
SortDistancesFromGroup(distancesCol2Group9, allDistancesInCol2Sorted);

var distanceDifferences = 0;

for (var i = 0; i < lines.Length; i++)
{
    var currentDistanceSorted1 = allDistancesInCol1Sorted.Dequeue();
    var currentDistanceSorted2 = allDistancesInCol2Sorted.Dequeue();

    distanceDifferences += Math.Abs(currentDistanceSorted1 - currentDistanceSorted2);
}

Console.WriteLine(distanceDifferences);

void SortDistancesFromGroup(SortedDictionary<int, int> distancesInGroup, Queue<int> allDistancesSorted)
{
    foreach (var distanceInGroup in distancesInGroup)
    {
        for (var i = 0; i < distanceInGroup.Value; i++)
        {
            allDistancesSorted.Enqueue(distanceInGroup.Key);
        }
    }
}

void AddToCol1(string distance1)
{
    var firstChar = distance1[0];
    var distance1Value = int.Parse(distance1);

    switch (firstChar)
    {
        case '1':
            TryAddThenIncrease(distancesCol1Group1, distance1Value);
            break;
        case '2':
            TryAddThenIncrease(distancesCol1Group2, distance1Value);
            break;
        case '3':
            TryAddThenIncrease(distancesCol1Group3, distance1Value);
            break;
        case '4':
            TryAddThenIncrease(distancesCol1Group4, distance1Value);
            break;
        case '5':
            TryAddThenIncrease(distancesCol1Group5, distance1Value);
            break;
        case '6':
            TryAddThenIncrease(distancesCol1Group6, distance1Value);
            break;
        case '7':
            TryAddThenIncrease(distancesCol1Group7, distance1Value);
            break;
        case '8':
            TryAddThenIncrease(distancesCol1Group8, distance1Value);
            break;
        case '9':
            TryAddThenIncrease(distancesCol1Group9, distance1Value);
            break;
    }
}


void AddToCol2(string distance2)
{
    var firstChar = distance2[0];
    var distance2Value = int.Parse(distance2);

    switch (firstChar)
    {
        case '1':
            TryAddThenIncrease(distancesCol2Group1, distance2Value);
            break;
        case '2':
            TryAddThenIncrease(distancesCol2Group2, distance2Value);
            break;
        case '3':
            TryAddThenIncrease(distancesCol2Group3, distance2Value);
            break;
        case '4':
            TryAddThenIncrease(distancesCol2Group4, distance2Value);
            break;
        case '5':
            TryAddThenIncrease(distancesCol2Group5, distance2Value);
            break;
        case '6':
            TryAddThenIncrease(distancesCol2Group6, distance2Value);
            break;
        case '7':
            TryAddThenIncrease(distancesCol2Group7, distance2Value);
            break;
        case '8':
            TryAddThenIncrease(distancesCol2Group8, distance2Value);
            break;
        case '9':
            TryAddThenIncrease(distancesCol2Group9, distance2Value);
            break;
    }
}

void TryAddThenIncrease(SortedDictionary<int, int> group, int key)
{
    var added = group.TryAdd(key, 1);
    if (!added) IncreaseValue(group, key);
}

void IncreaseValue(SortedDictionary<int, int> group, int key)
{
    var currentValue = group[key];
    group[key] = currentValue + 1;
}
