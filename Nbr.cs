using System; // IComparable
using System.Diagnostics; // Debug.Assert
using static RAM.Base; // Operator

namespace RAM {
    class Nbr : IComparable<Nbr> {

        internal ulong Val { get; } = 0; // Numerical value of the number itself
        internal int Level { get; } = 0; // Number of operators (parents) used to generate this number
        string _path = ""; // Interface string representation of all parents
        internal string Path {
            get {
                if (_path == "") _path = Dump(); // Singleton patern: build only first time we need it
                return _path;
            }
        }
        
        Nbr mother = null, father = null; // Parents of current number
        Operator op; // Operation used between parents to generate current number
       
        /// <summary>Creator used by Base to create initial user input numbers</summary>
        /// <param name="initialBaseNumber">User numbers</param>
        
        public Nbr(ulong initialBaseNumber) {
            Val = initialBaseNumber;
        }

        /// <summary>Creator used by Base.buildNextBase to generate new candidates for solutions</summary>
        /// <param name="motherNbr">A in A.op.B</param>
        /// <param name="fatherNbr">B in A.op.B</param>
        /// <param name="buildOp"> op in A.op.B</param>

        public Nbr(Nbr fatherNbr, Nbr motherNbr, Operator buildOp) {
            Debug.Assert(motherNbr.Val >= fatherNbr.Val, "Unsorted base");  
            mother = motherNbr;
            father = fatherNbr;
            op     = buildOp;
            Level  = mother.Level + father.Level + 1; // Number of operators used to create this number
            switch (op) {
                case Operator.ADD:
                    Val = mother.Val + father.Val; // No check here: Operator.MUL will raise exception in allmost cases
                    break;
                case Operator.MUL:
                    Val = checked(mother.Val * father.Val); // Check used to detect overflows
                    break;
                case Operator.SUB:
                    Val = mother.Val - father.Val; // Assertion needed to insure positive values
                    break;
                case Operator.DIV:
                    Val = (mother.Val % father.Val == 0) 
                          ? mother.Val / father.Val // Assertion insure results greater than 1
                          : 0; // Else: Non Euclidian division
                    break;
            }
        }

        /// <summary>String representation of number</summary>
        /// <remarks>Internally used by Path property</remarks>
        /// <returns>String representation of ancestors used to generate this number</returns>

        private string Dump() { // Recursive call (via Path property)
            return (mother == null) 
                   ? Val.ToString() // Original number 
                   : "(" + mother.Path + (char) op + father.Path + ")"; // Computed number
        }
        
        /// <summary>String representation of number as value (instead of path)</summary>
        /// <remarks>Internally used by Base.ToString</remarks>

        public override string ToString() {
            return Val.ToString();
        }

        /// <summary>Nbrs values are comparable</summary>
        /// <param name="other">The other Nbr</param>   
        /// <returns>+1 other is smaller, 0 values are equals, -1 other is bigger</returns>

        public int CompareTo(Nbr other) {
            return Val.CompareTo(other.Val);
        }
    }
}