using System.Collections;
using SubPub_Practice.Commons;
using SubPub_Practice.SampleClass.Records;

namespace SubPub_Practice.SampleClass;

/// <summary>
/// データ取得クラス
/// </summary>
public class DataGever : IDisposable
{
    #region プロパティ

    /// <summary>
    /// データ提供クラスAの提供情報
    /// </summary>
    public string? ResultA { private set; get; }

    /// <summary>
    /// データ提供クラスBの提供情報
    /// </summary>
    public string? ResultB { private set; get; }

    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DataGever()
    {
        // リクエストの購読
        MessageBroker.Subscribe(Keywords.ResponseA, CallbackResponseA);
        MessageBroker.Subscribe(Keywords.ResponseB, CallbackResponseB);
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
        // リクエストの購読解除
        MessageBroker.UnSubscribe(Keywords.ResponseA, CallbackResponseA);
        MessageBroker.UnSubscribe(Keywords.ResponseB, CallbackResponseB);
    }

    #region メソッド

    /// <summary>
    /// 発行(非同期化)
    /// </summary>
    public async Task PublishAsync()
    {
        var thisClass = GetType();

        // 提供クラスA取得リクエスト発行
        var dataRequestA = new RequestRecord(RequestRecord.Targets.DataTakerA);
        await MessageBroker.PublishAsync(Keywords.Request, dataRequestA, thisClass);

        // 提供クラスB取得リクエスト発行
        var dataRequestB = new RequestRecord(RequestRecord.Targets.DataTakerB);
        await MessageBroker.PublishAsync(Keywords.Request, dataRequestB, thisClass);

        // 結果を出力
        Console.WriteLine($"   >> ResultA:{ResultA}  ResultB:{ResultB}");
    }

    #endregion

    #region イベント

    /// <summary>
    /// MessageBroker購読コールバックA
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void CallbackResponseA(object data)
    {
        // 対象外は即時リターン
        if (data is not ResponseARecord response) return;

        // プロパティに格納
        var properties = $"DataStrings = [{string.Join(", ", response.DataStrings)}], DataString = {response.DataString}";
        ResultA = "ResultA:ResponseARecord { " + properties + " }";
    }

    /// <summary>
    /// MessageBroker購読コールバックB
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void CallbackResponseB(object data)
    {
        // 対象外は即時リターン
        if (data is not ResponseBRecord response) return;

        // プロパティに格納
        ResultB = $"{response}";
    }

    #endregion
}
