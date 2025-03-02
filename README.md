# FGORankGenerator

- [最強サーヴァントランキング](https://appmedia.jp/fategrandorder/1351236)
- からランキングを取得して所持チェックできるサイトの WebApi

## 構成

- Domain
  - 外部要因で影響を受けないロジック
- Infrastructure
  - 外部ソース(DB 含む)に影響を受ける部分
- UI
  - UI(WebApi フレームワーク等)に影響を受ける部分
  - ASP.NET Core WebApi

## スクレイピング(HTML 解析)技術選定

- 1 [HtmlAgilityPack](https://www.nuget.org/packages/HtmlAgilityPack/)
  - 一番使われている, 非構造的な HTML もパースしやすい, XPath や LINQ を用いた柔軟なクエリ
  - JS レンダリング非対応, CSS セレクタは追加ライブラリが必要
  - ダウンロード数:215.6M , 直近更新: 2025/2
- 2 [AngleSharp](https://www.nuget.org/packages/AngleSharp/)
  - 最新の Web 標準に対応, DOM ツリー操作や CSS セレクタを用いた要素抽出が可能
  - JS レンダリング対応は別ライブラリ, Selenium ほどの柔軟性はない
  - ダウンロード数:141.5M , 直近更新: 2025/2
- 3 [Selenium WebDriver](https://www.nuget.org/packages/Selenium.WebDriver/)
  - ブラウザ自動操作なので動的コンテンツに対応, UI 自動テストもできる
  - 実行速度が遅く大量データには不向き, ブラウザドライバが必要
  - ダウンロード数:125.7M , 直近更新: 2025/2
- 4 [IronWebScraper](https://www.nuget.org/packages/IronWebScraper/)
  - 最近のライブラリ, 並列処理で高速, エラーハンドリングが容易, C#クラスインスタンスや JSON 形式で取得できる
  - 一部有料, JS レンダリング非対応
  - ダウンロード数:100.2K , 直近更新: 2025/2
