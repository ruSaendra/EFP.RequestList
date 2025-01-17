﻿using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.HelperClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EFP.RequestList.Libraries
{
    public class RequestListContext: DbContext
    {
        private const string DB_FILE_NAME = "EFP.DataBase.db";

        private readonly string _dbFilePath;

        public DbSet<RequestedContent> RequestedContentSet { get; set; }
        public DbSet<Request> RequestSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<CurrencyRate> CurrencyRateSet { get; set; }
        public DbSet<Platform> PlatformSet { get; set; }
        public DbSet<User> UserSet { get; set; }
        public DbSet<UserAlias> UserAliasSet { get; set; }

        public RequestListContext()
        {
            string path = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Environment.GetEnvironmentVariable("HOME");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Environment.GetEnvironmentVariable("APPDATA");
            }

            var appName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.ModuleName);

            _dbFilePath = Path.Combine(path, appName, DB_FILE_NAME);

            if(!Directory.Exists(Path.GetDirectoryName(_dbFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(_dbFilePath));

            Database.EnsureCreated();

            //ChangeTracker.DetectedAllChanges += ChangesDetected;
        }

        private RequestListContext(string path)
        {
            _dbFilePath = path;

            if (!Directory.Exists(Path.GetDirectoryName(_dbFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(_dbFilePath));
        }

        public static RequestListContext Create(string path)
        {
            var db = new RequestListContext(path);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        public static RequestListContext Open(string path)
        {
            var db = new RequestListContext(path);
            db.Database.OpenConnection();
            return db;
        }

        public void Close() => Database.CloseConnection();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_dbFilePath}");

        /*
        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            OnDbChangesSaved?.Invoke();
            return result;
        }
        */
    }
}
