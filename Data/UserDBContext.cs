using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSENA.Models;

public class UserContext: DbContext {
    public UserContext (DbContextOptions<UserContext> options)
        : base(options)
    {}

    public DbSet<TSENA.Models.User> User { get; set; } = default!;
}