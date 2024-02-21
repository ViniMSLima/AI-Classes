using static System.Console;

namespace AIContinuous
{
    public static class Root
    {
        public static double Bisection(Func<double, double> function, double a, double b, double atol = 1e-4,
                                        double rtol = 1e-6, int maxIter = 1000)
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
            double y0, y1;
            double c = 0;

            for (int i = 0; i < maxIter; i++)
            {
                y0 = function(a);
                y1 = function(b);

                c = (-y0) * ((b - a) / (y1 - y0)) + a;
                var fc = function(c);

                if (fc * y0 < 0.0)
                    b = c;
                else
                    a = c;
                
                var calcTol = fc;
                if (Math.Abs(calcTol) < atol)
                {
                    WriteLine("ABSOLUTE");
                    break;
                }

                if (b - a < 2.0 * rtol)
                {
                    WriteLine("B - A");
                    break;
                }
            }

            return c;
        }

        public static double Newton(Func<double, double> function, Func<double, double> der, double x0, 
                                        double atol = 1e-6, int maxIter = 10000)
        {
            double xp = x0;

            for (int i = 0; i < maxIter; i++)
            {
                var fp = function(xp);

                xp -= fp / der(xp);

                if(Math.Abs(fp) < atol)
                    break;
            }

            return xp;
        }
    }
}
