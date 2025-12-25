/**
 * 設定區
 * 請將 SPREADSHEET_ID 替換為你自己的 Google Sheet ID
 */
const SPREADSHEET_ID = '1ui2mFhQW3an3f7ba-RgKQ1agMiZl0M5kUqDfrUA7-1c'; 
const SHEET_NAME = 'Scores';

/**
 * 網頁進入點
 * 根據 URL 參數 ?page=leaderboard 切換頁面
 */
function doGet(e) {
  // 確保這裡的字串與左側檔案清單中的名稱完全一致 (大小寫須相同)
  const page = e.parameter.page || 'game';
  const templateName = (page === 'leaderboard') ? 'Leaderboard' : 'Game';
  
  try {
    return HtmlService.createTemplateFromFile(templateName)
        .evaluate()
        .setTitle('獵人 vs 熊')
        .setXFrameOptionsMode(HtmlService.XFrameOptionsMode.ALLOWALL)
        .addMetaTag('viewport', 'width=device-width, initial-scale=1');
  } catch (err) {
    return HtmlService.createHtmlOutput("<h1>發生錯誤</h1><p>找不到 HTML 檔案：<b>" + templateName + "</b></p><p>請確認 GAS 專案左側檔案名稱是否正確。</p>");
  }
}

/**
 * 取得當前網頁部署網址
 */
function getScriptUrl() {
  return ScriptApp.getService().getUrl();
}

/**
 * 取得試算表物件，若無則自動初始化
 */
function getTargetSheet() {
  const ss = SpreadsheetApp.openById(SPREADSHEET_ID);
  let sheet = ss.getSheetByName(SHEET_NAME);
  if (!sheet) {
    sheet = ss.insertSheet(SHEET_NAME);
    sheet.appendRow(['ID', '名稱', '分數', '日期']);
    sheet.getRange(1, 1, 1, 4).setFontWeight('bold').setBackground('#f3f3f3');
  }
  return sheet;
}

// ==========================================
// CRUD 功能實作
// ==========================================

function createScore(name, score) {
  const sheet = getTargetSheet();
  const id = "ID_" + new Date().getTime(); 
  const date = new Date().toLocaleString('zh-TW');
  sheet.appendRow([id, name, score, date]);
  return { success: true, message: "戰績已成功儲存到雲端！" };
}

function readScores() {
  const sheet = getTargetSheet();
  const data = sheet.getDataRange().getValues();
  if (data.length <= 1) return []; // 只有標題列
  data.shift(); 
  
  const scores = data.map(row => ({
    id: row[0],
    name: row[1],
    score: row[2],
    date: row[3]
  }));
  
  return scores.sort((a, b) => b.score - a.score);
}

function updateScoreName(id, newName) {
  const sheet = getTargetSheet();
  const data = sheet.getDataRange().getValues();
  
  for (let i = 1; i < data.length; i++) {
    if (String(data[i][0]) === String(id)) {
      sheet.getRange(i + 1, 2).setValue(newName);
      return { success: true, message: "獵人代號更新成功！" };
    }
  }
  return { success: false, message: "找不到該筆資料。" };
}

function deleteScoreRecord(id) {
  const sheet = getTargetSheet();
  const data = sheet.getDataRange().getValues();
  
  for (let i = 1; i < data.length; i++) {
    if (String(data[i][0]) === String(id)) {
      sheet.deleteRow(i + 1);
      return { success: true, message: "資料已從資料庫移除。" };
    }
  }
  return { success: false, message: "刪除失敗。" };
}

