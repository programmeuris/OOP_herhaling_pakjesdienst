using System;
using System.Collections.Generic;
using System.Text;

namespace Herhaling_Pakjesdienst.Models
{
    public class OvernightPackage : Package
    {
        // most advanced stuff we didn't see yet has info and links in Package.cs

        //=constructors============================================================================
        public OvernightPackage(string naamVerzender,
                                string adresVerzender,
                                string naamBestemming,
                                string adresBestemming,
                                double gewichtKg,
                                double prijsKg,
                                double toeslag) : base(naamVerzender,
                                                       adresVerzender,
                                                       naamBestemming,
                                                       adresBestemming,
                                                       gewichtKg,
                                                       prijsKg)
        {
            ToeslagPerKg = toeslag;
        }

        // constructor chaining
        public OvernightPackage() : this("", "", "", "", 0, 0, 0) { }

        //=public=methods==========================================================================
        public override string BerekenVerzendingsKosten() => $"( {ToeslagPerKg} + {PrijsPerKg} ) x {Gewicht} = {VerzendingsKosten():c2}";
        public override double VerzendingsKosten() => Math.Round((PrijsPerKg + ToeslagPerKg) * Gewicht, 2);

        //=public=properties=======================================================================
        public double ToeslagPerKg
        {
            get { return _toeslagPerKg; }
            set
            {
                if (value < 0)
                    throw new ValueBelowZeroException(nameof(ToeslagPerKg));
                else
                    _toeslagPerKg = Math.Round(value, 2);
            }
        }

        //=private=variables=======================================================================
        private double _toeslagPerKg;
    }
}
