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

        // 情報取得
        Console.WriteLine();
        Console.WriteLine($"> DataGever");

        // 情報提供クラス生成
        var dataTakerA = new DataTakerA();
        var dataTakerB = new DataTakerB();

        //情報取得クラス生成、メソッド呼び出し
        var dataGever = new DataGever();
        await dataGever.PublishAsync();
    }
}
