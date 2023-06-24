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
            DapperService dapperService = new DapperService(AppSetting.GetDbConnection());

            var lst = await dapperService.Query<BlogDataModel>("select * from tbl_blog with (nolock) order by Blog_Id desc", new BlogDataModel());
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
                  (@Blog_Title
                  ,@Blog_Author
                  ,@Blog_Content)", createBlog);
            Console.WriteLine("{0}", dapperCreate == 1 ? "CreateBlog Success" : "CreateBlog Fail");
            #endregion

            #region dapper getbyid
            var id = lst[0].Blog_Id;
            #endregion

            #region dapper update
            BlogDataModel updatedapper = new BlogDataModel
            {
                Blog_Title = "Test3002",
                Blog_Author = "Test3002",
                Blog_Content = "Test3002"
            };
            var update = await dapperService.Execute($@"update [dbo].[tbl_blog]
                                 set [blog_title] = '{updatedapper.Blog_Title}' 
                                  ,[blog_author] = '{updatedapper.Blog_Author}'
                                  ,[blog_content] = '{updatedapper.Blog_Content}'
                             where Blog_Id = {id} ", new { Blog_Id = id });
            Console.WriteLine("{0}", update == 1 ? "updateblog success" : "updateblog fail");
            #endregion

            #region dapper delete
            var delete = await dapperService.Execute($"delete from Tbl_blog where Blog_Id = {id}", new { Blog_Id = id });
            Console.WriteLine("{0}", delete == 1 ? "DeleteBlog Success" : "DeleteBlog Fail");
            #endregion
        }
    }
}
