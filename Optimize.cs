namespace AIContinuous;

public static class Optimize
{
    public static double Newton(Func<double, double> function, double x0, double h = 1e-2, double atol = 1e-4, int maxIter = 10000)
    {
        Func<double, double> diffFunction = double (double x) => Diff.Differentiate(function, x, 2.0 * h);
        Func<double, double> diffSecondFunction = double (double x) => Diff.Differentiate(diffFunction, x, h);
        
        return Root.Newton(diffFunction, diffSecondFunction, x0, atol, maxIter);
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

    public static double[] GradientDescent(Func<double[], double> function, double[] x0, double learningRate = 1e-5, double atol = 1e-9)
    {
        var xp = (double[]) x0.Clone();
        var diff = Diff.Gradient(function, xp);
        var dim = x0.Length;
        double diffNorm;

        do
        {
            diffNorm = 0.0;
            diff = Diff.Gradient(function, xp);

            for(int i = 0; i < dim; i++)
            {
                xp[i] -= diff[i] * learningRate;
                diffNorm += Math.Abs(diff[i]);
            }
        } while(diffNorm > atol * dim);

        return xp;
    }
}
