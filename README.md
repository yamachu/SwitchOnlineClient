# DOES NOT WORK DUE TO API'S BREAKING CHANGE

# SwitchOnlineClient

## About

某アプリのクライアントの .NET 実装

## How to use

See sample application

### How to get some tokens

__SessionTokenCode__ と __SessionTokenCodeVerifier__ はログイン後のアカウント選択画面から取得することが出来ます．

1. 連携アカウントの選択の "これにする" ボタンを右クリックしてリンクのコピーを行います．
2. 取得したリンクのクエリパラメータの "session\_token\_code" が SessionTokenCode に，
"state" が SessionTokenCodeVerifier に対応します．

`NintendoOAuthService.GetSessionToken` で取得した SessionToken は "おそらく" 使い回しが出来ると思うので，保存するなりして再利用してください．

## Notice

認証のための URL の作成の際ランダムなパラメータを使用していますが，桁数を合わせたら通ったため正確でないかもしれません．
また海外フォーラムの解析情報や Proxy を通して確認した値を元にしてクエリパラメータや API のエンドポイントを指定したため，実装ミスなどにより正確なリクエストパラメータなどを送信していない恐れがあります．
そのためライブラリの使用に起因して生じた損害については，一切責任を負いません．
