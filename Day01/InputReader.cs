namespace AoC2024.Day01;

public class InputReader
{
    private readonly string day;

    public InputReader(string day)
    {
        this.day = day;
    }

    public List<string> ReadFile()
    {
        var stringList = new List<string>();

        using var streamReader = new StreamReader($@"Day{day}/Input.txt");
        while (streamReader.ReadLine() is { } line)
        {
            stringList.Add(line);
        }

        return stringList;
    }
}