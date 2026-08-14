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
├─Common                   // Pub/Subメッセージ機能
│ ├─Keywords.cs           // メッセージキークラス
│ └─MessageBroker.cs      // Pub/Subメッセージ本体
├─SampleClass              // 実装例
│ ├─Records               // エコーバック用record
│ │ ├─RequestRecord.cs   // リクエスト用：DataGeverで発行
│ │ ├─ResponseARecord.cs // レスポンス用DataTakerAで発行
│ │ └─ResponseBRecord.cs // レスポンス用DataTakerBで発行
│ ├─A.cs                  // Aクラス：購読処理を実装
│ ├─AInit.cs              // Aクラス：発行処理を実装
│ ├─B.cs                  // Bクラス：購読と発行を実装
│ ├─C.cs                  // Cクラス：購読と発行を実装
│ ├─DataGever.cs          // エコーバック用：情報取得クラス
│ ├─DataTakerA.cs         // エコーバック用：情報提供クラスA
│ └─DataTakerB.cs         // エコーバック用：情報提供クラスB
├─Program.cs               // 実装例の呼び出し
└─SubPub-Practice.csproj
```