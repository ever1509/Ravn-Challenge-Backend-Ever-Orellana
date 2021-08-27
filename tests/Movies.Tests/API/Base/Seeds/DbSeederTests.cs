using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Movies.Tests.API.Base.Seeds
{
    public static class DbSeederTests
    {
        public static void InitSeedDataFromTestDb(DbContext dbContext)
        {
            var properties = dbContext.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var file = $"../../../../Movies.Tests/API/Base/Jsons/{prop.Name}.json";
                if (!File.Exists(file))
                    continue;

                var entityType = prop.PropertyType.GenericTypeArguments[0];
                var mockDbSetType = typeof(MockDbSet<>);
                var mockDbSetTypeGeneric = mockDbSetType.MakeGenericType(entityType);
                var dbSet = Activator.CreateInstance(mockDbSetTypeGeneric);
                var method = mockDbSetTypeGeneric.GetMethod("LoadJson");
                method.Invoke(dbSet, new[] { prop.GetValue(dbContext), file });
                dbContext.SaveChanges();

            }
        }
    }
}
