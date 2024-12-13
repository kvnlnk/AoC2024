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
            if (counter == 3)
            {
                _clawMachines.Add(new ClawMachine
                {
                    ControlMatrix = new[,] { { controlMatrix[0], controlMatrix[2] }, { controlMatrix[1], controlMatrix[3]} },
                    PrizeLocation = [prizeLocation[0], prizeLocation[1]]
                });

                counter = 0;
                controlMatrix.Clear();
                prizeLocation.Clear();
            }
            if(line == string.Empty) continue;
            
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
        }
    }

    
    public string GetPartOneSolution()
    {
        var sum = 0;

        foreach (var clawMachine in _clawMachines)
        {
            var minCost = int.MaxValue;
            var x1 = clawMachine.ControlMatrix[0, 0];
            var x2 = clawMachine.ControlMatrix[1, 0];
            var y1 = clawMachine.ControlMatrix[0, 1];
            var y2 = clawMachine.ControlMatrix[1, 1];
            
            // Reduce amount of iterations
            var aMax = x1 > x2 ? x2 : x1;
            var bMax = y1 > y2 ? y2 : y1;
            var xPrize = clawMachine.PrizeLocation[0];
            var yPrize = clawMachine.PrizeLocation[1];
            
            
            for (var a = 0; a < xPrize / aMax; a++)
            {
                for (var b = 0; b < yPrize / bMax; b++)
                {
                    var xVal = a * x1 + b * y1 ;
                    var yVal = a * x2 + b * y2;
                    
                    if (xVal != xPrize || yVal != yPrize) continue;

                    var cost = a * 3 + b;

                    if (cost < minCost)
                    {
                        minCost = cost;
                    }
                }
            }
            
            if (minCost == int.MaxValue) continue;
            sum += minCost;
        }
        return sum.ToString();
    }

    public string GetPartTwoSolution()
    {
        throw new NotImplementedException();
    }


    private class ClawMachine
    {
        public required int[,] ControlMatrix { get; set; }
        public required int[] PrizeLocation { get; set; }
    }
}