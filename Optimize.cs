namespace AIContinuous;

public static class Optimize
{
    public static double Newton(Func<double, double> function, double x0, double h = 1e-2, double atol = 1e-4, int maxIter = 10000)
    {
        Func<double, double> diffFunction = double (double x) => Diff.Differentiate(function, x, h);
        Func<double, double> diffSecondFunction = double (double x) => Diff.Differentiate(diffFunction, x, h);
        
        return Root.Newton(diffFunction, diffSecondFunction, x0, atol, maxIter);
    }

    public static double AnyStartNewton(Func<double, double> function, double start)
    {
        var min = Newton(function, start);

        // while(true)
        // {
        //     start += 1e-2;
        //     var newPoint = Newton(function, start);
        //     Console.WriteLine(function(newPoint));
        //     if(function(newPoint) < function(min))
        //         min = newPoint;
        //     else
        //         break;

        // }

        // Console.WriteLine(function(min));

        return min;

    }

    public static double GradientDescent(Func<double, double> function, double x0, double learningRate = 1e-2, double atol = 1e-4)
    {
        double xp = x0;
        double diff = Diff.Differentiate(function, xp);

        while(Math.Abs(diff) > atol)
        {
            diff = Diff.Differentiate(function, xp);
            xp -= diff * learningRate;
        }

        return xp;
    }
}
