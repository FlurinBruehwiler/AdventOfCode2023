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

var inputColumns = input.Trim()
    .Split("\n")
    .Select(x => x.Trim())
    .ToList();

var numBuilder = string.Empty;

var numbers = new List<Number>();

var width = inputColumns.First().Length;

for (var i = 0; i < inputColumns.Count; i++)
{
    var inputLine = inputColumns[i];
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

var result = 0;

for (var row = 0; row < inputColumns.Count; row++)
{
    var inputRow = inputColumns[row];
    for (var column = 0; column < inputRow.Length; column++)
    {
        var c = inputRow[column];
        if (c == '*')
        {
            if (IsGear(row, column, out var ratio))
            {
                result += ratio;
            }
        }
    }
}

Console.WriteLine(result);

bool IsGear(int symbolRow, int symbolColumn, out int ratio)
{
    ratio = 0;

    Number? first = null;

    for (var column = symbolColumn - 1; column <= symbolColumn + 1; column++)
    {
        for (var row = symbolRow - 1; row <= symbolRow + 1; row++)
        {
            var row1 = row;
            var column1 = column;
            var res = numbers.FirstOrDefault(x => x.Row == row1 && column1 >= x.StartColumn && column1 <= x.EndColumn);

            if (res is not null)
            {
                if (first is null)
                {
                    first = res;
                }
                else if(first != res)
                {
                    ratio = first.Value * res.Value;
                    return true;
                }
            }
        }
    }

    return false;
}

record Number(int Value, int Row, int StartColumn, int EndColumn);

