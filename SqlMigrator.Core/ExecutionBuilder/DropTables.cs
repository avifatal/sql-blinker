using System.Collections.Generic;

namespace SqlMigrator.Core.ExecutionBuilder
{
    public class DropTables : DescriptiveCommand{
        public List<string> Tables{get;set;} = new List<string>();

        
    }

    public class CreateTables : DescriptiveCommand{
        public List<string> Tables{get;set;} = new List<string>();

    }


}
