using System.Collections;
using SubPub_Practice.Commons;
using SubPub_Practice.SampleClass.Records;

namespace SubPub_Practice.SampleClass;

/// <summary>
/// データ提供クラスA
/// </summary>
public class DataTakerA : IDisposable
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DataTakerA()
    {
        // リクエストの購読
        MessageBroker.Subscribe(Keywords.Request, Callback);
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
        // リクエストの購読解除
        MessageBroker.UnSubscribe(Keywords.Request, Callback);
    }

    /// <summary>
    /// 購読コールバック：ResponseARecord発行
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void Callback(object data)
    {
        // RequestRecordでTargetsが自身ではない場合は終了
        if (data is RequestRecord req && req.Target != RequestRecord.Targets.DataTakerA) return;

        // 発行
        var responseData = new ResponseARecord(["s1", "s2"], "ResponseARecord");
        MessageBroker.Publish(Keywords.ResponseA, responseData);
    }
}