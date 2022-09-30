using Microsoft.VisualStudio.TestTools.UnitTesting;
using KretaCommandLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Tests
{
    [TestClass()]
    public class SubjectTests
    {
        [TestMethod()]
        public void EqualsTestSubjectsAreEquals()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");
            Subject subject2 = new Subject(2, "Történelem");

            // act

            // assert
        }
    }
}