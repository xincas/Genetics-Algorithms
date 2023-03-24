using GA7;
using System.Diagnostics;

int itercount = 100;
int dimension = 2;
int swarmsize = 100;

double currentVelocityRatio = 0.3;
double localVelocityRatio = 2;
double globalVelocityRatio = 5;

double[] minvalues = new double[dimension];
double[] maxvalues = new double[dimension];

for (int i = 0; i < dimension; i++)
{
    minvalues[i] = -5.12;
    maxvalues[i] = 5.12;
}

RastriginFunction func = new RastriginFunction(minvalues, maxvalues);

Swarm swarm = new Swarm(
    func,
    swarmsize,
    currentVelocityRatio,
    localVelocityRatio,
    globalVelocityRatio);

swarm.SetListener(it =>
{
    Console.WriteLine($"Iteration {it.Iteration}\tBest position: {{{string.Join(", ", it.BestPosition)}}}\t Value: {it.BestFinalFunc}");
});

swarm.SetListener(it =>
{
    if (it.Iteration == 0 || it.Iteration == itercount - 1)
    {
        using StreamWriter sw = new("D:\\repos\\GA7\\points.txt", true);
        foreach (Particle p in it.Particles)
            sw.WriteLine($"{string.Join('\t', p.Position)}\t{swarm.FinalFunction(p.Position)}");
        sw.WriteLine();
    }
});

File.Delete("D:\\repos\\GA7\\points.txt");
Stopwatch stopwatch= Stopwatch.StartNew();
swarm.Evolve(itercount);
stopwatch.Stop();
Console.WriteLine($"Time spent: {stopwatch.ElapsedMilliseconds}ms");

RunCmd("D:\\repos\\GA7\\plot.py", "");

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