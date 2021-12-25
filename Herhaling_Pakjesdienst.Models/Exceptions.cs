using System;
using System.Collections.Generic;
using System.Text;

namespace Herhaling_Pakjesdienst.Models
{
    public class ValueBelowZeroException : Exception
    {
        public ValueBelowZeroException() { }
        public ValueBelowZeroException(string trigger) : base($"{trigger} kan niet negatief zijn") { }
        public ValueBelowZeroException(string message, Exception inner) : base(message, inner) { }
    }

    // unnecessary when using nameof to differentiate the messages
    //public class PriceBelowZeroException : ValueBelowZeroException
    //{
    //    public PriceBelowZeroException() : base("Prijs kan niet negatief zijn") { }
    //    public PriceBelowZeroException(string trigger) : base($"{trigger} kan niet negatief zijn") { }
    //    public PriceBelowZeroException(string message, Exception inner) : base(message, inner) { }
    //}

    //public class WeightBelowZeroException : ValueBelowZeroException
    //{
    //    public WeightBelowZeroException() : base("Gewicht kan niet negatief zijn") { }
    //    public WeightBelowZeroException(string trigger) : base($"{trigger} kan niet negatief zijn") { }
    //    public WeightBelowZeroException(string message, Exception inner) : base(message, inner) { }
    //}
}
