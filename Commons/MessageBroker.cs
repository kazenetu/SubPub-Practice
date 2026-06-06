using System.Diagnostics;

namespace SubPub_Practice.Commons;

public static class MessageBroker
{
    #region フィールド

    /// <summary>
    /// キーワード別アクションリスト
    /// </summary>
    private static Dictionary<string, List<Action<object>>> Subscribers = [];

    /// <summary>
    /// キーワード別asyncリスト
    /// </summary>
    private static Dictionary<string, List<Func<object, Task>>> SubscribeAsyncs = [];

    /// <summary>
    /// キーワード別クラス名リスト
    /// </summary>
    private static Dictionary<string, List<string>> ClassNames = [];

    #endregion

    #region 購買メソッド

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
    /// 購買メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="action">発行時に呼ばれる非同期メソッド</param>
    public static void Subscribe(string keyword, Func<object, Task> action)
    {
        // asyncリストにキーワードが存在しない場合は生成
        if (!SubscribeAsyncs.ContainsKey(keyword))
            SubscribeAsyncs.Add(keyword, []);

        // 発行時に呼ばれるメソッドを追加
        SubscribeAsyncs[keyword].Add(action);

        // クラスリストにキーワードが存在しない場合は生成
        if (!ClassNames.ContainsKey(keyword))
            ClassNames.Add(keyword, []);

        // クラス名を追加
        ClassNames[keyword].Add(action.Method?.DeclaringType?.Name ?? string.Empty);
    }

    #endregion

    #region 購買解除メソッド

    /// <summary>
    /// 購買解除メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="action">購買時に登録したメソッド</param>
    public static void UnSubscribe(string keyword, Action<object> action)
    {
        ClassNames[keyword].Remove(action.Method?.DeclaringType?.Name ?? string.Empty);
        Subscribers[keyword].Remove(action);
    }

    /// <summary>
    /// 購買解除メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="action">購買時に登録したメソッド</param>
    public static void UnSubscribe(string keyword, Func<object, Task> action)
    {
        ClassNames[keyword].Remove(action.Method?.DeclaringType?.Name ?? string.Empty);
        SubscribeAsyncs[keyword].Remove(action);
    }

    #endregion

    #region 発行メソッド

    /// <summary>
    /// 発行メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="data">値</param>
    public static void Publish<T>(string keyword, T data) where T : notnull
    {
        // 発行元クラスを取得
        var frame = new StackFrame(1);
        var method = frame.GetMethod();
        var className = method?.DeclaringType?.Name;

        // 非同期実行
        var tasks = new List<Task>();
        if (Subscribers.TryGetValue(keyword, out var actions))
        {
            foreach (var action in actions)
            {
                // アクションのクラス名と呼び出し元クラスが一致の場合は処理しない
                var actionClassName = action.Method?.DeclaringType?.Name;
                if (className == actionClassName) continue;

                tasks.Add(Task.Run(() => action(data)));
            }
        }
        if (SubscribeAsyncs.TryGetValue(keyword, out var actionsAsyncs))
        {
            foreach (var action in actionsAsyncs)
            {
                // アクションのクラス名と呼び出し元クラスが一致の場合は処理しない
                var actionClassName = action.Method?.DeclaringType?.Name;
                if (className == actionClassName) continue;

                tasks.Add(action(data));
            }
        }

        // タスク待ち
        if (tasks.Count > 0)
            Task.WaitAll([.. tasks]);
    }

    /// <summary>
    /// 非同期発行メソッド
    /// </summary>
    /// <param name="keyword">キーワード</param>
    /// <param name="data">値</param>
    public static async Task PublishAsync<T>(string keyword, T data) where T : notnull
    {
        // 発行元クラスを取得
        var frame = new StackFrame(1);
        var method = frame.GetMethod();
        var className = method?.DeclaringType?.Name;

        // 非同期実行
        var tasks = new List<Task>();
        if (Subscribers.TryGetValue(keyword, out var actions))
        {
            foreach (var action in actions)
            {
                // アクションのクラス名と呼び出し元クラスが一致の場合は処理しない
                var actionClassName = action.Method?.DeclaringType?.Name;
                if (className == actionClassName) continue;

                tasks.Add(Task.Run(() => action(data)));
            }
        }
        if (SubscribeAsyncs.TryGetValue(keyword, out var actionsAsyncs))
        {
            foreach (var action in actionsAsyncs)
            {
                // アクションのクラス名と呼び出し元クラスが一致の場合は処理しない
                var actionClassName = action.Method?.DeclaringType?.Name;
                if (className == actionClassName) continue;

                tasks.Add(action(data));
            }
        }

        // タスク待ち
        await Task.WhenAll(tasks);
    }

    #endregion
}