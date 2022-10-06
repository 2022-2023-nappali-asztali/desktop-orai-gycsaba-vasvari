using KretaCommandLine.Model;
using System;
using Xunit;

namespace KretaCommandLineTestXUnit
{
    public class SubjecEqualTest
    {
        [Theory]
        [InlineData(1,"Történelem",1,"Történelem")]
        [InlineData(1, "", 1, "")]
        [InlineData(1, null, 1, null)]
        public void SubjectEqualIsTrue(long s1id, string s1name,long s2id,string s2name)
        {
            //arange
            Subject s1 = new Subject(s1id, s1name);
            Subject s2 = new Subject(s2id, s2name);
            
            //act
            bool actual = s1.Equals(s2);

            //assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(1, "Történelem", 2, "Történelem")]
        [InlineData(1, "Történelem", 2, "Matematika")]
        [InlineData(1, "Történelem", 1, "Matematika")]
        [InlineData(1, null, 1, "Matematika")]
        [InlineData(1, null, 2, null)]
        public void SubjectEqualIsFalse(long s1id, string s1name, long s2id, string s2name)
        {
            //arange
            Subject s1 = new Subject(s1id, s1name);
            Subject s2 = new Subject(s2id, s2name);

            //act
            bool actual = s1.Equals(s2);

            //assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1, "Történelem")]
        [InlineData(1, null)]
        public void SubjectEqualWithOtherObject(long s1id, string s1name)
        {
            //arange
            Subject s1 = new Subject(s1id, s1name);
            Account account = new Account();

            //act
            bool actual = s1.Equals(account);

            //assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1, "Történelem")]
        [InlineData(1, null)]
        public void SubjectEqualWithOtherObjectIsNull(long s1id, string s1name)
        {
            //arange
            Subject s1 = new Subject(s1id, s1name);

            //act
            bool actual = s1.Equals(null);

            //assert
            Assert.False(actual);
        }
    }
}
