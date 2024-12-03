using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceRatingSystem.Domain.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 500;
    private int _pageSize = 5;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ?
                maxPageSize : value;
        }
    }
    public string OrderBy { get; set; } = string.Empty;
}


