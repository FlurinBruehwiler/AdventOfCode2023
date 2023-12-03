const string input = """
                     1abc2
                     pqr3stu8vwx
                     a1b2c3d4e5f
                     treb7uchet
                     """;

var res = input.Split("\n").Sum(x => int.Parse($"{x.First(char.IsNumber)}{x.Last(char.IsNumber)}"));

Console.WriteLine(res);
