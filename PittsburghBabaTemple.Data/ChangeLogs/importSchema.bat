@echo off
liquibase --changeLogFile=Update/Temp/rename_me.xml generateChangeLog 2> nul
pause