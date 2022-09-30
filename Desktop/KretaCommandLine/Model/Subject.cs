using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaCommandLine.Model.Abstract;

namespace KretaCommandLine.Model
{
    public class Subject : SubjectBase, IEquatable<Subject>, IComparable
    {
        public int CompareTo(object obj)
        {
            return 0;
        }

        public bool Equals(Subject other)
        {
            return false;
        }
    }
}
