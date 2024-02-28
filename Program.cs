using AIContinuous;
using AIContinuous.Nuenv;
using  AIContinuous.Rocket;

var timeData = Space.Linear(0.0, 50.0, 11);
var massFlowData = Space.Uniform(70.0, 11);

Rocket rocket = new(
    Math.PI * 0.6 * 0.6/ 4.0,
    massFlowData,
    timeData,
    750.0,
    1916.0,
    0.8
);

double MyFunction(double[] x)
{
    var times= Space.Linear(0.0, 50.0, 11);
    Rocket rocket = new(
    Math.PI * 0.6 * 0.6/ 4.0,
    x,
    timeData,
    750.0,
    1916.0,
    0.8
);
    
    return rocket.LaunchUntilMax();
}

// double MyFunction2(double[] x)
//     => rocket.LaunchUntilMax();

double Restriction(double[] x)
    => -1.0;

// double GD = Optimize.GradientDescent(MyFunction, 1.3);
// Console.WriteLine(GD);


List<double[]> bounds = new() {
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0},
    new double[]{0.0, 3500.0}
};

var diffEvolution = new DiffEvolution(MyFunction, bounds, 20, Restriction);
var res = diffEvolution.Optimize(1000);


foreach (var r in res)
{
    Console.WriteLine(r);
}