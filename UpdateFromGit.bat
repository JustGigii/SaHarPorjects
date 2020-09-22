echo off
set /p Input=Write What You Update To The Project: 
git pull origin master
git commit -m "%Input%"
git add .
git push origin master
