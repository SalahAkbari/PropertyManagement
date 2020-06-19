using Microsoft.EntityFrameworkCore;
using Property.DataAccess.DbContext;
using Property.Domain;
using System.Linq;

namespace Property.DataAccess
{
    public static class Helper
    {
        public static void DetachLocal<T>(this SqlDbContext context, T t, int entryId)
    where T : class, IIdentifier
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.PropertyID.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
