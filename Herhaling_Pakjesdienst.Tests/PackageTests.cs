using NUnit.Framework;
using Herhaling_Pakjesdienst.Models;
using System;

namespace Herhaling_Pakjesdienst.Tests
{
    [TestFixture]
    [SetCulture("en-US")] // all tests run on the same culture
    public class PackageTests
    {
        // cannot create a base package since its only constructor is protected
        // to get around this I create a TwoDayPackare and cast that to its base class

        [Test]
        public void Throws_ValueBelowZeroException_When_Gewicht_LessThan_Zero()
        {
            // delegates zijn pas iets voor in jaar 2
            Assert.Throws<ValueBelowZeroException>(delegate 
            { 
                Package _ = new TwodayPackage("", "", "", "", -1, 0, 0); 
            });
        }

        [Test]
        public void ThrowsValueBelowZeroException_When_PrijsPerKg_LessThan_Zero()
        {
            // delegates zijn pas iets voor in jaar 2
            Assert.Throws<ValueBelowZeroException>(delegate
            { 
                Package _ = new TwodayPackage("", "", "", "", 0, -1, 0); 
            });
        }

        // TestCase is a way to run a single test multiple times with different inputs
        [TestCase(420, 69)]
        [TestCase(12, 2.23)]
        [TestCase(12.34, 2)]
        public void VerzendingsKosten_Returns_Valid_Output(double weight,
                                                           double priceKg)
        {
            Package package = new TwodayPackage("", "", "", "", weight, priceKg, 0);

            // if the difference between the expected value and the actual value is smaller
            // than the neccesary precision (2 digits in this case), both numbers are equal
            Assert.IsTrue(Math.Abs(package.VerzendingsKosten() - (weight * priceKg)) < 0.001);

            // result when regularly comparing both values aka magic float number shenanigans:
            // Assert.AreEqual(weight * priceKg, package.VerzendingsKosten());
            //      Expected: 26.759999999999998d
            //      But was:  26.760000000000002d
        }

        [Test]
        public void ToString_Returns_Valid_Output()
        {
            // note I'm using the TwodayPackage constructor since the Package constructor is protected
            Package package = new TwodayPackage("Barry Batspak",
                                                "Ergens in Maaskantje",
                                                "Rikkert Biemans",
                                                "zijn Opel Manta",
                                                420, 69, 0);

            string expectedOutput = $"Gegevens verzending\n" +
                                    $"Verzender: Barry Batspak, Ergens in Maaskantje\n" +
                                    $"Bestemmeling: Rikkert Biemans, zijn Opel Manta\n" +
                                    $"Gewicht: 420.00\n" +
                                    $"Te Betalen: $28,980.00";

            Assert.AreEqual(expectedOutput, package.ToString());
        }

        [Test]
        public void Gewicht_Rounds_To_2_Decimal_Places()
        {
            Package package = new TwodayPackage("", "", "", "", 69.423, 0, 0);

            Assert.AreEqual("69.42", package.Gewicht.ToString());
        }

        // there's no good way to test the base class BerekenVerzendingsKosten as the only available
        // constructor in Package.cs is protected
        // can only test the derived types, but leaving it commented for clarity
        //[Test]
        //public void BerekenVerzendingsKosten_Returns_Valid_Output()
        //{
        //    Package package = new TwodayPackage("", "", "", "", 10, 5, 0);

        //    string expectedOutput = "10 x 5 = $50.00";

        //    Assert.AreEqual(expectedOutput, package.BerekenVerzendingsKosten());
        //}
    }
}