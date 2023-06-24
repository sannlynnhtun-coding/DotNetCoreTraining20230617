using DotNetCoreTraining20230617.Models;
using System.Runtime.CompilerServices;

namespace DotNetCoreTraining20230617.Mapper;

public static class ChangeModel
{
    public static BlogViewModel Change(this BlogDataModel item)
    {
        var model = new BlogViewModel
        {
            Id = item.Blog_Id,
            Title = item.Blog_Title,
            Author = item.Blog_Author,
            Content = item.Blog_Content,
        };

        return model;
    }

    public static BlogDataModel Change(this BlogViewModel item)
    {
        var model = new BlogDataModel
        {
            Blog_Id = item.Id,
            Blog_Title = item.Title,
            Blog_Author = item.Author,
            Blog_Content = item.Content,
        };

        return model;
    }
}