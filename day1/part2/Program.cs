var lines = File.ReadAllLines("../../../input.txt");
var distancesCol1Group1 = new Dictionary<int, int>();
var distancesCol1Group2 = new Dictionary<int, int>();
var distancesCol1Group3 = new Dictionary<int, int>();
var distancesCol1Group4 = new Dictionary<int, int>();
var distancesCol1Group5 = new Dictionary<int, int>();
var distancesCol1Group6 = new Dictionary<int, int>();
var distancesCol1Group7 = new Dictionary<int, int>();
var distancesCol1Group8 = new Dictionary<int, int>();
var distancesCol1Group9 = new Dictionary<int, int>();

var distancesCol2Group1 = new Dictionary<int, int>();
var distancesCol2Group2 = new Dictionary<int, int>();
var distancesCol2Group3 = new Dictionary<int, int>();
var distancesCol2Group4 = new Dictionary<int, int>();
var distancesCol2Group5 = new Dictionary<int, int>();
var distancesCol2Group6 = new Dictionary<int, int>();
var distancesCol2Group7 = new Dictionary<int, int>();
var distancesCol2Group8 = new Dictionary<int, int>();
var distancesCol2Group9 = new Dictionary<int, int>();

foreach (var line in lines)
{
    var distances = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var distance1 = distances[0];
    var distance2 = distances[1];

    AddToCol1(distance1);
    AddToCol2(distance2);
}

var totalSimilarityScore = 0;

AddGroupToSimilarityScore(distancesCol1Group1, distancesCol2Group1);
AddGroupToSimilarityScore(distancesCol1Group2, distancesCol2Group2);
AddGroupToSimilarityScore(distancesCol1Group3, distancesCol2Group3);
AddGroupToSimilarityScore(distancesCol1Group4, distancesCol2Group4);
AddGroupToSimilarityScore(distancesCol1Group5, distancesCol2Group5);
AddGroupToSimilarityScore(distancesCol1Group6, distancesCol2Group6);
AddGroupToSimilarityScore(distancesCol1Group7, distancesCol2Group7);
AddGroupToSimilarityScore(distancesCol1Group8, distancesCol2Group8);
AddGroupToSimilarityScore(distancesCol1Group9, distancesCol2Group9);

Console.WriteLine(totalSimilarityScore);

void AddGroupToSimilarityScore(Dictionary<int, int> col1Group, Dictionary<int, int> col2Group)
{
    foreach (var distanceInCol1 in col1Group)
    {
        var numberOfTimesInCol1 = distanceInCol1.Value;
        var existsInCol2 = col2Group.TryGetValue(distanceInCol1.Key, out var numberOfTimesInCol2);
        if (!existsInCol2) numberOfTimesInCol2 = 0;

        var currentSimilarityScore = distanceInCol1.Key * numberOfTimesInCol2 * numberOfTimesInCol1;
        totalSimilarityScore += currentSimilarityScore;
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

void TryAddThenIncrease(Dictionary<int, int> group, int key)
{
    var added = group.TryAdd(key, 1);
    if (!added) IncreaseValue(group, key);
}

void IncreaseValue(Dictionary<int, int> group, int key)
{
    var currentValue = group[key];
    group[key] = currentValue + 1;
}
