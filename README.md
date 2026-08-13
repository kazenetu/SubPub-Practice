# SubPub-Practice
Pub/Subメッセージ機能の試作

## 実行環境
* .NET8 SDK

## ライセンス
* [MITライセンス](LICENSE)  

## 実行方法
* ```dotnet run```  
  * 実行内容
    * 基本的なPub/Subメッセージ
    * エコーバック利用（複数クラスから情報取得）

## フォルダ構成
```
Root
├─Common
│ ├─Keywords.cs
│ └─MessageBroker.cs
├─SampleClass
│ ├─Records
│ │ ├─RequestRecord.cs
│ │ ├─ResponseARecord.cs
│ │ └─ResponseBRecord.cs
│ ├─A.cs
│ ├─AInit.cs
│ ├─B.cs
│ ├─C.cs
│ ├─DataGever.cs
│ ├─DataTakerA.cs
│ └─DataTakerB.cs
├─Program.cs
└─SubPub-Practice.csproj
```