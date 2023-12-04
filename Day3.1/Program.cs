var input = """
            467..114..
            ...*......
            ..35..633.
            ......#...
            617*......
            .....+.58.
            ..592.....
            ......755.
            ...$.*....
            .664.598..
            """;

input = File.ReadAllText("input.txt");

var inputLines = input.Trim()
    .Split("\n")
    .Select(x => x.Trim())
    .ToList();

var numBuilder = string.Empty;

var numbers = new List<Number>();

var width = inputLines.First().Length;

for (var i = 0; i < inputLines.Count; i++)
{
    var inputLine = inputLines[i];
    for (var y = 0; y < inputLine.Length; y++)
    {
        var c = inputLine[y];
        if (char.IsNumber(c))
        {
            numBuilder += c;
        }
        else //is not a number
        {
            if (numBuilder != string.Empty) //was previously a number
            {
                numbers.Add(new Number(int.Parse(numBuilder), i, y - numBuilder.Length, y - 1));
                numBuilder = string.Empty;
            }
        }
    }

    if (numBuilder != string.Empty) //was previously a number
    {
        numbers.Add(new Number(int.Parse(numBuilder), i,  width - numBuilder.Length, width - 1));
        numBuilder = string.Empty;
    }
}

var result = numbers.Where(IsPartNumber).Sum(x => x.Value);

Console.WriteLine(result);

bool IsPartNumber(Number number)
{
    for (var column = number.StartColumn - 1; column <= number.EndColumn + 1; column++)
    {
        for (var row = number.Row - 1; row <= number.Row + 1; row++)
        {
            if (IsSymbol(row, column))
                return true;
        }
    }

    return false;
}

bool IsSymbol(int row, int column)
{
    if (row < 0 || row >= inputLines.Count)
        return false;

    if (column < 0 || column >= width)
        return false;

    if (inputLines[row][column] == '.')
        return false;

    if (char.IsNumber(inputLines[row][column]))
        return false;

    return true;
}

record Number(int Value, int Row, int StartColumn, int EndColumn);

