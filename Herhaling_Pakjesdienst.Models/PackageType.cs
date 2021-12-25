using System;
using System.Collections.Generic;
using System.Text;

namespace Herhaling_Pakjesdienst.Models
{
    // this is an enum, basically a custom type of which you get to decide what values it can be
    // I could just as easily use a bool or a string over in MainWindow.xaml.cs, but this is neater
    // more info: https://docs.microsoft.com/en-us/dotnet/api/system.enum?view=netcore-3.1
    public enum PackageType
    {
        OvernightPackage,
        TwodayPackage
    }
}
