var lines = File.ReadAllLines("../../../input.txt");
var totalLines = lines.Length;
var total = 0;
var currentRowIndex = 1;
var totalAs = 0;

while (currentRowIndex < totalLines - 1)
{
    var currentColIndex = 0;
    var line = lines[currentRowIndex];

    while (currentColIndex <= line.Length - 3)
    {
        currentColIndex++;

        var character = line[currentColIndex];
        if (character == 'A')
        {
            totalAs++;

            // find diagonal to the bottom left
            var charSouthwest = lines[currentRowIndex + 1][currentColIndex - 1];
            if (charSouthwest != 'S' && charSouthwest != 'M') continue;

            // find diagonal to the bottom right
            var charSoutheast = lines[currentRowIndex + 1][currentColIndex + 1];
            if (charSoutheast != 'S' && charSoutheast != 'M') continue;

            // find diagonal to the top left
            var charNorthwest = lines[currentRowIndex - 1][currentColIndex - 1];
            if (charNorthwest != 'S' && charNorthwest != 'M') continue;

            // find diagonal to the top right
            var charNortheast = lines[currentRowIndex - 1][currentColIndex + 1];
            if (charNortheast != 'S' && charNortheast != 'M') continue;

            if (FoundXMASAtCoordinates(charNorthwest, charNortheast, charSoutheast, charSouthwest)) total++;
        }
    }

    currentRowIndex++;
}

Console.WriteLine(total);

static bool FoundXMASAtCoordinates(char charNorthwest, char charNortheast, char charSoutheast, char charSouthwest)
{
    if (charNorthwest == 'M' && charSoutheast != 'S') return false;
    if (charNorthwest == 'S' && charSoutheast != 'M') return false;

    if (charNortheast == 'M' && charSouthwest != 'S') return false;
    if (charNortheast == 'S' && charSouthwest != 'M') return false;

    return true;
}
