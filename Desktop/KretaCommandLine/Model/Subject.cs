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
        public Subject(long id, string subName) : base(id, subName)
        {
        }

        public int CompareTo(object obj)
        {
            return 0;
        }

        // Két tantrgyá megegyezhet: pl. 1. Történelem == 1. Történelem
        // Két tántárgy nem egyezik meg:
        // 2. a pl. 1. Történelem != 2. Matek
        // 2. b pl. 1. Történelem != 1. Matek
        // 2. c pl. 1. Történelem != 2. Történelem
        public bool Equals(Subject other)
        {
            return false;
        }
    }
}
