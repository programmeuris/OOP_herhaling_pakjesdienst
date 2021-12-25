﻿using System;

namespace Herhaling_Pakjesdienst.Models
{
    public class Package
    {
        // NOTE: should really use the decimal type for any form of currency rather than double
        //       leaving it as double because that's what the assignment instructs

        // ASS: Er is maar 1 constructor aanwezig en dat is de constructor met alle parameters.
        //      Er mag geen enkele andere constructor toegevoegd worden!                                OK

        //      De waarde PrijsPerKg mag niet kleiner zijn dan 0. Indien de waarde kleiner dan 0 is,
        //      dan wordt een Exception gegooid met als melding "Prijs per kg kan niet negatief zijn".  OK

        //      De waarde Gewicht mag niet kleiner zijn dan 0. Indien de waarde kleiner dan 0 is,
        //      dan wordt een Exception gegooid met als melding "Gewicht kan niet negatief zijn".       OK

        //      VerzendingsKosten() wordt als volgt berekend: Gewicht * PrijsPerKg                      OK

        //      BerekenVerzendingsKosten() returnt een string en wordt als volgt opgebouwd:
        //      "Gewicht x PrijsPerKg = VerzendingsKosten"                                              OK

        //      ToString() returnt als volgt:

        //          Gegevens verzending
        //          Verzender: Verzender, VerzenderAdres
        //          Bestemmeling: BestemmingNaam, BestemmingAdres
        //          Gewicht: Gewicht
        //          Te Betalen: Verzendingskosten                                                       OK

        //      Alle numerieke waarden worden tot 2 cijfers na de komma afgerond.                       OK


        // protected constructor
        protected Package(string naamVerzender,
                          string adresVerzender,
                          string naamBestemming,
                          string adresBestemming,
                          double gewichtKg,
                          double prijsKg)
        {
            VerzendAdres = adresVerzender;
            VerzendNaam = naamVerzender;
            BestemmingAdres = adresBestemming;
            BestemmingNaam = naamBestemming;
            Gewicht = gewichtKg;
            PrijsPerKg = prijsKg;
        }

        // public methods
        public virtual string BerekenVerzendingsKosten() => $"{Gewicht} x {PrijsPerKg} = {VerzendingsKosten():c2}";
        public virtual double VerzendingsKosten() => Gewicht * PrijsPerKg;
        public override string ToString() => $"Gegevens verzending\n" +
                                             $"Verzender: {VerzendNaam}, {VerzendAdres}\n" +
                                             $"Bestemmeling: {BestemmingNaam}, {BestemmingAdres}\n" +
                                             $"Gewicht: {Gewicht:n2}\n" +
                                             $"Te Betalen: {VerzendingsKosten():c2}";


        // public properties
        public string BestemmingAdres
        {
            get { return _bestemmingAdres; }
            set { _bestemmingAdres = value; }
        }

        public string BestemmingNaam
        {
            get { return _bestemmingNaam; }
            set { _bestemmingNaam = value; }
        }

        public double Gewicht
        {
            get { return _gewicht; }
            set
            {
                if (value < 0)
                    throw new ValueBelowZeroException(nameof(Gewicht));
                else
                    _gewicht = Math.Round(value, 2);
            }
        }

        public double PrijsPerKg
        {
            get { return _prijsPerKg; }
            set
            {
                if (value < 0)
                    throw new ValueBelowZeroException(nameof(PrijsPerKg));
                else
                    _prijsPerKg = Math.Round(value, 2);
            }
        }

        public string VerzendAdres
        {
            get { return _verzendAdres; }
            set { _verzendAdres = value; }
        }

        public string VerzendNaam
        {
            get { return _verzendNaam; }
            set { _verzendNaam = value; }
        }

        // private vars
        private string _bestemmingAdres;
        private string _bestemmingNaam;
        private double _gewicht;
        private double _prijsPerKg;
        private string _verzendAdres;
        private string _verzendNaam;
    }
}
