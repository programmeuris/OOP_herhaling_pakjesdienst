using NUnit.Framework;
using Herhaling_Pakjesdienst.Models;
using System;

namespace Herhaling_Pakjesdienst.Tests
{
    [TestFixture]
    [SetCulture("en-US")] // all tests run on the same culture
    public class OvernightPackageTests
    {
        [Test]
        public void Throws_ValueBelowZeroException_When_ToeslagPerKg_LessThan_Zero()
        {
            // delegates zijn pas iets voor in jaar 2
            Assert.Throws<ValueBelowZeroException>(delegate 
            {
               OvernightPackage _ = new OvernightPackage("", "", "", "", 0, 0, -1);
            });
        }

        [TestCase(420, 69, 10)]
        [TestCase(12, 2.23, 10)]
        [TestCase(12.34, 2, 10)]
        public void VerzendingsKosten_Returns_Valid_Output(double weight, double priceKg, double toeslagKg)
        {
            OvernightPackage package = new OvernightPackage("", "", "", "", weight, priceKg, toeslagKg);

            // check PackageTests.cs for explanation
            Assert.IsTrue(Math.Abs(package.VerzendingsKosten() - ((toeslagKg + priceKg) * weight)) < 0.001);
        }

        [Test]
        public void ToeslagPerKg_Rounds_To_2_Decimal_Places()
        {
            OvernightPackage package = new OvernightPackage("", "", "", "", 0, 0, 69.423);

            Assert.AreEqual("69.42", package.ToeslagPerKg.ToString());
        }

        [Test]
        public void BerekenVerzendingsKosten_Returns_Valid_Output()
        {
            OvernightPackage package = new OvernightPackage("", "", "", "", 10, 5, 6);

            string expectedOutput = "( 6 + 5 ) x 10 = $110.00";

            Assert.AreEqual(expectedOutput, package.BerekenVerzendingsKosten());
        }
    }
}