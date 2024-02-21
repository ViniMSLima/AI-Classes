using AIContinuous;

double MyFunction(double x)
{
    return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10;
}

double MyDer(double x)
{
    return (x / (2* Math.Sqrt(Math.Abs(x)))) + (Math.Sqrt(Math.Abs(x)) - 5);
}

double sol;
var data = DateTime.Now;
data = DateTime.Now;

// sol = Root.Bisection(MyFunction, -10, 10);
// sol = Root.FalsePosition(MyFunction, -10, 10);
sol = Root.Newton(MyFunction, MyDer, 10);


Console.WriteLine("\nExecution time: " + (DateTime.Now - data).TotalMilliseconds + " ms");
Console.WriteLine("Resultado: " + sol);
