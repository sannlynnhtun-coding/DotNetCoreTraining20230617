using DotNetCoreTraining20230617.Models;
using Microsoft.Data.SqlClient;

namespace DotNetCoreTraining20230617
{
    public static class AppSetting
    {
        public static AppSettingModel Setting { get; set; }
        public static SqlConnectionStringBuilder GetDbConnection()
        {
            return new SqlConnectionStringBuilder(Setting.ConnectionStrings.DbConnection);
        }
    }
}
