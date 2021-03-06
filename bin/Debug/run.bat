@echo OFF



REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Project   : CODE-CHALLENGE (Episode I : "Reglement de comptes") =
REM = Autor     : Christian BLANVILLAIN - bl@nvilla.in                = 
REM = Category  : Enseignant Informatique CFPT Geneve                 =
REM = Notes     : Benchmarks using Core i7-6650U 2.2GHz 16Gb Win10    =
REM =           : More info see http://goo.gl/Or6qZz                  =
REM =           : Move to bin/Debug after .exe generation             =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Version   : 3.0                                                 =
REM = Notes     : Remove all options and all durations                =
REM = Date      : 27th April 2016                                     =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Version   : 2.2                                                 =
REM = Notes     : Run add mode. Add count and duration results        =
REM = Date      : 31th March 2016                                     =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Version   : 2.1                                                 =
REM = Notes     : Add few tests                                       =
REM = Date      : 28th March 2016                                     =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Version   : 2.0                                                 =
REM = Notes     : Parametrisation                                     =
REM =           : Add procedures                                      =
REM = Date      : 20th March 2016                                     =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Version   : 1.1                                                 =
REM = Notes     : Add expected results using echo                     =
REM = Date      : 16th March 2016                                     =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
REM = Version   : 1.0                                                 =
REM = Notes     : Script creation                                     =
REM = Date      : 13th March 2016                                     =
REM = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =



set EXE=NiceSolver
set LCEB=%EXE%.exe

IF [%1] == []  (
		GOTO :USAGE 
	)
IF [%1] == [1] (
		CALL :ROBUSTNESS_ERGONOMY
	)
IF [%1] == [2] (
		CALL :RIGHTNESS_EFFICIENCY
	)
IF [%1] == [3] (
		CALL :ROBUSTNESS_ERGONOMY 
		CALL :RIGHTNESS_EFFICIENCY
	)
IF [%1] == [4] (
		CALL :BENCHMARK   
	)		
IF [%1] == [5] (
		CALL :ROBUSTNESS_ERGONOMY
		CALL :BENCHMARK   
	)		
IF [%1] == [6] (
		CALL :RIGHTNESS_EFFICIENCY
		CALL :BENCHMARK   
	)
IF [%1] == [7] (
		CALL :ROBUSTNESS_ERGONOMY 
		CALL :RIGHTNESS_EFFICIENCY
		CALL :BENCHMARK   
	)
	
GOTO :EOF





:USAGE

	echo .
    echo USABILITY TESTS FOR "%LCEB%"
	echo No parameters will shows this help
	echo ==============================================================
	echo First Param:
	echo = 1 Run Robustness and Ergonomy tests
	echo = 2 Run Rightness and Efficiency tests
	echo = 4 Run Benchmark tests
	echo = Other values between 1 and 7 will combine previous test
	echo ==============================================================
	echo Notes:
	echo = Install into %EXE%/bin/Debug close to .exe 
	echo = Edit line #41 "set EXE=%EXE%" with correct .exe name
	echo = Performances drop down if compiling with no optimisation
	echo ==============================================================
	echo Example: 
	echo %0 7 (run all tests)
	
