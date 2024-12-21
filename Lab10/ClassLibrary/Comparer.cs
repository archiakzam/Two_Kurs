using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public enum SortCriteria
    {
        Name,
        Age
    }

    public class Comparer : IComparer<Persona>
    {
        private readonly SortCriteria _criteria;

        public Comparer(SortCriteria criteria)
        {
            _criteria = criteria;
        }

        public int Compare(Persona x, Persona y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("One of the objects to compare is null.");
            }

            switch (_criteria)
            {
                case SortCriteria.Name:
                    return string.Compare(x.Name, y.Name, StringComparison.CurrentCulture);

                case SortCriteria.Age:
                    if (x.Age > y.Age)
                    {
                        return 1;
                    }
                    else if (x.Age < y.Age)
                    {
                        return -1;
                    }
                    return 0;

                

                default:
                    return 0;
            }
        }
    }
}
