using DotNetCoreTraining20230617.DbService.Services;
using DotNetCoreTraining20230617.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreTraining20230617
{
    public class EFCRUDExample
    {
        public static async Task RunAsync()
        {
            AppDbContext efService = new AppDbContext(AppSetting.GetDbConnection());

            #region Create

            BlogDataModel createModel = new BlogDataModel
            {
                Blog_Title = "EFcoreTesting",
                Blog_Author = "EFcoreTesting",
                Blog_Content = "EFcoreTesting",
            };

            await efService.AddAsync(createModel);
            var createReuslt = await efService.SaveChangesAsync();

            Console.WriteLine("Message :{0}", createReuslt == 1 ? "Create Success" : "Crate Fail");

            #endregion

            #region Retrieve

            int pageNo = 1;
            int pageSize = 5;
            int skipRowCount = (pageNo - 1) * pageSize;

            var blogData = await efService.Blogs.AsNoTracking()
                        .OrderByDescending(x => x.Blog_Id)
                        .Skip(skipRowCount)
                        .Take(pageSize).ToListAsync();

            Console.WriteLine(JsonConvert.SerializeObject(blogData, Formatting.Indented));

            #endregion

            #region Id

            int id = blogData[0].Blog_Id;

            #endregion

            #region Update

            var updateItem = await efService.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (updateItem == null)
            {
                Console.WriteLine("No data found!");
            }
            else
            {
                updateItem.Blog_Title = "Testing2002";
                updateItem.Blog_Author = "Testing2002";
                updateItem.Blog_Content = "Testing2002";
                efService.Entry(updateItem).State = EntityState.Modified;
                efService.Blogs.Update(updateItem);
                var result = efService.SaveChanges();
                Console.WriteLine("Message:{0}", result == 1 ? "BlogItem Update Success" : "BlogItem Update Fail");
            }

            #endregion

            #region Delete

            var deleteItem = await efService.Blogs
                //.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (deleteItem == null)
            {
                Console.WriteLine("No data found!");
            }
            else
            {
                //efService.Entry(deleteitem).State = EntityState.Deleted;
                efService.Blogs.Remove(deleteItem);
                var deleteResult = efService.SaveChanges();
                Console.WriteLine("Message:{0}", deleteResult == 1 ? "BlogItem Delete Success" : "BlogItem Delete Fail");
            }

            #endregion
        }
    }

    public static class AppSetting
    {
        public static AppSettingModel Setting { get; set; }
        public static SqlConnectionStringBuilder GetDbConnection()
        {
            return new SqlConnectionStringBuilder(Setting.ConnectionStrings.DbConnection);
        }
    }
}
