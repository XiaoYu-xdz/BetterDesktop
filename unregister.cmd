@echo off
chcp 65001 >nul
cd /d "F:\XiaoLv\ZcodeProject\.zcode\workspace\default\BetterDesktop\BetterDesktopHandler\bin\x64\Debug"

echo Unregistering...

"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" BetterDesktopHandler.dll /unregister
reg delete "HKCR\Directory\shellex\ContextMenuHandlers\BetterDesktop" /f 2>nul
reg delete "HKCR\Folder\shellex\InfoTip" /ve /f 2>nul

taskkill /f /im explorer.exe
start explorer.exe

echo Uninstalled!
pause