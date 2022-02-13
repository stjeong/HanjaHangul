FOR /F %%I IN ("%0") DO SET CURRENTDIR=%%~dpI
SET SOLUTIONDIR=%CURRENTDIR%

SET PROJECTPATH=%SOLUTIONDIR%HanConv\HanConv.csproj

SET OUTPUTPATH=%SOLUTIONDIR%\bin
msbuild "%PROJECTPATH%" /t:rebuild /p:Configuration=Release /p:OutputPath=%OUTPUTPATH%

:EndOfBuild