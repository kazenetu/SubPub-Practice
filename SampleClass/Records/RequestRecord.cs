namespace SubPub_Practice.SampleClass.Records;

/// <summary>
/// リクエストレコード
/// </summary>
/// <param name="Target">リクエスト対象</param>
public record RequestRecord(RequestRecord.Targets Target)
{
    /// <summary>
    /// リクエスト対象
    /// </summary>
    public enum Targets
    {
        /// <summary>
        /// データ提供クラスA
        /// </summary>
        DataTakerA,

        /// <summary>
        /// データ提供クラスB
        /// </summary>
        DataTakerB,
    }
}