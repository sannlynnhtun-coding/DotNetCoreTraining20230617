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
    public class AdoDotNetCRUDExample
    {
        public static async Task RunAsync()
        {
            AdoDotNetService adoDotNetService = new AdoDotNetService(AppSetting.GetDbConnection());

            #region Create

            BlogDataModel insertModel = new BlogDataModel
            {
                Blog_Title = "Blog Insert",
                Blog_Author = "Blog Insert",
                Blog_Content = "Blog Insert"
            };

            string insertQuery = $@"INSERT INTO [dbo].[Tbl_Blog]
                  ([Blog_Title]
                  ,[Blog_Author]
                  ,[Blog_Content])
            VALUES
                  (@Blog_Title
                  ,@Blog_Author
                  ,@Blog_Content)";

            //List<SqlParameterModel> parameters = new List<SqlParameterModel>();
            //parameters.Add(new SqlParameterModel("@Blog_Title", "test"));
            //parameters.Add(new SqlParameterModel("@Blog_Author", "test"));
            //parameters.Add(new SqlParameterModel("@Blog_Content", "test"));

            SqlParameterModel[] parameters = new SqlParameterModel[]
            {
                new SqlParameterModel("@Blog_Title", "test"),
                new SqlParameterModel("@Blog_Author", "test"),
                new SqlParameterModel("@Blog_Content", "test")
            };

            var insertResult = await adoDotNetService.Execute(insertQuery, parameters);

            Console.WriteLine("Message :{0}", insertResult == 1 ? "Create Success" : "Create Fail");

            #endregion

            #region Retrieve

            string retrieveQuery = "select * from tbl_blog with (nolock) order by blog_id desc";
            var data = await adoDotNetService.Query<BlogDataModel>(retrieveQuery);
            var lstData = data.AsEnumerable().Select(x => new BlogViewModel
            {
                Id = x.Blog_Id,
                Title = x.Blog_Title,
                Author = x.Blog_Author,
                Content = x.Blog_Content,

            }).ToList();
            Console.WriteLine(JsonConvert.SerializeObject(lstData, Formatting.Indented));

            #endregion

            int id = lstData[0].Id;

            #region GetByID

            var getByIDQuery = "select * from tbl_blog where Blog_Id = @Id";
            parameters = new SqlParameterModel[]
               {
                    new SqlParameterModel("@Id",id),
               };
            var getByIdData = await adoDotNetService.Query<BlogDataModel>(getByIDQuery, parameters);
            var getByIdLstData = data.AsEnumerable().Select(x => new BlogViewModel
            {
                Id = x.Blog_Id,
                Title = x.Blog_Title,
                Author = x.Blog_Author,
                Content = x.Blog_Content,

            }).ToList();
            Console.WriteLine("Message Get By Id :{0}", JsonConvert.SerializeObject(getByIdLstData, Formatting.Indented));

            #endregion

            #region Update

            BlogDataModel updateDataModel = new BlogDataModel
            {
                Blog_Title = "blogupdated",
                Blog_Author = "blogupdated",
                Blog_Content = "blogupdated"
            };
            string updatequery = $@"update [dbo].[tbl_blog]
                                 set [blog_title] = @Blog_Title
                                  ,[blog_author] = @Blog_Title
                                  ,[blog_content] = @Blog_Title
                             where blog_id = @Id ";
            parameters = new SqlParameterModel[]
            {
                new SqlParameterModel("@Blog_Title", "test"),
                new SqlParameterModel("@Blog_Author", "test"),
                new SqlParameterModel("@Blog_Content", "test"),
                new SqlParameterModel("@Id",id)
            };
            var updateresult = await adoDotNetService.Execute(updatequery, parameters);

            Console.WriteLine("message :{0}", updateresult == 1 ? "update success" : "update fail");

            #endregion

            #region Delete

            string deleteQuery = $"Delete from tbl_blog where Blog_Id = @Id";
            parameters = new SqlParameterModel[]
              {
                    new SqlParameterModel("@Id",id),
              };
            var deleteResult = await adoDotNetService.Execute(deleteQuery, parameters);

            Console.WriteLine("Message => {0}", deleteResult == 1 ? "Delete success" : "Delete fail");

            #endregion

        }
    }
}
