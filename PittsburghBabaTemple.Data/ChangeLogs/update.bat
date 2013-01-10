@echo off
liquibase --changeLogFile=update.xml update
pause