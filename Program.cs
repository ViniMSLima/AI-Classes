using AIContinuous;

double MyFunction(double x)
{
    return Math.Sqrt(x) - Math.Cos(x);
}

var data = DateTime.Now;
var sol = Root.FalsePosition(MyFunction, 0, 1);

Console.WriteLine("\nExecution time: " + (DateTime.Now - data).TotalMilliseconds + " ms");
Console.WriteLine("Resultado: " + sol);
