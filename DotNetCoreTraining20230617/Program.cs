using DotNetCoreTraining20230617.DbService.Services;
using DotNetCoreTraining20230617.Models;
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
{
    DataSource = ".",
    InitialCatalog = "testdb",
    UserID = "sa",
    Password = "sa@123"
};


AdoDotNetService adoDotNetService = new AdoDotNetService(sqlConnectionStringBuilder);
var lst = await adoDotNetService.Query<BlogDataModel>("select * from tbl_blog with (nolock) order by Blog_Id desc");

Console.Write("Press any key to continue...");
Console.ReadKey();