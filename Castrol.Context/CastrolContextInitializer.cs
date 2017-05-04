using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castrol.Context
{
    public class CastrolContextInitializer: CreateDatabaseIfNotExists<CastrolContext>
    {
        protected override void Seed(CastrolContext context)
        {

            IList<UserData> userDataList = new List<UserData>();

            userDataList.Add(new UserData { UserId = "1984", UserName="abdallah", UserPassword= "123456" });
            userDataList.Add(new UserData { UserId = "4891", UserName = "AutofitGentz229", UserPassword = "Kll08#0a" });

            context.UserDataSet.AddRange(userDataList);

            base.Seed(context);
        }
    }
}
