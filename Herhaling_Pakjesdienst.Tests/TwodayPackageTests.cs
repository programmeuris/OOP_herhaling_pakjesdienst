using NUnit.Framework;
using Herhaling_Pakjesdienst.Models;
using System;

namespace Herhaling_Pakjesdienst.Tests
{
    // most advanced stuff we didn't see yet has info and links in PackageTests.cs

    [TestFixture]
    [SetCulture("en-US")]
    public class TwodayPackageTests
    {
        [Test]
        public void Throws_ValueBelowZeroException_When_VasteKost_LessThan_Zero()
        {
            // assert
            Assert.Throws<ValueBelowZeroException>(delegate 
            { 
               TwodayPackage _ = new TwodayPackage("", "", "", "", 0, 0, -1);
            });
        }

        [TestCase(420, 69, 10)]
        [TestCase(12, 2.23, 10)]
        [TestCase(12.34, 2, 10)]
        public void VerzendingsKosten_Returns_Valid_Output(double weight, double priceKg, double vasteKost)
        {
            // arrange
            TwodayPackage package = new TwodayPackage("", "", "", "", weight, priceKg, vasteKost);

            // assert
            Assert.IsTrue(Math.Abs(package.VerzendingsKosten() - (weight * priceKg + vasteKost)) < 0.001);
        }

        [Test]
        public void VasteKost_Rounds_To_2_Decimal_Places()
        {
            // arrange
            TwodayPackage package = new TwodayPackage("", "", "", "", 0, 0, 69.423);

            // assert
            Assert.AreEqual("69.42", package.VasteKost.ToString());
        }

        [Test]
        public void BerekenVerzendingsKosten_Returns_Valid_Output()
        {
            // arrange
            TwodayPackage package = new TwodayPackage("", "", "", "", 10, 5, 6);

            string expectedOutput = "10 x 5 + 6 = $56.00";

            // assert
            Assert.AreEqual(expectedOutput, package.BerekenVerzendingsKosten());
        }
    }
}