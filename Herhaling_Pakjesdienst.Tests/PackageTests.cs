using NUnit.Framework;
using Herhaling_Pakjesdienst.Models;
using System;

namespace Herhaling_Pakjesdienst.Tests
{
    // makes all tests run on the same culture so $ is $ and 0,34 turns into 0.34 etc
    // setting this here only affects the values in this class, not in the main program
    [TestFixture]
    [SetCulture("en-US")]
    public class PackageTests
    {
        // NOTE: Unit tests are a lot of code, but lots of it is very similar and can easily be copied over

        // cannot create a base package since its only constructor is protected
        // to get around this I create a TwoDayPackage and cast that to its base class
        // note that any overridden methods pr properties will still use the derived implementation
        // this means that this hack only works when you want to test things that are not overridden in the
        // subclass you're casting from

        [Test]
        public void Throws_ValueBelowZeroException_When_Gewicht_LessThan_Zero()
        {
            // assert
            // delegates are hard, not even sure I understand them completely, you won't use them this year
            // but if you ever want to test if something throws an exception before then
            //      just copy this, change the exception in <> to whatever you want to test for
            //      and put whatever code that will throw the exception in the [delegate {here}]
            // more info: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
            Assert.Throws<ValueBelowZeroException>(delegate 
            { 
                Package _ = new TwodayPackage("", "", "", "", -1, 0, 0); 
            });
        }

        [Test]
        public void ThrowsValueBelowZeroException_When_PrijsPerKg_LessThan_Zero()
        {
            // assert
            Assert.Throws<ValueBelowZeroException>(delegate
            { 
                Package _ = new TwodayPackage("", "", "", "", 0, -1, 0); 
            });
        }

        // TestCase is a way to run a single test multiple times with different inputs
        // more info: https://docs.nunit.org/articles/nunit/writing-tests/attributes/testcase.html
        [TestCase(420, 69)]
        [TestCase(12, 2.23)]
        [TestCase(12.34, 2)]
        public void VerzendingsKosten_Returns_Valid_Output(double weight,
                                                           double priceKg)
        {
            // arrange
            Package package = new TwodayPackage("", "", "", "", weight, priceKg, 0);

            // assert
            // to prevent weird bugs, floats and doubles shouldn't be directly compared with eachother
            // if the absolute difference between the expected value and the actual value is smaller
            // than the neccesary precision (2 digits in this case), both numbers are equal
            Assert.IsTrue(Math.Abs(package.VerzendingsKosten() - (weight * priceKg)) < 0.001);

            // possible result when comparing floating point values (eg doubles, floats) the normal way:
            // Assert.AreEqual(weight * priceKg, package.VerzendingsKosten());
            //      Expected: 26.759999999999998d
            //      But was:  26.760000000000002d
        }

        [Test]
        public void ToString_Returns_Valid_Output()
        {
            // arrange
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

            // assert
            Assert.AreEqual(expectedOutput, package.ToString());
        }

        [Test]
        public void Gewicht_Rounds_To_2_Decimal_Places()
        {
            // arrange
            Package package = new TwodayPackage("", "", "", "", 69.423, 0, 0);

            // assert
            Assert.AreEqual("69.42", package.Gewicht.ToString());
        }

        // there's no good way to test the base class BerekenVerzendingsKosten as the only available
        // constructor in Package.cs is protected
        // derived types are tested in their respective test classes, but I'll leave this commented for clarity
        //[Test]
        //public void BerekenVerzendingsKosten_Returns_Valid_Output()
        //{
        //    Package package = new TwodayPackage("", "", "", "", 10, 5, 0);

        //    string expectedOutput = "10 x 5 = $50.00";

        //    Assert.AreEqual(expectedOutput, package.BerekenVerzendingsKosten());
        //}
    }
}