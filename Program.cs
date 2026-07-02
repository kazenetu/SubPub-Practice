using SubPub_Practice.SampleClass;

namespace SubPub_Practice;

class Program
{
    static async Task Main(string[] args)
    {
        using var a = new A();
        using var b = new B();
        using var c = new C();

        // 発行
        Console.WriteLine($"> Publish A");
        a.Publish();

        Console.WriteLine();
        Console.WriteLine($"> Publish ClassType A");
        a.PublishAsync();

        Console.WriteLine();
        Console.WriteLine($"> Publish B");
        b.Publish();

        Console.WriteLine();
        Console.WriteLine($"> PublishAsync B");
        await b.PublishAsync();

        Console.WriteLine();
        Console.WriteLine($"> Publish C");
        c.Publish();
    }
}
