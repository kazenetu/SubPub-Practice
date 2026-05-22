namespace SubPub_Practice.Commons;

public static class MessageBroker
{
    /// <summary>
    /// キーワード別アクションリスト
    /// </summary>
    private static Dictionary<string, List<Action<object>>> Subscribers = [];

    /// <summary>
    /// 購買メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="action">発行時に呼ばれるメソッド</param>
    public static void Subscribe(string keyword, Action<object> action)
    {
        // キーワードが存在しない場合は生成
        if (!Subscribers.ContainsKey(keyword))
            Subscribers.Add(keyword, []);

        // 発行時に呼ばれるメソッドを追加
        Subscribers[keyword].Add(action);
    }
}