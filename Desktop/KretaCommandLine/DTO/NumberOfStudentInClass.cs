using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.DTO
{
    public class NumberOfStudentInClass
    {
        public int SchoolYear { get; set; }
        public char ClassType { get; set; }
        public int NumberOfStudent { get; set; }

        public NumberOfStudentInClass()
        {
            SchoolYear = -1;
            ClassType = '0';
            NumberOfStudent = -1;
        }

        public NumberOfStudentInClass(int schoolYear, char classType, int numberOfStudent)
        {
            SchoolYear = schoolYear;
            ClassType = classType;
            NumberOfStudent = numberOfStudent;
        }

        public override string ToString()
        {
            return $"{SchoolYear}.{ClassType} osztály, {NumberOfStudent} fő";
        }
    }
}
