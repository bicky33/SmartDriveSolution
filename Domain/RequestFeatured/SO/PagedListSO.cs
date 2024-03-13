using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestFeatured.SO
{
    public class PagedListSO<T> : PagedList<T> where T : class
    {
        public PagedListSO(List<T> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
        {
            
        }
        public static PagedListSO<T> ToPagedList(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListSO<T>(items, count, pageNumber, pageSize);
        }
    }
}
