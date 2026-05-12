using System;

namespace Calculator.Models;

public class Calculator
{
    public double Calculate(double lastValue, double current, string operation)
    {
        switch (operation)
        {
            case "+":
                return lastValue + current;
            case "-":
                return lastValue - current;
            case "*":
                return lastValue * current;
            case "/":
                return lastValue / current;
            case "^":
                return Math.Pow(lastValue, current);
            case "%":
                return lastValue * current / 100.0;
            default:
                return current;
        }
    }

    public double ApplyUnary(double value, string operation)
    {
        switch (operation)
        {
            case "sqrt":
                return Math.Sqrt(value);
            case "log":
                return Math.Log10(value);
            case "sin":
                return Math.Sin(value);
            case "cos":
                return Math.Cos(value);
            case "tan":
                return Math.Tan(value);
            default:
                return value;
        }
    }
}