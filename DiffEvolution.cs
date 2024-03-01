using System.Security.Cryptography.X509Certificates;
using AIContinuous.Nuenv;

namespace AIContinuous;

public class DiffEvolution
{
    protected   List<double[]> Individuals { get; set; }
    protected   Func<double[], double> Fitness { get; }
    protected   Func<double[], double> Restriction { get; }
    protected   List<double[]> Bounds { get; }
    protected   int       BestIndividualIndex { get; set; }
    protected   double    MutationMin { get; set; }
    protected   double    MutationMax { get; set; }
    protected   double  Recombination { get; set; }
    protected   int     Dimension { get; }
    protected   int     NPop { get; }
    private     double[] IndividualsRestrictions { get; set; }
    private     double[] IndividualsFitness { get; set; }

    public DiffEvolution(
        Func<double[], double> fitness,
        List<double[]> bounds,
        int npop,
        Func<double[], double> restriction,
        double mutationMin = 0.9,
        double mutationMax = 0.6,
        double recombination = 0.8)
    {
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.Bounds = bounds;
        this.NPop = npop;
        this.Restriction = restriction;
        this.MutationMin = mutationMin;
        this.MutationMax = mutationMax;
        this.Recombination = recombination;
        this.IndividualsRestrictions = new double[NPop];
        this.IndividualsFitness = new double[NPop];

        Individuals = new(this.NPop);
    }

    private void GeneratePopulation()
    {
        int dimension = Dimension;
        for (int i = 0; i < this.NPop; i++)
        {
            Individuals.Add(new double[dimension]);
            for (int j = 0; j < dimension; j++)
            {
                Individuals[i][j] = Utils.Rescale(
                    Random.Shared.NextDouble(),
                    Bounds[j][0],
                    Bounds[j][1]
                );
            }

            IndividualsRestrictions[i] = Restriction(Individuals[i]);

            IndividualsFitness[i] = IndividualsRestrictions[i] <= 0.0 ? 
                                    Fitness(Individuals[i]) :
                                    double.MaxValue;
        }
    }

    private void FindBestIndividual()
    {
        var fitnessBest = IndividualsFitness[BestIndividualIndex];
        for (int i = 0; i < NPop; i++)
        {
            if (IndividualsFitness[i] < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = IndividualsFitness[i];
            }
        }
        IndividualsFitness[BestIndividualIndex] = fitnessBest;
    }

    private void EnsureBounds(double[] individual)
    {
        for(int i = 0; i < Dimension; i++)
        {
            if(individual[i] < Bounds[i][0] || individual[i] > Bounds[i][1])
                individual[i] = Utils.Rescale(
                    Random.Shared.NextDouble(),
                    Bounds[i][0], 
                    Bounds[i][1]
                );
        }
    }

    public double[] Optimize(int n)
    {
        GeneratePopulation();
        FindBestIndividual();

        for (int i = 0; i < n; i++)
        {
            Iterate();
            Console.WriteLine("Generation: " + (i+1));
        }

        return Individuals[BestIndividualIndex];
    }

    private double[] Mutate(int index)
    {
        int individualRand1, individualRand2;

        do individualRand1 = Random.Shared.Next(NPop);
        while (index == individualRand1);

        do individualRand2 = Random.Shared.Next(NPop);
        while (individualRand1 == individualRand2);

        var newIndividual = (double[])Individuals[BestIndividualIndex].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            newIndividual[i] += Utils.Rescale(Random.Shared.NextDouble(), MutationMin, MutationMax) * 
                                (Individuals[individualRand1][i] - Individuals[individualRand2][i]);
        }

        return newIndividual;
    }

    protected double[] Crossover(int index)
    {
        var trial = Mutate(index);
        var trial2 = (double[])Individuals[index].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            if (!(Random.Shared.Next() < Recombination || i == Random.Shared.Next(Dimension)))
                trial2[i] = trial[i];
        }

        EnsureBounds(trial2);

        return trial2;
    }

    protected void Iterate()
    {
        for (int i = 0; i < NPop; i++)
        {
            var trial = Crossover(i);
            var resTrial = Restriction(trial);
            var resIndividual = IndividualsRestrictions[i];
            double fitnessTrial = resTrial <= 0.0 ? Fitness(trial) : double.MaxValue;

            if 
            (
                (resIndividual > 0.0 && resTrial < resIndividual) ||
                (resTrial <= 0.0 && resIndividual > 0.0) || 
                (resTrial <= 0.0 && resIndividual <= 0.0 && fitnessTrial < IndividualsFitness[i])
            )
            {
                Individuals[i] = trial;
                IndividualsRestrictions[i] = resTrial;
                IndividualsFitness[i] = fitnessTrial;
            }
        }

        FindBestIndividual();
    }
}
