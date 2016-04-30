using System;
using NiceSolver.Properties; // For localisation purposes

namespace NiceSolver {
    static class Args {
        
        /// <summary>Check user input arguments</summary>
        /// <param name="args">List or arguments: target must be in first position</param>
        /// <param name="target">Output target</param>
        /// <param name="initialBase">Output initial base of numbers</param>
        /// <returns>Error message in case of problem</returns>
        
        public static string Check(string[] args, ref ulong[] initialBase, ref ulong target) {

            const ushort MIN_PARAM = 3; // Min nbr of parameters: 2 numbers + 1 target = 3
            const ushort MAX_PARAM = 9; // Max nbr of parameters: 8 numbers + 1 target = 9. No options.

            ulong[] input = new ulong[args.Length];
            
            // Check number of parameters
            if (args.Length < MIN_PARAM || args.Length > MAX_PARAM) 
                return string.Format(Resources.HELP, System.AppDomain.CurrentDomain.FriendlyName);
            
            // Try to convert parameters 
            for (int i = 0; i < args.Length; i++) 
                if (!UInt64.TryParse(args[i], out input[i])) 
                    return string.Format(Resources.HELP, System.AppDomain.CurrentDomain.FriendlyName);                                    

            // Check all strict positive values
            bool strictPositiveValues = true;
            for (int k = 0; k < args.Length; k++) 
                strictPositiveValues &= (input[k] > 0); 
            if (!strictPositiveValues)
                return Resources.FORBIDDEN_NBRS;
            
            // Build data set
            target = input[0]; 
            for (int k = 1; k < args.Length; k++) 
                initialBase[k - 1] = input[k];
            return null; // Everything is OK!
        }     
    }
}
