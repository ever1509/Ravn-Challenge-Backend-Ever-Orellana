using System;

namespace Movies.Application.Common.Interfaces
{
    public  interface ICacheable
    {        
        string CacheKey { get; }        
    }
}
