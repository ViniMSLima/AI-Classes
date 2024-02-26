using AIContinuous;

double MyFunction(double x)
{
    // return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10;
    return (x - 1) * (x - 1) + Math.Sin(x * x * x);
}


double RosenBrock(double[] x)
{
    // return x[0] * x[0] + x[1] * x[1];
    // return (x[0] + 2 * x[1] - 7) * (x[0] + 2 * x[1] - 7) + (2 * x[0] + x[1] - 5) * (2 * x[0] + x[1] - 5);

    double z = 0.0;
    int n = x.Length - 1;

    for(int i = 0; i < n; i++)
    {
        var xi = x[i];
        var xi1 = x[i + 1];
        z += 100 * (xi1 - xi * xi) * (xi1 - xi * xi) + (1 - xi) * (1 - xi);  
    }

    return z;
}

double Restriction(double[] x)
{
    return -1.0;
}

// double sol;
// double[] sol2;
var data = DateTime.Now;
data = DateTime.Now;

// sol = Root.Bisection(MyFunction, -10, 10);
// sol = Root.FalsePosition(MyFunction, -10, 10);
// sol = Root.Newton(MyFunction, MyDer, 10);
// sol = Root.Newton(MyFunction, double (double x) => Diff.Differentiate(MyFunction, x), 10);

// sol = Optimize.AnyStartNewton(MyFunction, 1);
// sol = Optimize.GradientDescent(MyFunction, 1.3);

double[] a = {0, 0};
// sol2 = Optimize.GradientDescent(MyFunction2, a);

List<double[]> bounds = new() {
    new double[]{-10.0, 10.0},
    new double[] {-10.0, 10.0}
};


var diffEvolution = new DiffEvolution(RosenBrock, bounds, 200, Restriction);
var res = diffEvolution.Optimize(1000);

Console.WriteLine("\nExecution time: " + (DateTime.Now - data).TotalMilliseconds + " ms");
// Console.WriteLine("Result => x: " + sol2[0] + " | y: " + sol2[1]);
Console.WriteLine("Result => x: " + res[0] + " | y: " + res[1]);
