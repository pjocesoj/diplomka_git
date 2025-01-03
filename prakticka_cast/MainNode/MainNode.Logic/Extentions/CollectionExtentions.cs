using MainNode.Logic.Do;
using System.Collections.Generic;
using System.Linq;

namespace MainNode.Logic.Extentions
{
    public static class CollectionExtentions
    {
        public static void CopyValues<T>(this IEnumerable<ValueDo<T>> dest, IEnumerable<T> src)
        {
            //kvůli univerzálnosti použit IEnumerable, který nemá všechny funkce jednotlivých implementací
            int count = dest.Count();
            for (int i = 0; i <count ; i++)
            {                
                dest.ElementAt(i).Value = src.ElementAt(i);
            }
        }
    }
}
