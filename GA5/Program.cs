using GA5;
using GA5.GA;
using System.Diagnostics;

var fitness = new Fitness();
var context = new Context(
        populationSize: 100,
        n: 3,
        maxValue: 5.12,
        minValue: -5.12,
        variance: 1.0
    );

Population<Chromosome> CreatePopulation(Fitness fitness, Context context)
{
    var population = new Population<Chromosome>();
    for (int i = 0; i < context.PopulationSize; ++i)
    {
        var xs = new List<double>(context.N);
        for (int j = 0; j < context.N; j++)
            xs.Add(context.GetRandom());

        population.Add(new Chromosome(xs, fitness, context));
    }

    return population;
}

void Update(GAEngine<Chromosome, double> engine)
{
    var best = engine.Best();
    Console.WriteLine($"iter {engine.Iteration + 1}: best ({string.Join(',', best.Xs)}) fitness {best.Fitness}");
    if (best.Fitness == 0) engine.Terminate();
}

void RunCmd(string cmd, string args)
{
    var start = new ProcessStartInfo
    {
        FileName = "C:/Users/xincas/AppData/Local/Microsoft/WindowsApps/PythonSoftwareFoundation.Python.3.10_qbz5n2kfra8p0/python.exe",
        Arguments = $"{cmd} {args}",
        UseShellExecute = false,
        RedirectStandardOutput = true
    };
    using var process = Process.Start(start)!;
}

GAEngine<Chromosome, double> engine = new(CreatePopulation(fitness, context), fitness, Update);
var firstPopulation = engine.Population;

Stopwatch timer = new();
timer.Start();
engine.Evolve(50);
timer.Stop();

Console.WriteLine($"Time spent: {timer.ElapsedMilliseconds}ms");

var lastPopulation = engine.Population;

using (StreamWriter sw = new("D:\\repos\\GA5\\points.txt"))
{
    foreach (Chromosome p in firstPopulation)
        sw.WriteLine($"{string.Join('\t', p.Xs)}\t{p.Fitness}");
    sw.WriteLine();
    foreach (Chromosome p in lastPopulation)
        sw.WriteLine($"{string.Join('\t', p.Xs)}\t{p.Fitness}");
    sw.WriteLine();
}

RunCmd("D:\\repos\\GA5\\plot.py", "");