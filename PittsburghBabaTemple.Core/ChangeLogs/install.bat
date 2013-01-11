@echo off
REM liquibase  --changeLogFile=ChangeLog.xml --logLevel=DEBUG --url=jdbc:sqlserver://localhost;databaseName=SSSBT --username=sssbt --password=$$$bt migrate

@echo off

if "%1%" == "" GOTO CHECKPARAMETERS
	GOTO VALIDATEDATABASE

:CHECKPARAMETERS
ECHO You cannot run Install.bat with out parameters use install_local.bat or install_uat.bat based on your environments 
ECHO.&ECHO.Press any key to exit.&PAUSE>NUL&GOTO:EOF

:VALIDATEDATABASE
if "%1%" == "localhost"	GOTO INSTALLLIQUIBASE
if "%1%" == "MOONVTADUATDB1" GOTO INSTALLLIQUIBASE
if "%1%" == "staging" GOTO UPDATESTAGING
if "%1%" == "production" GOTO UPDATEPRODUCTION

ECHO.Not a valid Database.
ECHO.&ECHO.Press any key to exit.&PAUSE>NUL&GOTO:EOF


:INSTALLLIQUIBASE
liquibase  --changeLogFile=install.xml --logLevel=SEVERE --url=jdbc:sqlserver://%1%;databaseName=SSSBT --username=sssbt --password=$$$bt dropAll
liquibase  --changeLogFile=install.xml --logLevel=INFO --url=jdbc:sqlserver://%1%;databaseName=SSSBT --username=sssbt --password=$$$bt update

ECHO.
ECHO SSSBT database reset 
PAUSE
GOTO:EOF

:UPDATESTAGING
liquibase  --changeLogFile=install.xml --logLevel=INFO --url=jdbc:sqlserver://tad-db1win;databaseName=SSSBT --username=sssbt --password=$$$bt update

ECHO.
ECHO Staging Update 
PAUSE
GOTO:EOF

:UPDATEPRODUCTION
liquibase  --changeLogFile=install.xml --logLevel=INFO --url=jdbc:sqlserver://tad-db1win;databaseName=SSSBT --username=sssbt --password=$$$bt update

ECHO.
ECHO Production Update 
PAUSE




