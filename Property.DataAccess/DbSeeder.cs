using Property.DataAccess.DbContext;
using Property.Domain.Entities;
using System.Linq;

namespace Property.DataAccess
{
    public static class DbSeeder
    {
        public static void Seed(SqlDbContext dbContext)
        {
            // Create default Landlords (if there are none)
            if (!dbContext.Landlords.Any())
                CreateLandlords(dbContext);
        }

        private static void CreateLandlords(SqlDbContext dbContext)
        {
            dbContext.Landlords.Add(new Landlord
            {
                LandlordName = "Saeid",
                LandlordFamily = "Ahmadi",
                MobileNo = "0912123456"
            });

            dbContext.Landlords.Add(new Landlord
            {
                LandlordName = "Vahid",
                LandlordFamily = "Naderi",
                MobileNo = "0912102030"
            });

            dbContext.Landlords.Add(new Landlord
            {
                LandlordName = "Farid",
                LandlordFamily = "Rahmati",
                MobileNo = "0912302010"
            });

            dbContext.SaveChanges();
        }
    }
}
