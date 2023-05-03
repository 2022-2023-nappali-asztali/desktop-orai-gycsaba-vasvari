using KretaCommandLine.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KretaCommandLine.Comparer
{
    public class SubjectComparer : IEqualityComparer<Subject>
    {
        public bool Equals(Subject x, Subject y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id && x.SubjectName == y.SubjectName;
        }

        public int GetHashCode(Subject obj)
        {
            if (obj == null)
            {
                return 0;
            }
            int IDHashCode = obj.Id.GetHashCode();           
            int NameHashCode = obj.SubjectName == null ? 0 : obj.SubjectName.GetHashCode();
            
            return IDHashCode ^ NameHashCode;
        }
    }
}
