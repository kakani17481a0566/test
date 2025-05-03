@echo off
setlocal

:: Get the modified file name(s) from git status and store it in a variable
for /f "tokens=*" %%a in ('git status --porcelain ^| findstr " M"') do (
    set fileName=%%a
    set fileName=%fileName:~3%
)

:: Commit message will be the file name(s)
set commitMsg=%fileName%

:: Git commands
git add .
git commit -m "%commitMsg%"
git push origin master

pause
