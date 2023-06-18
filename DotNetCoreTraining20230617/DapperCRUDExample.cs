using DotNetCoreTraining20230617.DbService.Services;
using DotNetCoreTraining20230617.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreTraining20230617
{
    public class DapperCRUDExample
    {
        public static async Task RunAsync()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                //DataSource = ".",
                //InitialCatalog = "testdb",
                //UserID = "sa",
                //Password = "sa@123"

                DataSource = ".\\SQL2022",
                InitialCatalog = "Blog",
                UserID = "sa",
                Password = "sa@123",
                TrustServerCertificate = true
            };
            //List<BlogDataModel> lstData = new List<BlogDataModel>();
            BlogDataModel lstData = new BlogDataModel();
            DapperService dapperService = new DapperService(sqlConnectionStringBuilder);
            var lst = await dapperService.Query<BlogDataModel>("select * from tbl_blog with (nolock) order by Blog_Id desc", lstData);
            var jsonstr = JsonConvert.SerializeObject(lst, Formatting.Indented);
            Console.WriteLine(jsonstr);

            #region Dapper create
            BlogDataModel createBlog = new BlogDataModel
            {
                Blog_Title = "Test4",
                Blog_Author = "Test4",
                Blog_Content = "Test4"
            };
            var dapperCreate = await dapperService.Execute($@"INSERT INTO [dbo].[Tbl_Blog]
                  ([Blog_Title]
                  ,[Blog_Author]
                  ,[Blog_Content])
            VALUES
                  ('{createBlog.Blog_Title}'
                  ,'{createBlog.Blog_Author}'
                  ,'{createBlog.Blog_Content}')", lstData);
            Console.WriteLine("{0}", dapperCreate == 1 ? "CreateBlog Success" : "CreateBlog Fail");
            #endregion

            #region Dapper GetByID
            var id = lst[0].Blog_Id;
            #endregion

            #region Dapper Update
            BlogDataModel updateDapper = new BlogDataModel
            {
                Blog_Title = "Test5",
                Blog_Author = "Test5",
                Blog_Content = "Test5"
            };
            var update = await dapperService.Execute($@"UPDATE [dbo].[Tbl_Blog]
                                 SET [Blog_Title] = '{updateDapper.Blog_Title}' 
                                  ,[Blog_Author] = '{updateDapper.Blog_Author}'
                                  ,[Blog_Content] = '{updateDapper.Blog_Content}'
                             WHERE Blog_Id = {id} ", lstData);
            Console.WriteLine("{0}", update == 1 ? "UpdateBlog Success" : "UpdateBlog Fail");
            #endregion

            #region dapper delete
            var delete = await dapperService.Execute($"delete from Tbl_blog where Blog_Id = {id}", lstData);
            Console.WriteLine("{0}", delete == 1 ? "DeleteBlog Success" : "DeleteBlog Fail");
            #endregion
        }
    }
}
