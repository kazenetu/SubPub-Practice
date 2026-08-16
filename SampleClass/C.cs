using System.Collections;
using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

public class C : IDisposable
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public C()
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
    /// 発行
    /// </summary>
    public void Publish()
    {
        var thisClass = GetType();
        MessageBroker.Publish(Keywords.Test, "パラメータ", thisClass);
    }

    /// <summary>
    /// 購読コールバック
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

        Console.WriteLine($"CallBack C! deta is {result}");
    }
}
