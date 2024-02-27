namespace AIContinuous.Rocket;

public static class Rocket
{
    public static double Height(double v, double dt) 
        => v * dt;

    public static double Velocity(double a, double dt) 
        => a * dt;

    public static double Acceleration(double T, double D, double W, double m)
        => (T + D + W) / m;

    public static double Drag()
    {

        return 0.0;
    }

} 