using System;
using System.Collections.Generic;
using System.Text;

namespace Herhaling_Pakjesdienst.Models
{
    public class TwodayPackage : Package
    {
        // constructors
        public TwodayPackage(string naamVerzender,
                             string adresVerzender,
                             string naamBestemming,
                             string adresBestemming,
                             double gewichtKg,
                             double prijsKg,
                             double vasteKost) : base(naamVerzender,
                                                      adresVerzender,
                                                      naamBestemming,
                                                      adresBestemming,
                                                      gewichtKg,
                                                      prijsKg)
        {
            VasteKost = vasteKost;
        }

        public TwodayPackage() : this("", "", "", "", 0, 0, 0) { }

        //public methods
        public override string BerekenVerzendingsKosten() => $"{Gewicht} x {PrijsPerKg} + {VasteKost} = {VerzendingsKosten():c2}";
        public override double VerzendingsKosten() => Math.Round(Gewicht * PrijsPerKg + VasteKost, 2);


        // public properties
        public double VasteKost
        {
            get { return _vasteKost; }
            set
            {
                if (value < 0)
                    throw new ValueBelowZeroException(nameof(VasteKost));
                else
                    _vasteKost = Math.Round(value, 2);
            }


        }

        // private variables
        private double _vasteKost;
    }
}
