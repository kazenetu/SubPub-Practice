using SubPub_Practice.Commons;

namespace SubPub_Practice.SampleClass;

public partial class A
{
    /// <summary>
    /// 発行(クラス名：AInitとして発行)
    /// </summary>
    public void Publish()
    {
        MessageBroker.Publish(Keywords.Test, 1m);
    }

    /// <summary>
    /// 発行(クラスType指定)
    /// </summary>
    public async void PublishAsync()
    {
        await MessageBroker.PublishAsync(Keywords.Test, "A.PublishAsync! ClassType", GetType());
    }
}