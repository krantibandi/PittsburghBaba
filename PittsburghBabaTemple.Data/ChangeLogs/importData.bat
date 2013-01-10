@echo off
liquibase --changeLogFile=Update/Temp/rename_me.xml --dataDir=Update/Temp/data --diffTypes=data generateChangeLog 2> nul
pause