@echo off
REM rollback last 1 change sets
liquibase --changeLogFile=update.xml Rollack "1"
REM pause