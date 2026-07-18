using System.Collections;
using SubPub_Practice.Commons;

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
        await MessageBroker.PublishAsync(Keywords.RequestA, string.Empty, thisClass);

        // 提供クラスB取得リクエスト発行
        await MessageBroker.PublishAsync(Keywords.RequestB, string.Empty, thisClass);
    }

    #endregion

    #region イベント

    /// <summary>
    /// MessageBroker購読コールバックA
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void CallbackResponseA(object data)
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

        // プロパティに格納
        ResultA = result as string;
    }

    /// <summary>
    /// MessageBroker購読コールバックB
    /// </summary>
    /// <param name="data">発行時に送信された情報</param>
    private void CallbackResponseB(object data)
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

        // プロパティに格納
        ResultB = result as string;
    }

    #endregion
}
