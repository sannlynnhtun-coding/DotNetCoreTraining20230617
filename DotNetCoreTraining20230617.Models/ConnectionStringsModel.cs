﻿namespace DotNetCoreTraining20230617.Models
{
    public class ConnectionStringsModel
    {
        public string DbConnection { get; set; }
    }
    
    public enum EnumRespType
    {
    Success,
    Information,
    Warning,
    Error
    }
}