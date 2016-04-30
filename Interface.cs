#region HEADER

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Projet    : CODE-CHALLENGE (Épisode I : "Règlement de comptes") *
 * Auteur    : Christian BLANVILLAIN - bl@nvilla.in                *
 * Catégorie : Enseignant Informatique CFPT Genève                 *   
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Version   : 6.0 Nice Solver                                     *
 * Notes     : 10 time slower (in average) than previous one       * 
 *           : 4 time less lines of codes (400 instead of 1600)    *
 *           : Removal of parallelisation and CSharp tricks.       *
 *           : A lot object oriented and simpler to understand.    *
 * Date      : 25th of April 2016                                  *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

/// <summary>
/// 
///     Deep command line analyser for french game "Le compte est bon"
///     Rules and analyses: http://www.alliot.fr/papers/compte.pdf
///     See TV game: https://youtu.be/6mCgiaAFCu8
///     
/// </summary>
/// <returns>
/// 
///     Hard constraint: quality > lisibility > understandability > speed, which means it's VERY slow!
///     Input : target + 2 up to 8 base numbers
///     Output: total number of all best solutions possible sorted by number of operators used
///     Warning: up to 2mn for 8 numbers on Core i7-6650U 2.2GHz (16Gb Win10)
///     Limitation: +Int64 intermediary calculus
/// 
/// </returns>
/// <example>
/// 
///     NiceSolver 653 100 75 50 25 10 10
///     Return: 653=(((((50*10)-10)*100)-25)/75)
///     
/// </example>

#endregion

using System;
using System.IO; // File
using System.Diagnostics; // Stopwatch 
using NiceSolver.Properties; // For localisation purposes

namespace NiceSolver {
    static class Interface {
        
        public static void Main(string[] args) {
            
            ulong[]   initialBase;   // Set of number for performing search
            ulong     target = 0;    // Number we are looking for
            Search    search = null; // Search engine        
            Stopwatch watch;         // Chrono shared with scheduler for internal benchmarks and 

            // Step 1. Check data input 

            initialBase = new ulong[Math.Max(0, args.Length - 1)]; // Could be negative if no args
            try {    

                string msgError = Args.Check(args, ref initialBase, ref target);
                if (!String.IsNullOrEmpty(msgError)) {
                    Console.WriteLine(msgError);
                    Environment.Exit(1); // Non critical error
                }

            } catch (Exception e) {
                Console.WriteLine(Resources.ERR_READING_ARGS);
                File.AppendAllText(Resources.ERR_FILE, e.ToString()); 
                Environment.Exit(-1); // Critical error
            }
            
            // Step 2. Processing search 

            try {

                watch = new Stopwatch();
                watch.Start();
                search = new Search(initialBase, target);
                watch.Stop();
                Console.WriteLine(watch.Elapsed);

            } catch (Exception e) {
                Console.WriteLine(Resources.ERR_SEARCH_PROCESS);
                File.AppendAllText(Resources.ERR_FILE, e.ToString()); 
                Environment.Exit(-1); // Critical error
            }
            
            // Step 3. Dump data output 

            try {

                string fileName = target + "=(" + search.BaseNbrs + ").log";
                int total = search.BestSol.DumpToFile(fileName); // dumpToFile returns total number of solution
                Console.WriteLine(Resources.NSOL, total);
                Console.WriteLine(Resources.OUTPUT, fileName);

            } catch (Exception e) {
                Console.WriteLine(Resources.ERR_DUMP_FILE);
                File.AppendAllText(Resources.ERR_FILE, e.ToString()); 
                Environment.Exit(-1); // Critical error
            }
        }        
    }
}