@echo off
chcp 65001 >nul
cd /d "F:\XiaoLv\ZcodeProject\.zcode\workspace\default\BetterDesktop\BetterDesktopHandler\bin\x64\Debug"

echo Step 1: Register COM component...
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" BetterDesktopHandler.dll /codebase

echo Step 2: Remove old InfoTip registration...
reg delete "HKCR\Folder\shellex\InfoTip" /ve /f 2>nul

echo Step 3: Register as ContextMenu handler for folders...
reg add "HKCR\Directory\shellex\ContextMenuHandlers\BetterDesktop" /ve /d "{B8E2D3F1-5A7C-4E9B-8D1F-3C6A9B2E7F4D}" /f

echo Step 4: Restart Explorer...
taskkill /f /im explorer.exe
start explorer.exe

echo Done!
echo Test: right-click a folder + hold Shift -> popup window appears
pause