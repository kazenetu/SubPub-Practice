using System.Collections;
using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

public partial class A : IDisposable
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public A()
    {
        MessageBroker.Subscribe(Keywords.Test, Callback);
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
        MessageBroker.UnSubscribe(Keywords.Test, Callback);
    }

    /// <summary>
    /// 購読コールバック
    /// </summary>
    /// <param name="data">受信情報</param>
    private async Task Callback(object data)
    {
        // 時間のかかる処理
        await Task.Delay(1000);

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

        Console.WriteLine($"CallBack A Async! deta is {result}");
    }
}
