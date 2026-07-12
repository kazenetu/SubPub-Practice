using System.Collections;
using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

/// <summary>
/// データ提供クラスB
/// </summary>
public class DataTakerB
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DataTakerB()
    {
        // リクエストの購読
        MessageBroker.Subscribe(Keywords.RequestB, Callback);
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
        // リクエストの購読解除
        MessageBroker.UnSubscribe(Keywords.RequestB, Callback);
    }

    /// <summary>
    /// MessageBroker購読コールバック
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

        // 発行
        var sendData = "DataTakerB";
        MessageBroker.Publish(Keywords.ResponseB, sendData);
    }
}
