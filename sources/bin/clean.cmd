@echo off

:clear
@echo.
FOR /F "tokens=*" %%D IN ('DIR ".\*.pdb" /S /B') DO DEL /S /Q "%%D"
::FOR /F "tokens=*" %%D IN ('DIR ".\*.xml" /S /B ^| FIND /I /V "macro_capture.xml"') DO DEL /S /Q "%%D"
FOR /F "tokens=*" %%D IN ('DIR ".\*.xml" /S /B') DO DEL /S /Q "%%D"
FOR /F "tokens=*" %%D IN ('DIR ".\*.vshost.*" /S /B') DO DEL /S /Q "%%D"
goto exit

:exit
exit