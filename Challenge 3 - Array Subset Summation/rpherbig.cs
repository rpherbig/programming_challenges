// Copyright: Robert P Herbig, 2010

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Herbig.ProgrammingChallenge
{
    [TestFixture]
    public class ArraySubsetSummation
    {
        private static readonly List<int> ChallengeList = new List<int> {
            3, 4, 9, 14, 15, 19, 28, 37, 47, 50, 54, 56, 59, 61, 70, 73, 78, 81, 92, 95, 97, 99 };

        #region Tests

        [Test]
        public void ShouldConvertToBitArray()
        {
            var expected = new List<bool>
                               {
                                   false, // Padding bit
                                   true,
                                   false,
                                   true,
                                   false,
                                   true,
                                   false,
                               };

            // decimal 42 => binary 101010
            var results = ConvertToBitArray( 42, 7 );

            Assert.AreEqual( expected.Count, results.Count, "different count" );
            for ( int i = 0; i < results.Count; i++ )
            {
                Assert.AreEqual( expected[i], results[i], "different value" );
            }
        }

        [Test]
        public void ShouldFindMax()
        {
            // decimal 10 => binary 1010
            var bitArray = ConvertToBitArray( 10, 4 );

            var result = FindAndRemoveMax( bitArray, new List<int> { 1, 2, 3, 4 } );

            Assert.AreEqual( 3, result );
        }

        [Test]
        public void ShouldSum()
        {
            // decimal 10 => binary 1010
            var bitArray = ConvertToBitArray( 10, 4 );

            var result = Sum( bitArray, new List<int> { 1, 2, 3, 4 } );

            Assert.AreEqual( 4, result );
        }

        [Test]
        public void ShouldSolveSmallChallenge()
        {
            var smallList = new List<int> { 1, 2, 3, 4, 6 };
            Assert.AreEqual( 4, Solve( smallList ) );
        }

        [Test]
        public void ShouldSolveTheActualChallenge()
        {
            Assert.AreEqual( 179, Solve( ChallengeList ) );
        }

        #endregion

        #region Helper Methods

        private static BitArray ConvertToBitArray( int toConvert, int totalSize )
        {
            var binaryString = Convert.ToString( toConvert, 2 ).PadLeft( totalSize, '0' );
            var bitArray = new BitArray( binaryString.Length );
            for ( int i = 0; i < bitArray.Length; i++ )
            {
                bitArray[i] = (binaryString[i] == '1');
            }
            return bitArray;
        }

        private static int FindAndRemoveMax( BitArray bitArray, List<int> list )
        {
            // Assumes list is sorted ascending, so the max is the last true bit
            var max = 0;
            for ( int i = bitArray.Length - 1; i >= 0; i-- )
            {
                if ( bitArray[i] )
                {
                    max = list[i];
                    // Exclude the max item from further calculations
                    bitArray[i] = false;
                    break;
                }
            }
            return max;
        }

        private static int Sum( BitArray bitArray, List<int> list )
        {
            var sum = 0;
            for ( int l = 0; l < bitArray.Length; l++ )
            {
                if ( bitArray[l] )
                {
                    sum += list[l];
                }
            }
            return sum;
        }

        private static int Solve( List<int> list )
        {
            // http://en.wikipedia.org/wiki/Power_set#Representing_subsets_as_functions
            var count = 0;
            var power = Math.Pow( 2, list.Count );
            for ( int i = 1; i < power; i++ )
            {
                var bitArray = ConvertToBitArray( i, list.Count );

                var max = FindAndRemoveMax( bitArray, list );

                var sum = Sum( bitArray, list );

                if ( max == sum )
                {
                    count++;
                }
            }
            return count;
        }

        #endregion
    }
}
