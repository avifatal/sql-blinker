using System.Collections.Generic;
using SqlMigrator.Core.ExecutionBuilder;

namespace SqlMigrator.Core
{
    public interface ISqlTableDiifDetectorMigrator
    {
        CreateTables detrectCreate();
        DropTables detrectDrop();

    }





}
