using System.Linq; // bool.Count
using System.Collections.Generic; // HashSet
using RAM; // Objects built by search process

namespace NiceSolver {    
    class Search {

        internal Base      BaseNbrs  { get; set; } // Initial base of numbers
        internal Solutions BestSol { get; set; } // Best founded solutions (or approximated solutions)
        
        // Accelerators for letting 8 numbers compute faster (~2mn instead of ~18h!) 
        private static HashSet<string> pruneTreeSearch = new HashSet<string>(); // To prune branches of tree search        

        /// <summary>Creator of search process</summary>
        /// <param name="initialBase">Initial base of numbers<</param>
        /// <param name="target">Number to find</param>

        public Search(ulong[] initialBase, ulong target) {
            BaseNbrs = new Base(initialBase);  // Setup initial base of numbers
            BestSol  = new Solutions(target);  // Setup target
            BestSol.CheckCandidate(BaseNbrs); // initialBase could already contains solutions                
            Loop(BaseNbrs); // Start recursive search
        }

        /// <summary>Main recursive search engine</summary>
        /// <param name="numbers">The initial base of numbers</param>
        /// <param name="solutions">[GLOBAL] Solutions is a manager that memorise best candidates</param>
        /// <returns>True or false, if current base leads to some solutions or not</returns>    
        /// <note>
        /// Parallelisation has not a good speed up (less than twice faster with 3 threads) probably because of memoization?
        /// You need:
        /// - merge method in Solution static class with lock mecanism (faster than concurrent dictionnary)
        /// - modulo Environment.ProcessorCount in first iteration of main loop to split the work
        /// Drawback:
        /// It adds complexity for realy few improvments (on my tablet). Code available on demand.
        /// </note>
        
        private bool Loop(Base numbers) { // Greedy algorithm: combining of all pairs of numbers
            bool found = false; // Does this base leads to solutions?
            foreach (Base newBase in numbers.BuildNextBase()) { // Generate all possible solutions 
                if (newBase != null) { // End of search branch of recursive calls
                    if (!pruneTreeSearch.Contains(newBase.ToString())) { // Memoization (only for speed)
                        // Eventually found a solution (last number generated). Then tail-recursive call with a smaller set of numbers.
                        if (Truth(BestSol.CheckCandidate(newBase.LastNb), Loop(newBase)) > 0) { // At least one is true 
                            found = true; // Thus this base generates solutions
                        } else { // We add this branch to set of prune branches of tree search
                            pruneTreeSearch.Add(newBase.ToString()); // We won't search again with those numbers
                        }
                    }
                }
            }
            return found; // Leads to a solution (or not)
        }        

        /// <summary>Count number of true values</summary>
        /// <param name="booleans">list of booleans values</param>
        /// <returns>number of true values in input list</returns>
        /// <seealso cref="http://goo.gl/Z2mxBi"/>

        public static int Truth(params bool[] booleans) {
            return booleans.Count(b => b);
        }
    }
}