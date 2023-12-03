string input = """
                     two1nine
                     eightwothree
                     abcone2threexyz
                     xtwone3four
                     4nineeightseven2
                     zoneight234
                     7pqrstsixteen
                     """;

input = File.ReadAllText("input.txt");

var numbers = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

var res = input.Trim().Split("\n").Sum(x => int.Parse($"{GetWrap(x, true)}{GetWrap(x, false)}"));

Console.WriteLine(res);

int GetWrap(string x, bool first)
{
    x = x.Trim();
    var res = Get(x, first);
    Console.WriteLine($"{x}: {res}");
    return res;
}

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
    var luvv = luv.Select(x => (char?)x);
    char? firstNum = first ? luvv.FirstOrDefault(x => char.IsNumber(x.Value)) : luvv.LastOrDefault(c => char.IsNumber(c.Value));

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
    var ll = numbers.Select(x => new { idx =  luv.IndexOf(x, StringComparison.Ordinal), num = x}).Where(x => x.idx != -1).OrderBy(x => x.idx);
    var abc = first ? ll.FirstOrDefault(x => x.idx != -1) : ll.LastOrDefault(x => x.idx != -1);
    if (abc is null)
        return false;
    num = numbers.IndexOf(abc.num) + 1;
    idx = first ? luv.IndexOf(abc.num, StringComparison.Ordinal) : luv.LastIndexOf(abc.num, StringComparison.Ordinal);
    return true;
}
