
using HanConv;

if (args.Length < 1)
{
    Console.WriteLine($"[Usage] {System.IO.Path.GetFileNameWithoutExtension(Environment.ProcessPath)} \"...text...\"");
    return;
}

Console.WriteLine(args[0].HanjaToHangulDueum());