GOTO :EOF
	
	


	
:ROBUSTNESS_ERGONOMY

	echo ==============================================================
	echo =====        PART ONE - ROBUSTNESS AND ERGONOMY          =====
	echo ==============================================================
	echo ===== Normal
	echo ===== 1 1 1
	echo ==============================================================
	%LCEB% 1 1 1
	
	echo ==============================================================
	echo ===== Negative
	echo ===== 1 2 -3 4 5 6
	echo ==============================================================
	%LCEB% 1 2 -3 4 5 6
	
	echo ==============================================================
	echo ===== Decimal as number
	echo ===== 1 2 3.14 4 5
	echo ==============================================================
	%LCEB% 1 2 3.14 4 5
	
	echo ==============================================================
	echo ===== Decimal as target
	echo ===== 3.14 3 2 1
	echo ==============================================================
	%LCEB% 3.14 3 2 1

	echo ==============================================================
	echo ===== Big number
	echo ===== 1000 100 10
	echo ==============================================================
	%LCEB% 1000 100 10
		
	echo ==============================================================
	echo ===== Zero as number
	echo ===== 2 0 1 6
	echo ==============================================================
	%LCEB% 2 0 1 6
	
	echo ==============================================================
	echo ===== Zero as target
	echo ===== 0 100 100
	echo ==============================================================
	%LCEB% 0 100 100
	
	echo ==============================================================
	echo ===== Unknow parameter
	echo ===== -z 1 1 1 1 1 1 1
	echo ==============================================================
	%LCEB% -z 1 1 1 1 1 1 1
	
	echo ==============================================================
	echo ===== Too much numbers
	echo ===== 510512 1 2 3 5 7 11 13 17 19
	echo ==============================================================
	%LCEB% 510512 1 2 3 5 7 11 13 17 19
	
	echo ==============================================================
	echo ===== Overflow
	echo ===== 1 18446744073709551615 18446744073709551615
	echo ==============================================================
	%LCEB% 1 18446744073709551615 18446744073709551615
	
	echo ==============================================================
	echo ===== Overflow 8 numbers
	echo ===== 999 888 777 666 555 444 333 222 111
	echo ==============================================================
	%LCEB% 999 888 777 666 555 444 333 222 111
	
GOTO :EOF





:RIGHTNESS_EFFICIENCY

	echo ==============================================================
	echo =====        PART TWO - RIGHTNESS AND EFFICIENCY         =====
	echo ==============================================================
	echo ===== 9                                      1 1 1 1 1 1 =====
	echo ==============================================================
	%LCEB% 9 1 1 1 1 1 1 
	
	echo ==============================================================
	echo ===== 886                                  4 3 5 100 1 6 =====
	echo ==============================================================
	%LCEB% 886 4 3 5 100 1 6
	
	echo ==============================================================
	echo ===== 653                             10 10 25 50 75 100 =====
	echo ==============================================================
	%LCEB% 653 10 10 25 50 75 100

	echo ==============================================================
	echo ===== 660                                2 2 7 50 75 100 =====
	echo ==============================================================
	%LCEB% 660 2 2 7 50 75 100
	
	echo ==============================================================
	echo ===== 952                               100 75 50 25 6 3 =====
	echo ==============================================================
	%LCEB% 952 100 75 50 25 6 3
	
	echo ==============================================================
	echo ===== 255255                              3 5 7 11 13 17 =====
	echo ==============================================================
	%LCEB% 255255 3 5 7 11 13 17
	
	echo ==============================================================
	echo ===== 1155                          10 10 10 10 10 10 10 =====
	echo ==============================================================
	%LCEB% 1155 10 10 10 10 10 10 10
	
	echo ==============================================================
	echo ===== 653                        10 10 25 50 75 100 1000 =====
	echo ==============================================================
	%LCEB% 653 10 10 25 50 75 100 1000
	
	echo ==============================================================
	echo ===== 510510                            2 3 5 7 11 13 17 ===== 
	echo ==============================================================
	%LCEB% 510510 2 3 5 7 11 13 17

GOTO :EOF





:BENCHMARK

	echo ==============================================================
	echo =====             PART TREE - BENCHMARK                  =====
	echo ==============================================================
	echo ===== 9                  100 100 100 100 100 100 100 100 =====
	echo ==============================================================
	%LCEB% 9 100 100 100 100 100 100 100 100

	echo ==============================================================
	echo ===== 3                                  2 2 2 2 2 2 2 3 =====
	echo ==============================================================
	%LCEB% 3 2 2 2 2 2 2 2 3
	
	echo ==============================================================
	echo ===== 9                        100 100 75 75 50 50 25 25 =====
	echo ==============================================================
	%LCEB% 9 100 100 75 75 50 50 25 25 	
	
	echo ==============================================================
	echo ===== 510511                          1 2 3 5 7 11 13 17 ===== 
	echo ==============================================================
	%LCEB% 510511 1 2 3 5 7 11 13 17
		
	echo ==============================================================
	echo ===== 9699690                        2 3 5 7 11 13 17 19 ===== 
	echo ==============================================================
	%LCEB% 9699690 2 3 5 7 11 13 17 19
			
GOTO :EOF





:EOF

	echo ==============================================================
	echo =====          END OF USABILITY TEST SCRIPT              =====
	echo ==============================================================
