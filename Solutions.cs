using System; // Environment.NewLine
using System.IO; // File
using System.Collections.Generic; // HashSet
using NiceSolver.Properties; // Resources for dumpToFile customisation

namespace RAM {
    class Solutions {
        
        ulong target; // The number we are looking for        
        ulong best = ulong.MaxValue; // The minimum distance with target founded in solutions    

        const int MAX = 8; // We could accept potentially any numbers of numbers BUT execution with 8 is really slow...
        HashSet<string>[] sol = new HashSet<string>[MAX]; // Current closest set of solution founded (group by number of operators used)

        /// <summary>Creator used by Interface to define target</summary>
        /// <param name="targetNumber">Number we are looking for</param>

        public Solutions(ulong targetNumber) {
            target = targetNumber;
            for (int k = 0; k < MAX; k++)
                sol[k] = new HashSet<string>();
        }

        /// <summary>Will eventually memorise best solutions</summary>
        /// <param name="number">A candidate</param>
        /// <returns>Modify sol HashSet of best solutions</returns>

        public bool CheckCandidate(Nbr number) {
            ulong proximity = (target > number.Val) ? (target - number.Val) : (number.Val - target); // Math.Abs(ulong) doesn't exist
            if (proximity <= best) {
                if (proximity < best) {
                    best = proximity; // Better solution found iff strict inegality
                    for (int k = 0; k < MAX; k++)
                        sol[k].Clear(); // Erase all previous solutions
                }
                sol[number.Level].Add(number.Val + "=" + number.Path);
                return true;
            } else {
                return false;
            }
        }

        /// <summary>Used by Interface to check initial numbers in the base</summary>
        /// <param name="initialBase">User initial input base of numbers</param>

        public void CheckCandidate(Base initialBase) {
            foreach(Nbr number in initialBase.BaseNb)
                CheckCandidate(number);
        }

        /// <summary>Used by Interface to ouput all founded solutions in a file</summary>
        /// <param name="fileName">File name generate by Interface (with target and initial numbers)</param>
        /// <returns>Total number of founded solutions</returns>
        
        public int DumpToFile (string fileName) {
            int total = 0; // Total number of solutions
            string[] results = new string[MAX];
            for (int k = 0; k < MAX; k++)
                if (sol[k].Count > 0) { // One approximate solution always exists
                    total += sol[k].Count;
                    results[k] = string.Format(Resources.K_SOLUTIONS_FOUND, sol[k].Count, k + 1, Environment.NewLine) 
                               + string.Join(Environment.NewLine, sol[k]);
                }
            File.WriteAllLines(fileName, results); // File long term storage of solutions for further analysis
            return total;
        }
    }
}