using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaCommandLine.Model.Abstract;

namespace KretaCommandLine.Model
{
    public class Subject : ClassWithId, IEquatable<object>, IComparable, ICloneable
    {
        public virtual string SubjectName { get; set; }

        public long SubjectTypeId { get; set; }
        public virtual TypeOfSubject TypeOfSubject { get; set; }

        public Subject(long id, string subName, long subjectTypeId) : base(id)
        {
            this.Id = id;
            this.SubjectName = subName;
            SubjectTypeId = subjectTypeId;
        }

        public Subject(long id, string subName) : base(id)
        {
            this.Id = id;
            this.SubjectName = subName;
            SubjectTypeId = -1;
        }

        public Subject() : base(-1)
        {
            SubjectTypeId = -1;
            this.SubjectName = String.Empty;
        }

        public override object Clone()
        {
            return new Subject
            {
                Id = Id,
                SubjectName = SubjectName,
            };
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
            if (other == null)
                return false;
            // A két objektum a memeóriában ugyan ott van
            if (Object.ReferenceEquals(this, other))
                return true;
            // OOP: is -> a változó adott típusú-e
            if (other is Subject)
            {
                // (Subject) -> tipúskényszerítés (cast)
                Subject subjectToChek = (Subject) other;
                if (this.Id == subjectToChek.Id && this.SubjectName == subjectToChek.SubjectName)
                    return true;
            }
            return false;
        }
    }
}
