using AoC2024.Common;

namespace AoC2024.Day13;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<ClawMachine> _clawMachines = [];


    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }


    private void SeparateList()
    {
        var counter = 0;

        var controlMatrix = new List<int>();
        var prizeLocation = new List<int>();

        foreach (var line in _list)
        {
            if (line == string.Empty) continue;

            var splitLine = line.Split([":", ",", "+", "="], StringSplitOptions.RemoveEmptyEntries);

            switch (splitLine[0])
            {
                case "Button A":
                case "Button B":
                    controlMatrix.Add(Convert.ToInt32(splitLine[2]));
                    controlMatrix.Add(Convert.ToInt32(splitLine[4]));
                    break;
                default:
                    prizeLocation.Add(Convert.ToInt32(splitLine[2]));
                    prizeLocation.Add(Convert.ToInt32(splitLine[4]));
                    break;
            }

            counter++;

            if (counter == 3)
            {
                _clawMachines.Add(new ClawMachine
                {
                    ControlMatrix = new[,]
                        { { controlMatrix[0], controlMatrix[2] }, { controlMatrix[1], controlMatrix[3] } },
                    PrizeLocation = [prizeLocation[0], prizeLocation[1]]
                });

                counter = 0;
                controlMatrix.Clear();
                prizeLocation.Clear();
            }
        }
    }


    public string GetPartOneSolution()
    {
        return _clawMachines.Sum(clawMachine => CramersRule(clawMachine, 0)).ToString();
    }


    public string GetPartTwoSolution()
    {
        return _clawMachines.Sum(clawMachine => CramersRule(clawMachine, 10000000000000)).ToString();
    }


    private long CramersRule(ClawMachine clawMachine, long offset)
    {
        var x1 = clawMachine.ControlMatrix[0, 0];
        var x2 = clawMachine.ControlMatrix[1, 0];
        var y1 = clawMachine.ControlMatrix[0, 1];
        var y2 = clawMachine.ControlMatrix[1, 1];
        var xPrize = clawMachine.PrizeLocation[0] + offset;
        var yPrize = clawMachine.PrizeLocation[1] + offset;

        var det = x1 * y2 - x2 * y1;
        var det1 = xPrize * y2 - yPrize * y1;
        var det2 = x1 * yPrize - x2 * xPrize;

        var x = det1 / det;
        var y = det2 / det;
        if (x * x1 + y * y1 != xPrize || x * x2 + y * y2 != yPrize) return 0;
        return 3 * det1 / det + det2 / det;
    }


    private class ClawMachine
    {
        public required int[,] ControlMatrix { get; set; }
        public required int[] PrizeLocation { get; set; }
    }
}