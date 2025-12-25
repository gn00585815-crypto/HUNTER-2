# HUNTER-2
🤠 獵人 vs 熊 (Hunter vs Bear) 🐻

這是一個結合 Google Apps Script (GAS) 與 Google Sheets 開發的網頁遊戲，具備完整的分數資料庫 CRUD (增、查、改、刪) 功能。

🎮 遊戲演示

運行網址: [請在此填入您的 GAS 部署網址]

遊戲玩法: 左右方向鍵移動，空白鍵射擊。抵禦熊的攻擊並儲存最高分數！

🛠️ 技術特點

前端: HTML5 Canvas, Tailwind CSS, JavaScript (非同步 google.script.run)。

後端: Google Apps Script。

資料庫: Google Sheets (試算表)。

📋 CRUD 功能實現

Create: 遊戲結束上傳獵人代號與分數。

Read: 排行榜即時讀取並排序雲端數據。

Update: 可在排行榜內點擊編輯按鈕修改玩家名稱。

Delete: 管理員可刪除特定不當的分數紀錄。

🚀 部署指南 (如何自行架設)

建立試算表: 建立一個新 Google Sheet 並取得其 ID (網址列 /d/ 後的一串字元)。

開啟 GAS: 在試算表點擊 擴充功能 > Apps Script。

複製程式碼:

建立 Code.gs 並貼入本倉庫同名檔案內容，替換其中的 SPREADSHEET_ID。

建立 Game.html 與 Leaderboard.html 並貼入對應內容。

部署網頁:

點擊 部署 > 新增部署作業。

選擇 網頁應用程式，執行身分選 我，存取權選 任何人。

授權: 首次執行請務必點擊核准權限，允許 GAS 存取您的試算表。
