// Copyright: Robert P Herbig, 2010

using System.Collections.Generic;
using NUnit.Framework;

namespace Herbig.ProgrammingChallenge
{
    [TestFixture]
    public class Palindrome
    {
        private const string ChallengeText =
                    "FourscoreandsevenyearsagoourfaathersbroughtforthonthiscontainentanewnationconceivedinzLibertyanddedicatedtothepropositionthatallmenarecreatedequalNowweareengagedinagreahtcivilwartestingwhetherthatnaptionoranynartionsoconceivedandsodedicatedcanlongendureWeareqmetonagreatbattlefiemldoftzhatwarWehavecometodedicpateaportionofthatfieldasafinalrestingplaceforthosewhoheregavetheirlivesthatthatnationmightliveItisaltogetherfangandproperthatweshoulddothisButinalargersensewecannotdedicatewecannotconsecratewecannothallowthisgroundThebravelmenlivinganddeadwhostruggledherehaveconsecrateditfaraboveourpoorponwertoaddordetractTgheworldadswfilllittlenotlenorlongrememberwhatwesayherebutitcanneverforgetwhattheydidhereItisforusthelivingrathertobededicatedheretotheulnfinishedworkwhichtheywhofoughtherehavethusfarsonoblyadvancedItisratherforustobeherededicatedtothegreattdafskremainingbeforeusthatfromthesehonoreddeadwetakeincreaseddevotiontothatcauseforwhichtheygavethelastpfullmeasureofdevotionthatweherehighlyresolvethatthesedeadshallnothavediedinvainthatthisnationunsderGodshallhaveanewbirthoffreedomandthatgovernmentofthepeoplebythepeopleforthepeopleshallnotperishfromtheearth";
        
        #region Tests

        [Test]
        public void ShouldCheckEven()
        {
            const string test = "sccabbaddt";

            Assert.AreEqual( "abba", CheckEven( test, 4, 5 ) );
        }

        [Test]
        public void ShouldCheckOdd()
        {
            const string test = "sccabebaddt";

            Assert.AreEqual( "abeba", CheckOdd( test, 5 ) );
        }

        [Test]
        public void ShouldFindLargestPalindromeInSmallString()
        {
            const string test = "sccabbaddt";

            Assert.AreEqual( "abba", FindLargestPalindrome( test ) );
        }

        [Test]
        public void ShouldFindLargestPalindromeInMediumString()
        {
            const string test =
                        "FourscoreandsevenyearsagoourfaathersbroughtforthonthiscontainentanewnationconceivedinzLibertyanddedicatedtothepropositionthatallmenarecreatedequalNowweareengagedinagreahtcivilwartestingwhetherthatnapti";

            Assert.AreEqual( "ivi", FindLargestPalindrome( test ) );
        }

        [Test]
        public void ShouldSolveTheActualChallenge()
        {
            Assert.AreEqual( "ranynar", FindLargestPalindrome( ChallengeText ) );
        }

        #endregion

        #region Helper Methods

        private static string FindLargestPalindrome( string s )
        {
            var largest = "";

            for ( int index = 0; index < s.Length - 1; index++ )
            {
                largest = FindLongestString( largest, s, index );
            }

            return largest;
        }

        private static string FindLongestString( string largest, string text, int index )
        {
            // Check for even or odd pattern palindromes
            var even = CheckEven( text, index, index + 1 );
            var odd = CheckOdd( text, index );

            // Return whichever was the longest
            var l = new List<string> { largest, even, odd };
            l.Sort( ( x, y ) => y.Length.CompareTo( x.Length ) );
            return l[0];
        }

        private static string CheckOdd( string s, int index )
        {
            if ( index > 0 && index < s.Length && s[index - 1] == s[index + 1] )
            {
                // odd: "aba"
                return CheckEven( s, index - 1, index + 1 );
            }
            else
            {
                return "";
            }
        }

        private static string CheckEven( string s, int left, int right )
        {
            if ( left > 0 && right < s.Length && s[left] == s[right] )
            {
                // even: "aa"
                return CheckEven( s, left - 1, right + 1 );
            }
            else
            {
                // Go back to the last known good string
                return s.Substring( left + 1, right - left - 1 );
            }
        }

        #endregion
    }
}
