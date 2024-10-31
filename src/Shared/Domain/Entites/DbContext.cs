﻿using Microsoft.EntityFrameworkCore;
using Domain.Entites;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    
}
