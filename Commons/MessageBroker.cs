using System.Diagnostics;

namespace SubPub_Practice.Commons;

public static class MessageBroker
{
    /// <summary>
    /// キーワード別アクションリスト
    /// </summary>
    private static Dictionary<string, List<Action<object>>> Subscribers = [];

    /// <summary>
    /// キーワード別クラス名リスト
    /// </summary>
    private static Dictionary<string, List<string>> ClassNames = [];

    /// <summary>
    /// 購買メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="action">発行時に呼ばれるメソッド</param>
    public static void Subscribe(string keyword, Action<object> action)
    {
        // アクションリストにキーワードが存在しない場合は生成
        if (!Subscribers.ContainsKey(keyword))
            Subscribers.Add(keyword, []);

        // 発行時に呼ばれるメソッドを追加
        Subscribers[keyword].Add(action);

        // クラスリストにキーワードが存在しない場合は生成
        if (!ClassNames.ContainsKey(keyword))
            ClassNames.Add(keyword, []);

        // クラス名を追加
        ClassNames[keyword].Add(action.Method?.DeclaringType?.Name ?? string.Empty);
    }

    /// <summary>
    /// 購買解除メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="action">購買時に登録したメソッド</param>
    public static void UnSubscribe(string keyword, Action<object> action)
    {
        Subscribers[keyword].Remove(action);
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
            // 発行元クラスを取得
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var className = method?.DeclaringType?.Name;

            foreach (var action in actions)
            {
                // アクションのクラス名と呼び出し元クラスが一致の場合は処理しない
                var actionClassName = action.Method?.DeclaringType?.Name;
                if (className == actionClassName) continue;

                action(data);
            }
        }
    }
}