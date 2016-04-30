using System; // Enum
using System.Collections.Generic; // IEnumerable

namespace RAM {
    class Base {

        internal Nbr[] BaseNb { get; }
        internal Nbr   LastNb { get; } = null; // Read by Solution.CheckCandidate
        public enum Operator { MUL = 42, ADD = 43, SUB = 45, DIV = 47 } // Ascii codes

        /// <summary>Creator used by Interface to create initial base of numbers</summary>
        /// <param name="initialBaseNumber">Users input parameters</param>
        
        public Base(ulong[] initialBaseNumber) {
            int length = initialBaseNumber.Length;            
            Array.Sort(initialBaseNumber); 
            BaseNb = new Nbr[length];
            for (int k = 0; k < length; k++)
                BaseNb[k] = new Nbr(initialBaseNumber[k]);            
        }        

        /// <summary>Creator used by buildNextBase</summary>
        /// <param name="numbers">The full array of numbers</param>
        /// <param name="lastNumber">Last build number will be checked as solution candidate</param>
        
        public Base(Nbr[] numbers, Nbr lastNumber) {
            LastNb = lastNumber;
            BaseNb = numbers;
        }

        /// <summary>Build all possible set of numbers combining existing pairs of numbers</summary>
        /// <returns>A new base if possible, nothing else</returns>
        /// <remarks>Iterator used in Search.loop foreach</remarks>
        /// <note>
        /// Idea#1: directly build a sorted base using the fact that numbers are already sorted
        /// Idea#2: reuse numbers and bases to avoid garbage collector
        /// Gain..: between 20 to 60% faster (we actually try those tricks)
        /// Lost..: awfull lisibility and a lot of more lines of code
        /// </note>
         
        public IEnumerable<Base> BuildNextBase() {

            Nbr twin = new Nbr(0); // To eliminate duplicated bases build with same numbers
            for (int inA = 0; inA < BaseNb.Length - 1; inA++) { 
                Nbr nbrA = BaseNb[inA]; // foreach (Nbr nbrA in BaseNb)
                if (nbrA.Path != twin.Path) { // Values and ancestors are the same, so they are twins
                    twin = nbrA; // Two twins will follow because the numbers are sorted
                    for (int inB = inA + 1; inB < BaseNb.Length; inB++) {
                        Nbr nbrB = BaseNb[inB]; // foreach (Nbr nbrB in BaseNb)                                            
                        foreach (Operator op in Enum.GetValues(typeof(Operator))) {
                            Nbr newNbr = new Nbr(nbrA, nbrB, op); // Combine all pairs with all operators
                            if (newNbr.Val != 0) { // This is a valid operation
                                List<Nbr> newBase = new List<Nbr> { newNbr }; // Build new base with newNbr
                                foreach (Nbr nbr in BaseNb) // Copy all existing numbers
                                    if (nbr != nbrA && nbr != nbrB) // Jump over newNbr parents
                                        newBase.Add(nbr);
                                newBase.Sort(); // Sort necessary for twins prune mecanism
                                yield return new Base(newBase.ToArray(), newNbr);
                            }
                        }                        
                    }
                }
            }                                                        
        }

        /// <summary>Footprint of base numbers</summary>
        /// <returns>One single string that represent all numbers values</returns>
        /// <remarks>Used by Interface for memoization</remarks>
        /// <seealso cref="http://goo.gl/dWB6Xv"/>

        public override string ToString() {
            return string.Join(" ", Array.ConvertAll(BaseNb, nbr => nbr.Val));
        }
    }
}