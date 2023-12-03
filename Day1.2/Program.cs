const string input = """
                     two1nine
                     eightwothree
                     abcone2threexyz
                     xtwone3four
                     4nineeightseven2
                     zoneight234
                     7pqrstsixteen
                     """;

var numbers = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eigth", "nine" };

var res = input.Split("\n").Sum(x => int.Parse($"{Get(x, true)}{Get(x, false)}"));

Console.WriteLine(res);

int Get(string x, bool first)
{
    var res1 = Num(x, first, out var num1, out var idx1);
    var res2 = NumAsString(x, first, out var num2, out var idx2);

    if (res1 && res2)
    {
        if (idx1 > idx2)
        {
            return first ? num2 : num1;
        }

        return first ? num1 : num2;
    }
    if (res1)
    {
        return num1;
    }
    if (res2)
    {
        return num2;
    }
    throw new Exception();
}

bool Num(string luv, bool first, out int num, out int idx)
{
    num = 0;
    idx = 0;
    char? firstNum = first ? luv.FirstOrDefault(char.IsNumber) : luv.LastOrDefault(char.IsNumber);

    if (firstNum is null)
        return false;

    idx = first ? luv.IndexOf(firstNum.Value) : luv.LastIndexOf(firstNum.Value);
    num = firstNum.Value - '0';
    return true;
}

bool NumAsString(string luv, bool first, out int num, out int idx)
{
    num = 0;
    idx = 0;
    var ll = numbers.Select(x => new { idx =  luv.IndexOf(x, StringComparison.Ordinal), num = x});
    var abc = first ? ll.FirstOrDefault(x => x.idx != -1) : ll.LastOrDefault(x => x.idx != -1);
    if (abc is null)
        return false;
    num = numbers.IndexOf(abc.num) + 1;
    idx = first ? luv.IndexOf(abc.num, StringComparison.Ordinal) : luv.LastIndexOf(abc.num, StringComparison.Ordinal);
    return true;
}
