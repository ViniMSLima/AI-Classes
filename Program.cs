using AIContinuous;

double MyFunction(double x)
{
    // return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10;
    return (x - 1) * (x - 1) + Math.Sin(x * x * x);
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
// sol = Root.Newton(MyFunction, MyDer, 10);
// sol = Root.Newton(MyFunction, double (double x) => Diff.Differentiate(MyFunction, x), 10);

sol = Optimize.AnyStartNewton(MyFunction, 1);
sol = Optimize.GradientDescent(MyFunction, 1.3);


// Console.WriteLine("\nExecution time: " + (DateTime.Now - data).TotalMilliseconds + " ms");
Console.WriteLine("Resultado: " + sol);
