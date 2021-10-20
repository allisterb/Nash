REM @echo off
REM oc delete bc Nash
REM oc delete dc Nash
REM oc delete svc Nash

REM cd C:\Projects\Nash\src\Nash.Web

REM dotnet publish -c Debug /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
REM oc new-build --name=Nash dotnet:3.1-el7 --binary=true
REM oc start-build Nash --from-dir=bin\Debug\netcoreapp3.1\publish
REM oc new-app Nash -e PGSQL=172.30.80.112 -e EXPERTAI_USER=%EAI_USERNAME% -e EXPERTAI_PASS=%EAI_PASSWORD% -e EXPERTAI_TOKEN=<t> -e TYPINGDNA_KEY=%TYPINGDNA_KEY% -e TYPINGDNA_SECRET=%TYPINGDNA_SECRET% -e AZURE_SPEECH_KEY=%AZURE_SPEECH_KEY% -e AZURE_SPEECH_ENDPOINT=%AZURE_SPEECH_ENDPOINT% -e AZURE_FACE_KEY=%AZURE_FACE_KEY% -e AZURE_FACE_ENDPOINT=%AZURE_FACE_ENDPOINT%
REM oc expose svc/Nash
REM cd C:\Projects