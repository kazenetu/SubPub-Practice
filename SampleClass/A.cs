using System.Collections;
using System.Text;
using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

public class A : IDisposable
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public A()
    {
        MessageBroker.Subscribe("test", Callback);
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
        MessageBroker.UnSubscribe("test", Callback);
    }

    /// <summary>
    /// 発行
    /// </summary>
    public void Publish()
    {
        MessageBroker.Publish("test", 1m);
    }

    /// <summary>
    /// MessageBroker発行時のコールバック
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void Callback(object data)
    {
        var result = data;
        if (data is IList list)
        {
            var resultList = new List<string>();
            foreach (var item in list)
            {
                resultList.Add($"{item}");
            }
            result = string.Join(",", resultList);
        }

        Console.WriteLine($"CallBack A! deta is {result}");
    }
}
