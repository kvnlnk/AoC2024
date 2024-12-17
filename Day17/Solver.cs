using AoC2024.Common;

namespace AoC2024.Day17;

public class Solver : ISolver
{
    private readonly List<string> _list;
    private readonly List<int> _program = [];

    private int _registerA;
    private int _registerB;
    private int _registerC;
    
    private int _instructionPointer;
    private readonly List<int> _output = [];
    
    public Solver(List<string> list)
    {
        _list = list;
        SeparateList();
    }
    
    
    private void SeparateList()
    {
        foreach (var line in _list)
        {
            var splitLine = line.Split([":"], StringSplitOptions.RemoveEmptyEntries);

            if (splitLine.Length == 0) continue;
            
            switch (splitLine[0])
            {
                case "Register A":
                    _registerA = Convert.ToInt32(splitLine[1]);
                    break;
                case "Register B":
                    _registerB = Convert.ToInt32(splitLine[1]);
                    break;
                case "Register C":
                    _registerC = Convert.ToInt32(splitLine[1]);
                    break;
                case "Program":
                    foreach (var val in splitLine[1].Split(","))
                    {
                        _program.Add(Convert.ToInt32(val));
                    }
                    break;
            }
        }
    }
    
    public string GetPartOneSolution()
    {
        while (_instructionPointer < _program.Count-1)
        {
            GetInstruction(_program[_instructionPointer], _program[_instructionPointer + 1]);
        }
        return string.Join(",", _output);
    }



    public string GetPartTwoSolution()
    {
        return "";
    }

    private void GetInstruction(int opcode, int operand)
    {
        switch (opcode)
        {
            case 0:
                Adv(operand);
                break;
            case 1:
                Bxl(operand);
                break;
            case 2:
                Bst(operand);
                break;
            case 3:
                Jnz(operand);
                break;
            case 4:
                Bxc(operand);
                break;
            case 5:
                Out(operand);
                break;
            case 6:
                Bdv(operand);
                break;
            case 7:
                Cdv(operand);
                break;
        }
    }
    private int GetComboOperandValue(int comboOperand)
    {
        switch (comboOperand)
        {
            case 4:
                return _registerA;
            case 5:
                return _registerB;
            case 6:
                return _registerC;
            case 7:
                break;
        }
        return comboOperand;
    }
    
    
    private void Adv(int comboOperand)
    {
        _registerA = (int)(_registerA / Math.Pow(2, GetComboOperandValue(comboOperand)));
        _instructionPointer += 2;
    }

    private void Bxl(int literalOperand)
    {
        _registerB ^= literalOperand;
        _instructionPointer += 2;
    }

    private void Bst(int comboOperand)
    {
        _registerB = GetComboOperandValue(comboOperand) % 8;
        _instructionPointer += 2;
    }

    private void Jnz(int literalOperand)
    {
        if (_registerA != 0)
        {
            _instructionPointer = literalOperand;
        }
        else
        {
            _instructionPointer++;
        }
    }

    private void Bxc(int operand)
    {
        _registerB ^= _registerC;
        _instructionPointer += 2;
    }
    
    private void Out(int comboOperand)
    {
        _output.Add(GetComboOperandValue(comboOperand) % 8);
        _instructionPointer += 2;
    }

    private void Bdv(int comboOperand)
    {
        _registerB = (int)(_registerA / Math.Pow(2, GetComboOperandValue(comboOperand)));
        _instructionPointer += 2;
    }

    private void Cdv(int comboOperand)
    {
        _registerC = (int)(_registerA / Math.Pow(2, GetComboOperandValue(comboOperand)));
        _instructionPointer += 2;
    }
}