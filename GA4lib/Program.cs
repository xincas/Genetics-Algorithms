using System.Diagnostics;
using System.Globalization;
using System.Text;
using GA4lib.GA;
using GA4lib.GP;

double? Function6(List<double> xs)
{
    if (xs == null || xs.Count != 10) return null;
    double res = 0;

    foreach (var x in xs) res += -x * Math.Sin(Math.Sqrt(Math.Abs(x)));

    return res;
}

List<Target> CreateTargets()
{
    List<Target> targets = new List<Target>();
    int max = 500;
    int min = -500;
    Dictionary<string, double> xs = new();
    foreach (int i in Enumerable.Range(0, 10)) xs.Add($"x{i}", 0d);
    var Keys = xs.Keys;
    for (int i = 0; i < 1; i++)
    {
        foreach (var key in Keys) xs[key] = Random.Shared.Next(min, max);
        double target = (double)Function6(xs.Select(it => it.Value).ToList())!;
        var t = new Target(xs, target);
        targets.Add(t);
    }

    return targets;
}

var targets = CreateTargets();

//foreach (var target in targets)
//{
//    StringBuilder sb = new StringBuilder();
//    foreach (var keyVal in target.contextState)
//        sb.Append($".when(\"{keyVal.Key}\", {keyVal.Value})");
//    Console.WriteLine($"new Target(){sb}.targetIs({target.TargetValue}),");
//}

//StringBuilder sb2 = new StringBuilder();
//foreach (var target in targets)
//{
//    StringBuilder sb = new StringBuilder();
//    foreach (var keyVal in target.contextState)
//        sb.Append($"{{ \"{keyVal.Key}\", {keyVal.Value} }},");
//    sb2.Append($"new Target(new Dictionary<string, double>(){{ {sb} }}, {target.TargetValue.ToString(CultureInfo.InvariantCulture)}),");
//}
//Console.WriteLine($"var lst = new List<Target>(){{{sb2}}};");

Console.Read();

TabulatedFunctionFitness fitness = new TabulatedFunctionFitness(targets);

GPEngine engine =
    new GPEngine(
        fitness,
        new() { "x0", "x1", "x2", "x3", "x4", "x5", "x6", "x7", "x8", "x9" });

void Update(GAEngine<GpChromosome, double> engine)
{
    
    GpChromosome bestChromosome = engine.Best();
    double currentFitnessValue = engine.Fitness(bestChromosome);
    Console.WriteLine($"iter = {engine.Iteration + 1}\tbest: fit = {currentFitnessValue}\tfunc = {bestChromosome.SyntaxTree.ToString(bestChromosome.Context)}");
    var avg = engine.Population.Chromosomes.Sum(it => engine.Fitness(it)) / engine.Population.Count;
    Console.WriteLine($"\theight of best = {SyntaxTreeUtils.GetHeight(bestChromosome.SyntaxTree)}");
    Console.WriteLine($"\tavg fitness = {avg}\n\n");
    

    /*foreach (GpChromosome chromosome in engine.Population)
        Console.WriteLine(
            $"\tfit = {engine.Fitness(chromosome)}\tfunc = {chromosome.SyntaxTree.ToString(chromosome.Context)}");*/

    if (currentFitnessValue.CompareTo(1.0) < 0) engine.Terminate();
}

engine.AddListener(Update);

var stopWatch = new Stopwatch();

stopWatch.Start();
engine.Evolve(100);
stopWatch.Stop();

GpChromosome bestChromosome = engine.Best();
double currentFitnessValue = engine.Fitness(bestChromosome.SyntaxTree);
Console.WriteLine($"iter = {engine.Iteration}\tbest: fit = {currentFitnessValue}\tfunc = {bestChromosome.SyntaxTree.ToString(bestChromosome.Context)}");
Console.WriteLine($"time: {stopWatch.ElapsedMilliseconds}ms");
