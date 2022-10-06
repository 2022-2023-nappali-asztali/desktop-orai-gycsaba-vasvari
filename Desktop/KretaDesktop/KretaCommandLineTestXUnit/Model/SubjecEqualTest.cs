using KretaCommandLine.Model;
using System;
using Xunit;

namespace KretaCommandLineTestXUnit
{
    public class SubjecEqualTest
    {
        [Theory]
        [InlineData(1,"Történelem",1,"Történelem")]
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
    }
}
