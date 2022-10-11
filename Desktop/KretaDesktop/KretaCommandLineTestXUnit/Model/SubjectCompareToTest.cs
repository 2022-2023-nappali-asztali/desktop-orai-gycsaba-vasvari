using System;
using Xunit;

using KretaCommandLine.Model;

namespace KretaCommandLineTestXUnit.Model
{
    public class SubjectCompareToTest
    {
        [Theory]
        [InlineData(1,"Angol",1,"Angol",0)]
        [InlineData(1, "Angol", 1, "Fizika", -1)]
        [InlineData(1, "Angol", 2, "Fizika", -1)]
        [InlineData(2, "Angol", 1, "Fizika", -1)]
        [InlineData(1, "Fizika", 1, "Angol", 1)]
        [InlineData(1, "Fizika", 2, "Angol", 1)]
        [InlineData(2, "Fizika", 1, "Angol", 1)]
        public void SubjectCompareTest(long s1id,string s1name,long s2id, string s2name, int expected)
        {
            // arrange
            Subject subject = new Subject(s1id, s1name);
            Subject other = new Subject(s1id, s1name);
            // act
            int actual = subject.CompareTo(other);
            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubjectComareTestOtherIsNotSubject()
        {
            // arrange
            Subject subject = new Subject(1, "Törénelem");
            Account account = new Account();
            // act
            int actual = subject.CompareTo(account);
        }

        [Fact]
        public void SubjectComapareTestOtherIsNull()
        {
            // arrange
            Subject subject = new Subject(1, "Történelem");
            // act
            int actual = subject.CompareTo(null);

        }
    }
}
