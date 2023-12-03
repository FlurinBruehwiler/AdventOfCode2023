var input = """
            Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
            """;

input = File.ReadAllText("input.txt");

var res = input.Trim().Split("\n").Select(x => new
{
    id = int.Parse(x.Split(' ', ':')[1].Trim()),
    power = x.Split(':', ';', ',').Skip(1)
        .Select(o => new
        {
            num = int.Parse(o.Trim().Split(" ").First().Trim()),
            color = o.Trim().Split().Last().Trim()
        }).GroupBy(b => b.color)
        .Select(b => b.Max(z => z.num))
        .Aggregate((b, c) => b * c)
}).Sum(x => x.power);

Console.WriteLine(res);
