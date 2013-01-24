dir bin build target /s/b >>kill.txt
For /f %%i in (kill.txt) DO rd /s/q %%i
del kill.txt
#pause