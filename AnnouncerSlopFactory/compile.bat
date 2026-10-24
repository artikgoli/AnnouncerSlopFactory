@echo off
setlocal
cd /d "%~dp0"

echo ============================================
echo   Building portable (framework-dependent)
echo ============================================
echo.

dotnet publish AnnouncerSlopFactory.csproj ^
  -c Release ^
  -r win-x64 ^
  --self-contained false ^
  -p:PublishSingleFile=true ^
  -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:PublishReadyToRun=true ^
  -p:DebugType=None ^
  -p:DebugSymbols=false ^
  -o "%~dp0portable"

if errorlevel 1 (
    echo.
    echo BUILD FAILED. Check the errors above.
    pause
    exit /b 1
)

echo.
echo ============================================
echo   DONE
echo   Output: %~dp0portable\AnnouncerSlopFactory.exe
echo ============================================
echo.
dir "%~dp0portable\AnnouncerSlopFactory.exe"
pause