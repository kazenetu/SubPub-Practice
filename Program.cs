using SubPub_Practice.SampleClass;

namespace SubPub_Practice;

class Program
{
    static void Main(string[] args)
    {
        using var a = new A();
        using var b = new B();
        using var c = new C();
    }
}
