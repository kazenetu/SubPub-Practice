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

    /// <summary>
    /// 発行メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="data">値</param>
    public static void Publish(string keyword, object data)
    {
        if (Subscribers.TryGetValue(keyword, out var actions))
        {
            foreach (var action in actions)
            {
                action(data);
            }
        }
    }
}