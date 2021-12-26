using System;
using System.Collections.Generic;
using System.Text;

namespace Herhaling_Pakjesdienst.Models
{
    // I decided to group all exceptions in a single class file because they clutter the project otherwise
    public class ValueBelowZeroException : Exception
    {
        public ValueBelowZeroException() { }

        // supply the name of the thing that caused the exception and it will update the message to use that name
        public ValueBelowZeroException(string trigger) : base($"{trigger} kan niet negatief zijn") { }

        // this one isn't used, but the docs said it was a good idea to include it anyway
        // more info: https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
        public ValueBelowZeroException(string message, Exception inner) : base(message, inner) { }
    }

    public class PackageTypeInvalidException : Exception
    {
        public PackageTypeInvalidException() { }

        public PackageTypeInvalidException(string trigger) : base($"{trigger} is geen geldig pakkettype") { }

        public PackageTypeInvalidException(string message, Exception inner) : base(message, inner) { }
    }

    // this implementation does not need a name supplied, but just sets a message based on which derived type you throw
    // NOTE: if you want these to work change $"{trigger} kan niet negatief zijn" to trigger or the message will be off
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
