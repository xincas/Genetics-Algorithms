using GA2;
using System.Diagnostics;

double f(params double[] xs)
{
    double sum = 0;
    foreach(var x in xs)
        sum += Math.Pow(x, 2) - 10 * Math.Cos(2 * Math.PI * x);
    return 10d * xs.Length + sum;
}

/*var P_C = new double[] { 0.05d, 0.2d, 0.3d, 0.4d, 0.5d, 0.6d, 0.7d, 0.8d, 0.9d, 1 };
var P_M = new double[] { 0.05d, 0.2d, 0.3d, 0.4d, 0.5d, 0.6d, 0.7d, 0.8d, 0.9d, 1 };
var N = new int[] { 10, 25, 50, 75, 100, 125, 150, 175, 200, 500 };

foreach(var x in N)
{
    GASolver solver = new(
        fitnessFunction: new(f),
        vARIABLE_SIZE: 2,
        range: new(-5.12d, 5.12d),
        pOPULATION_SIZE: 100,
        mAX_GEN: 100,
        cROSS_RATE: 0.5,
        mUTATION_RATE: 0.05,
        isMax: false
    );

    Stopwatch timer = new();
    timer.Start();

    var info = solver.Solve();

    timer.Stop();

    Console.WriteLine($"N: {x}\ttime: {timer.ElapsedMilliseconds}ms\tval: {0-info.Last().Points.Min(it => it.Y)}\tgen: {info.Count}");
}*/

GASolver solver = new(
        fitnessFunction: f,
        vARIABLE_SIZE: 3,
        range: new DoubleRange(-5.12d, 5.12d),
        pOPULATION_SIZE: 100,
        mAX_GEN: 100,
        cROSS_RATE: 0.5,
        mUTATION_RATE: 0.05,
        isMax: false
    );

Stopwatch timer = new();
timer.Start();

var info = solver.Solve();

timer.Stop();

using (StreamWriter sw = new("D:\\repos\\GA2\\points.txt"))
{    
    foreach (var p in info.First().Points)
        sw.WriteLine(p);
    sw.WriteLine();
    foreach (var p in info.Last().Points)
        sw.WriteLine(p);
    sw.WriteLine();
}

Console.WriteLine($"Time spent: {timer.ElapsedMilliseconds}ms");
Console.WriteLine($"Generation count: {info.Count}");
Console.WriteLine($"Result: {info.Last().Points.Min(it => it.Y)}");
foreach(var i in info)
{
    Console.WriteLine($"Generation {i.Index}:");
    Console.WriteLine($"\tBest point f(x):{i.Points.Min(it => it.Y)}");
}

RunCmd("D:\\repos\\GA2\\plot.py", "");

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