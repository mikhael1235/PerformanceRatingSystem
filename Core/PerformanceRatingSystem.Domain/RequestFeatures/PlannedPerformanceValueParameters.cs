using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceRatingSystem.Domain.RequestFeatures;

public class PlannedPerformanceValueParameters : RequestParameters
{
    public string SearchQuarter { get; set; } = string.Empty;
    public string SearchYear { get; set; } = string.Empty;
    public PlannedPerformanceValueParameters()
    {
        OrderBy = "value desc";
    }
}
