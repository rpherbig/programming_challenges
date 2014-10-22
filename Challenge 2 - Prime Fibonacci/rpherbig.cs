// Copyright: Robert P Herbig, 2010

using System;
using NUnit.Framework;

namespace Herbig.ProgrammingChallenge
{
    [TestFixture]
    public class PrimeFibonacci
    {
        private static readonly double Phi = (1 + Math.Sqrt( 5 )) / 2;

        #region Tests

        [Test]
        public void ShouldCalculateNthFibonacci()
        {
            Assert.AreEqual( 0, CalculateNthFibonacci( 0 ) );
            Assert.AreEqual( 1, CalculateNthFibonacci( 1 ) );
            Assert.AreEqual( 1, CalculateNthFibonacci( 2 ) );
            Assert.AreEqual( 2, CalculateNthFibonacci( 3 ) );
            Assert.AreEqual( 3, CalculateNthFibonacci( 4 ) );
            Assert.AreEqual( 5, CalculateNthFibonacci( 5 ) );
            Assert.AreEqual( 8, CalculateNthFibonacci( 6 ) );
        }

        [Test]
        public void ShouldFindFibonacciGreaterThanN()
        {
            Assert.AreEqual( 1, FindFibonacciPrimeGreaterThanN( 0 ) );
            Assert.AreEqual( 2, FindFibonacciPrimeGreaterThanN( 1 ) );
            Assert.AreEqual( 3, FindFibonacciPrimeGreaterThanN( 2 ) );
            Assert.AreEqual( 5, FindFibonacciPrimeGreaterThanN( 3 ) );
            Assert.AreEqual( 5, FindFibonacciPrimeGreaterThanN( 4 ) );
            Assert.AreEqual( 13, FindFibonacciPrimeGreaterThanN( 5 ) );
            Assert.AreEqual( 13, FindFibonacciPrimeGreaterThanN( 10 ) );
            Assert.AreEqual( 89, FindFibonacciPrimeGreaterThanN( 13 ) );
        }

        [Test]
        public void ShouldDetermineIfPrime()
        {
            Assert.IsTrue( IsPrime( 1 ) );
            Assert.IsTrue( IsPrime( 2 ) );
            Assert.IsTrue( IsPrime( 3 ) );
            Assert.IsTrue( IsPrime( 5 ) );
            Assert.IsTrue( IsPrime( 13 ) );
            Assert.IsTrue( IsPrime( 37 ) );
            Assert.IsTrue( IsPrime( 401 ) );

            Assert.IsFalse( IsPrime( 4 ) );
            Assert.IsFalse( IsPrime( 10 ) );
            Assert.IsFalse( IsPrime( 33 ) );
            Assert.IsFalse( IsPrime( 400 ) );
        }

        [Test]
        public void ShouldSolveForALargeNumber()
        {
            Assert.AreEqual( 514229, FindFibonacciPrimeGreaterThanN( 227000 ) );
        }

        #endregion

        #region Helper Methods

        private static int FindFibonacciPrimeGreaterThanN( int n )
        {
            for ( int i = 0; ; i++ )
            {
                var fibo = CalculateNthFibonacci( i );
                if ( fibo > n && IsPrime( fibo ) )
                {
                    return fibo;
                }
            }
        }

        private static bool IsPrime( int n )
        {
            // Take care of the easy case: 2 is the only even prime
            if ( n % 2 == 0 )
            {
                return n == 2;
            }

            // http://en.wikipedia.org/wiki/Prime_number#Verifying_primality
            var endPoint = Math.Sqrt( n );
            // Increment by two since we already know to skip even numbers
            for ( int i = 3; i < endPoint; i += 2 )
            {
                var divisionResult = n / ((double)i);
                // If any of the divisions result in an integer, then the original number is not a prime.
                // Otherwise, it is a prime.
                if ( divisionResult == (int)divisionResult )
                {
                    return false;
                }
            }

            return true;
        }

        private static int CalculateNthFibonacci( int n )
        {
            // http://en.wikipedia.org/wiki/Fibonacci_number#Relation_to_the_golden_ratio
            double numer = Math.Pow( Phi, n ) - Math.Pow( (-1 / Phi), n );
            double denom = Math.Sqrt( 5 );
            return (int)(numer / denom);
        }

        #endregion
    }
}
