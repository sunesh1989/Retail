using System;
using System.Collections.Generic;
using System.Text;

namespace RetailManagementService.DataContext
{
    public interface IMongosettings
    {
        public string ProductCollectionName { get; set; }
        public string ReductionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
