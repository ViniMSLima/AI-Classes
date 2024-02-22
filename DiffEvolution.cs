namespace AIContinuous;

public class DiffEvolution
{
    protected List<double[]> Individuals { get; set; }
    protected Func<double[], double> Fitness { get; }
    protected List<double[]> Bounds { get; }
    protected double BestIndividualFitness { get; set; } = double.MaxValue;
    protected int BestIndividualIndex { get; set; }
    protected int Dimension { get; }
    protected int NPop { get; }

    public DiffEvolution(Func<double[], double> fitness, List<double[]> bounds, int npop)
    {
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.Bounds = bounds;
        this.NPop = npop;

        Individuals = new(this.NPop);

    }

    private void GeneratePopulation()
    {
        int dimension = Dimension;
        for (int i = 0; i < this.NPop; i++)
        {
            Individuals[i] = new double[dimension];
            for (int j = 0; j < dimension; j++)
            {
                Individuals[i][j] = Utils.Rescale(
                    Random.Shared.NextDouble(),
                    Bounds[j][0],
                    Bounds[j][1]
                );
            }
        }
    }

    private void FindBestIndividual()
    {
        var fitnessBest = BestIndividualFitness;
        for (int i = 0; i < Individuals.Count; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        BestIndividualFitness = fitnessBest;
    }

    public double[] Optimize()
    {
        GeneratePopulation();
        FindBestIndividual();

        return Individuals[BestIndividualIndex];
    }

    private double[] Mutate(double[] individual)
    {
        var newIndividual = new double[Dimension];

        for(int i = 0; i < Dimension; i++)
        {
            newIndividual[i] += Individuals[Random.Shared.Next(NPop)][i] - Individuals[Random.Shared.Next(NPop)][i] ;
        }

        return newIndividual;
    }
}
