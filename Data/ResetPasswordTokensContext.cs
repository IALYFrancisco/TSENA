using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSENA.Models;

public class ResetPasswordTokensContext : DbContext
{
    public ResetPasswordTokensContext (DbContextOptions<ResetPasswordTokensContext> options)
        : base(options)
    {
    }

    public DbSet<TSENA.Models.ResetPasswordTokens> ResetPassword {get;set;} = default!;
}