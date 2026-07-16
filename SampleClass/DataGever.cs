using System.Collections;

namespace SubPub_Practice.SampleClass;

/// <summary>
/// データ取得クラス
/// </summary>
public class DataGever
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
