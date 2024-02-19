using static System.Console;

namespace AIContinuous
{
    public static class Root
    {
        public static double Bisection(Func<double, double> function, double a, double b, double atol = 1e-4,
                                        double rtol = 1e-4, int maxIter = 1000)
        {
            double x0;
            double x2;
            double c = 0;

            for (int i = 0; i < maxIter; i++)
            {
                c = (a + b) / 2;
                x0 = function(a);
                x2 = function(c);


                if (x2 * x0 > 0)
                    a = c;
                else
                    b = c;

                if (b - a < rtol * 2)
                {
                    WriteLine("B - A");
                    break;
                }

                var calcTol = function(c);
                if (Math.Abs(calcTol) < atol)
                {
                    WriteLine("ABSOLUTE");
                    break;
                }

                WriteLine("\niteracao " + (i + 1) + "\n" + "a: " + a + "\nb: " + b + "\nc: " + c);
                WriteLine("atol: " + atol + "| calcTol: " + calcTol + "\nrtol: " + (rtol * 2) + "| b - a: " + (b - a));

            }

            return c;
        }

        public static double FalsePosition(Func<double, double> function, double a, double b, double atol = 1e-4,
                                            double rtol = 1e-4, int maxIter = 1000)
        {
            if (a < 0 || b < 0)
            {
                WriteLine("Math.sqrt() can't resolve negative number");
                return 0;
            }

            double x0, y0, y1, x1;
            double x2 = 0;

            for (int i = 0; i < maxIter; i++)
            {
                y0 = function(a);
                y1 = function(b);

                x2 = (-y0) * ((b - a) / (y1 - y0)) + a;

                if (x2 * y0 > 0)
                    a = x2;
                else
                    b = x2;

                if (b - a < rtol * 2)
                {
                    WriteLine("B - A");
                    break;
                }

                var calcTol = function(x2);
                if (Math.Abs(calcTol) < atol)
                {
                    WriteLine("ABSOLUTE");
                    break;
                }
            }

            return x2;
        }
    }
}
 