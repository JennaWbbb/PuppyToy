using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PuppyToy.Models.Storable;

namespace PuppyToy.Services.DataAccess {
    public class PuppyToyContext : DbContext {

        public DbSet<EmlalockFeedItem> EmlalockFeedItems {
            get;
            set;
        }

        public DbSet<EmlalockSession> EmlalockSessions {
            get;
            set;
        }

        public string DbPath {
            get;
        }

        public PuppyToyContext() {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath( folder );
            DbPath = Path.Join( path, "PuppyToy\\PuppyToy.sqlite" );
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            base.OnConfiguring( optionsBuilder );
            optionsBuilder.EnableDetailedErrors( true );
            optionsBuilder.EnableSensitiveDataLogging( true );
            optionsBuilder.LogTo( s => Debug.WriteLine( s ) );
            optionsBuilder.UseSqlite( $"Data Source={DbPath}" );
        }
    }
}
