using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FilterControl;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Security.Server;
namespace LightSwitchApplication
{
    public partial class ApplicationDataService
    {
        partial void FilterQuery_PreprocessQuery(string Parameter, ref IQueryable<Lab> query)
        {
            query = FilterExtensions.Filter(query, Parameter, this.Application);
        }
    }
}
