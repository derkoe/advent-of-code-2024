using Microsoft.VisualBasic;

public class Day17
{
    // Combo operands 0 through 3 represent literal values 0 through 3.
    // Combo operand 4 represents the value of register A.
    // Combo operand 5 represents the value of register B.
    // Combo operand 6 represents the value of register C.
    // Combo operand 7 is reserved and will not appear in valid programs.
    enum OpCode
    {
        Adv = 0, // Division. The numerator is the value in the A register. The denominator is found by raising 2 to the power of the instruction's combo operand.  The result of the division operation is truncated to an integer and then written to the A register.
        Bxl = 1,// The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.
        Bst = 2, // The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.
        Jnz = 3, // The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
        Bxc = 4, // The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
        Out = 5, // The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value. (If a program outputs multiple values, they are separated by commas.)
        Bdv = 6, // The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register. (The numerator is still read from the A register.)
        Cdv = 7 // The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register. (The numerator is still read from the A register.)
    }

    struct Computer
    {
        public long a;
        public long b;
        public long c;

        public List<(OpCode, int)> operations;

        public int operationPointer;
    }

    public static void Run()
    {
        var input = File.ReadAllLines("day17/input.txt");
        var programStr = input[4].Replace("Program: ", "");
        var program = programStr.Split(",").Select(int.Parse).ToList();
        var operations = new List<(OpCode, int)>();
        for (int i = 0; i < program.Count; i += 2)
        {
            operations.Add(((OpCode)program[i], program[i + 1]));
        }
        string output = Compute(new Computer
        {
            a = int.Parse(input[0].Split(" ")[2]),
            b = int.Parse(input[1].Split(" ")[2]),
            c = int.Parse(input[2].Split(" ")[2]),
            operations = operations,
            operationPointer = 0,
        });
        Console.WriteLine("Part 1: " + output);

        // found this start number by multiplying the number of outputs by 8
        for (long a = 107416870455296; a < 1107416870455296; a++)
        {
            output = Compute(new Computer
            {
                a = a,
                b = int.Parse(input[1].Split(" ")[2]),
                c = int.Parse(input[2].Split(" ")[2]),
                operations = operations,
                operationPointer = 0,
            });
            if (output.Equals(programStr))
            {
                Console.WriteLine("Part 2: " + a + " - " + output);
                break;
            }
            if (programStr.EndsWith(output))
            {
                Console.WriteLine(a + " - " + output);
            }
        }
    }

    private static string Compute(Computer computer)
    {
        var output = "";
        while (computer.operationPointer < computer.operations.Count)
        {
            var (opCode, literalOperand) = computer.operations[computer.operationPointer];
            long comboOperand = literalOperand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => computer.a,
                5 => computer.b,
                6 => computer.c,
                _ => throw new Exception("Invalid operand")
            };
            switch (opCode)
            {
                case OpCode.Adv:
                    computer.a = computer.a / (int)Math.Pow(2, comboOperand);
                    break;
                case OpCode.Bxl:
                    computer.b ^= literalOperand;
                    break;
                case OpCode.Bst:
                    computer.b = comboOperand % 8;
                    break;
                case OpCode.Jnz:
                    if (computer.a != 0)
                    {
                        computer.operationPointer = literalOperand;
                        continue;
                    }
                    break;
                case OpCode.Bxc:
                    computer.b ^= computer.c;
                    break;
                case OpCode.Out:
                    if (output.Length > 0) output += ",";
                    output += comboOperand % 8;
                    break;
                case OpCode.Bdv:
                    computer.b = computer.a / (long)Math.Pow(2L, comboOperand);
                    break;
                case OpCode.Cdv:
                    computer.c = computer.a / (long)Math.Pow(2L, comboOperand);
                    break;
            }
            computer.operationPointer++;
        }

        return output;
    }
}