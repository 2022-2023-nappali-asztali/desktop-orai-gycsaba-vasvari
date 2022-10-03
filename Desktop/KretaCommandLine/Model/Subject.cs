using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaCommandLine.Model.Abstract;

namespace KretaCommandLine.Model
{
    public class Subject : SubjectBase, IEquatable<object>, IComparable
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
        // this.Equals(other)
        public bool Equals(object other)
        {
            // OOP: is -> a változó adott típusú-e
            if (other is Subject)
            {
                // (Subject) -> tipúskényszerítés (cast)
                Subject subjectToChek = (Subject)other;
                if (this.Id == subjectToChek.Id && this.SubjectName == subjectToChek.SubjectName)
                    return true;
            }
            return false;

        }
    }
}
