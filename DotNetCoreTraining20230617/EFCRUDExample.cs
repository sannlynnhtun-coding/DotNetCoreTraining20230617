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
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "testdb",
                UserID = "sa",
                Password = "sa@123"

                //DataSource = "DESKTOP-NKQIS3G",
                //InitialCatalog = "testdb",
                //IntegratedSecurity = true,
                //TrustServerCertificate = true


                //DataSource = ".\\SQL2022",
                //InitialCatalog = "Blog",
                //UserID = "sa",
                //Password = "sa@123",
                //TrustServerCertificate = true
            };

            AppDbContext efService = new AppDbContext(sqlConnectionStringBuilder);


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

            Console.WriteLine(JsonConvert.SerializeObject(blogData,Formatting.Indented));

            #endregion

            #region Id
            int id = blogData[0].Blog_Id;
            #endregion

            #region update
            var updateitem = await efService.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (updateitem == null)
            {
                Console.WriteLine("No data found!");
            }
            updateitem.Blog_Title = "Testing2002";
            updateitem.Blog_Author = "Testing2002";
            updateitem.Blog_Content = "Testing2002";
            efService.Entry(updateitem).State = EntityState.Modified;
            efService.Blogs.Update(updateitem);
            var result=efService.SaveChanges();
            Console.WriteLine("Message:{0}", result == 1 ? "BlogItem Update Success" : "BlogItem Update Fail");
            #endregion


            #region delete
            var deleteitem = await efService.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (deleteitem == null)
            {
                Console.WriteLine("No data found!");
            }
            efService.Entry(deleteitem).State = EntityState.Deleted;
            efService.Blogs.Remove(deleteitem);
            var deleteResult = efService.SaveChanges();
            Console.WriteLine("Message:{0}", deleteResult == 1 ? "BlogItem Delete Success" : "BlogItem Delete Fail");
            #endregion
        }
    }
}
