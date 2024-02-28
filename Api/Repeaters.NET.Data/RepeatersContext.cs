using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repeaters.NET.Data
{
    public class RepeatersContext : DbContext
    {
        public RepeatersContext(DbContextOptions<RepeatersContext> options)
            : base(options)
        {
        }
    }
}