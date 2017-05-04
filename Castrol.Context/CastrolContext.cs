﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castrol.Context
{
    public class CastrolContext: DbContext
    {
        //public CastrolContext():base("name=CastrolContext")
        //{
        //    Configuration.ProxyCreationEnabled = true;
        //    Configuration.LazyLoadingEnabled = true;
        //    Database.SetInitializer(new CastrolContextInitializer());
        //    UserDataSet.LoadAsync();
        //}

        public CastrolContext(string connectionString):base(connectionString)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new CastrolContextInitializer());
            UserDataSet.LoadAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserData> UserDataSet { get; set; }

        #region Methods
        public async Task<UserData> GetUserDataAsync(string userId)
        {
            UserData userData = null;
            userData = await (UserDataSet.Where(u => u.UserId == userId).FirstOrDefaultAsync<UserData>());
            return userData;
        }

        public UserData GetUserData(string userId)
        {
            UserData userData = null;
            userData = UserDataSet.Where(u => u.UserId == userId).FirstOrDefault<UserData>();
            return userData;
        }
        #endregion
    }
}
