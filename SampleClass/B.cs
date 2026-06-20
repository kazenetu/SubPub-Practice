using System.Collections;
using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

public class B : IDisposable
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public B()
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
        var list = new List<string>()
        {
            "パラメータ1",
            "パラメータ2",
        };

        MessageBroker.Publish("test", list);
    }

    /// <summary>
    /// 発行(非同期化)
    /// </summary>
    public async Task PublishAsync()
    {
        await MessageBroker.PublishAsync("test", "PublishAsync!");
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

        Console.WriteLine($"CallBack B! deta is {result}");
    }
}
