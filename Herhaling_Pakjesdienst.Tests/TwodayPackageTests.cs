using NUnit.Framework;
using Herhaling_Pakjesdienst.Models;

namespace Herhaling_Pakjesdienst.Tests
{
    [TestFixture]
    [SetCulture("en-US")]
    public class TwodayPackageTests
    {
        [Test]
        public void Throws_ValueBelowZeroException_When_VasteKost_LessThan_Zero()
        {
            // delegates zijn pas iets voor in jaar 2
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
            TwodayPackage package = new TwodayPackage("", "", "", "", weight, priceKg, vasteKost);

            Assert.AreEqual(weight * priceKg + vasteKost, package.VerzendingsKosten());
        }

        [Test]
        public void VasteKost_Rounds_To_2_Decimal_Places()
        {
            TwodayPackage package = new TwodayPackage("", "", "", "", 0, 0, 69.423);

            Assert.AreEqual("69.42", package.VasteKost.ToString());
        }

        [Test]
        public void BerekenVerzendingsKosten_Returns_Valid_Output()
        {
            Package package = new TwodayPackage("", "", "", "", 10, 5, 5);

            string expectedOutput = "10 x 5 + 5 = $55.00";

            Assert.AreEqual(expectedOutput, package.BerekenVerzendingsKosten());
        }
    }
}