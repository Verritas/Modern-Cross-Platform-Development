using System;
using Xunit;

namespace PrimeFactorsTests
{
    public class PrimeFactorsTests
    {
        [Fact]
        public void PrimeFactors4()
        {
            //arrange
            int number = 4;
            string expected = "2 x 2";

            //act
            var calc = PrimeFactorsLib.PrimeFactors.GetPrimeFactors(number);

            //assert
            Assert.Equal(expected, calc);
        }

        [Fact]
        public void PrimeFactors7()
        {
            //arrange
            int number = 7;
            string expected = "7";

            //act
            var calc = PrimeFactorsLib.PrimeFactors.GetPrimeFactors(number);

            //assert
            Assert.Equal(expected, calc);
        }

        [Fact]
        public void PrimeFactors30()
        {
            //arrange
            int number = 30;
            string expected = "5 x 3 x 2";

            //act
            var calc = PrimeFactorsLib.PrimeFactors.GetPrimeFactors(number);

            //assert
            Assert.Equal(expected, calc);
        }

        [Fact]
        public void PrimeFactors40()
        {
            //arrange
            int number = 40;
            string expected = "5 x 2 x 2 x 2";

            //act
            var calc = PrimeFactorsLib.PrimeFactors.GetPrimeFactors(number);

            //assert
            Assert.Equal(expected, calc);
        }

        [Fact]
        public void PrimeFactors50()
        {
            //arrange
            int number = 50;
            string expected = "5 x 5 x 2";

            //act
            var calc = PrimeFactorsLib.PrimeFactors.GetPrimeFactors(number);

            //assert
            Assert.Equal(expected, calc);
        }
    }
}
