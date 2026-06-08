using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

public class A
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public A()
    {
        MessageBroker.Subscribe("test", Callback);
    }

    /// <summary>
    /// MessageBroker発行時のコールバック
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void Callback(object data)
    {
        Console.WriteLine($"CallBack A! deta is {data}");
    }
}
