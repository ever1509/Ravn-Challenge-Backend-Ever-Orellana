using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Movies.Tests.API.Base.Seeds
{
    public class MockDbSet<TEntity> : DbSet<TEntity>
       where TEntity : class, new()
    {
        public void LoadJson(DbSet<TEntity> dbset, string file)
        {
            if (dbset.Any())
                return;

            var text = File.ReadAllText(file);
            var list = JsonConvert.DeserializeObject<List<TEntity>>(text);

            foreach (var item in list)
            {
                dbset.Add(item);
            }
        }
    }
}
