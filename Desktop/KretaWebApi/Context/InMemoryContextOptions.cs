﻿using Microsoft.EntityFrameworkCore;

namespace EF.Context
{
    public static class InMemoryContextOptions
    {
        public static DbContextOptions<InMemoryContext> contextOptions = new DbContextOptionsBuilder<InMemoryContext>()
            .UseInMemoryDatabase(databaseName: "KretaTest" + Guid.NewGuid().ToString())
            .Options;
    }
}
