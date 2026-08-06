namespace SubPub_Practice.SampleClass.Records;

/// <summary>
///  応答レスポンスAレコード
/// </summary>
/// <param name="DataStrings">文字列リストデータ</param>
/// <param name="DataString">文字列データ</param>
public record ResponseARecord(List<string> DataStrings, string DataString);