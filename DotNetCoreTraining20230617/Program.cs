using DotNetCoreTraining20230617;
using DotNetCoreTraining20230617.DbService.Services;
using DotNetCoreTraining20230617.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("appsetting.json");
AppSettingModel appSettingModel = JsonConvert.DeserializeObject<AppSettingModel>(jsonStr);
AppSetting.Setting = appSettingModel;

//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
//{
//    //DataSource = ".",
//    //InitialCatalog = "testdb",
//    //UserID = "sa",
//    //Password = "sa@123",

//};

//AdoDotNetService adoDotNetService = new AdoDotNetService(sqlConnectionStringBuilder);
//var lst = await adoDotNetService.Query<BlogDataModel>("select * from tbl_blog with (nolock) order by blog_id desc");

//AdoDotNetService adoDotNetService = new AdoDotNetService(sqlConnectionStringBuilder);
//var lst = await adoDotNetService.Query<BlogDataModel>("select * from tbl_blog with (nolock) order by blog_id desc");

//await EFCRUDExample.RunAsync();
//await AdoDotNetCRUDExample.RunAsync();
await DapperCRUDExample.RunAsync();

Console.Write("Press any key to continue...");
Console.ReadKey();