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
        //https://codinghelmet.com/articles/testing-equals-and-gethashcode
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type
        // pl. 1. Történelem == 1. Történelem                
        [TestMethod()]
        public void EqualsTestSubjectsAreEquals()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");
            Subject subject2 = new Subject(1, "Történelem");

            // act
            bool actual=subject1.Equals(subject2);

            // assert
            Assert.IsTrue(actual);
        }
        // pl. 1. Történelem != 2. Matek
        [TestMethod()]
        public void EqualsTestIdAndSubjectNameDifferent()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");
            Subject subject2 = new Subject(2, "Matek");

            // act
            bool actual = subject1.Equals(subject2);

            // assert
            Assert.IsFalse(actual);
        }
        // pl. 1. Történelem != 1. Matek
        [TestMethod()]
        public void EqualsTestIdSameSubjectNameDifferent()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");
            Subject subject2 = new Subject(1, "Matek");

            // act
            bool actual = subject1.Equals(subject2);

            // assert
            Assert.IsFalse(actual);
        }
        // pl. 1. Történelem != 2. Történelem
        [TestMethod()]
        public void EqualsTestIdDifferentSubjectNameSame()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");
            Subject subject2 = new Subject(2, "Történelem");

            // act
            bool actual = subject1.Equals(subject2);

            // assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void EqualsTestOtherIsNotSubject()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");
            string subject2 = "1. Történelem";

            // act
            bool actual = subject1.Equals(subject2);

            // assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EqualsTestOtherIsNull()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");

            // act
            bool actual = subject1.Equals(null);

            // assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EqualsTestSameReference()
        {
            // arrange
            Subject subject1 = new Subject(1, "Történelem");

            // act
            bool actual = subject1.Equals(subject1);

            // assert
            Assert.IsTrue(actual);
        }

    }
}