﻿namespace MinimalApiAndWasm.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{

}
