@echo off
set path=%~d0%~p0
FOR /F "tokens=*" %%D IN ('DIR "%path%*.png" /S /B') DO "%path%optipng.exe" -v -force -o7 -strip all "%%D"
