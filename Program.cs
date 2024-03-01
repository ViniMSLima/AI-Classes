
// using AIContinuous;
// using AIContinuous.Nuenv;
// using AIContinuous.Rocket;
// var size = 11;
// var timeData = Space.Geometric(1.0, 101.0, size).Select(x => x - 1.0).ToArray();

// double Simulate(double[] massFlowData)
// {
//     Rocket rocket = new
//     (
//         Math.PI * 0.6 * 0.6 / 4.0,
//         massFlowData,
//         timeData,
//         750.0,
//         1916.0,
//         0.8
//     );

//     return rocket.LaunchUntilMax();
// }

// double FitnessDE(double[] x) => - 1.0 * Simulate(x);

// double Restriction(double[] y)
//     => Integrate.Romberg(timeData, y) - 3500.0;

// List<double[]> bounds = new();

// for (int i = 0; i < size; i++)
//     bounds.Add(new double[] { 0.0, 1000.0 });

// var diffEvolution = new DiffEvolution(FitnessDE, bounds, 100 * size, Restriction);
// var res = diffEvolution.Optimize(100);

// foreach (var r in res)
//     Console.WriteLine(r);

// Console.WriteLine("Max height: " + Simulate(res));

using AIClasses.Collections;



Tree<int> BuildTree()
{
    // Tree 1 (root: 50)
    var node = new TreeNode<int>(6);
    node = new TreeNode<int>(21, children: new List<TreeNode<int>> { node });
    var node2 = new TreeNode<int>(45);
    node = new TreeNode<int>(12, children: new List<TreeNode<int>> { node, node2 });
    node = new TreeNode<int>(50, children: new List<TreeNode<int>> { node });
    var tree1 = new Tree<int>(node);

    // Tree 2 (root: 1)
    var root = new TreeNode<int>(1)
               .AddChild(new TreeNode<int>(70))
               .AddChild(new TreeNode<int>(61));

    var tree2 = new Tree<int>(root);

    // Tree 3 (root: 30)
    root = new TreeNode<int>(30)
               .AddChild(new TreeNode<int>(96))
               .AddChild(new TreeNode<int>(9));

    var tree3 = new Tree<int>(root);

    // Tree4 (root: 150)
    root = new TreeNode<int>(150)
        .AddChild(tree3.Root)
        .AddChild(new TreeNode<int>(5))
        .AddChild(new TreeNode<int>(11));

    var tree4 = new Tree<int>(root);

    // Tree 5 (root: 100)
    root = new TreeNode<int>(100)
           .AddChild(tree1.Root)
           .AddChild(tree2.Root)
           .AddChild(tree4.Root);

    var tree5 = new Tree<int>(root);

    Console.WriteLine(tree5);

    return tree5;
}

BuildTree();