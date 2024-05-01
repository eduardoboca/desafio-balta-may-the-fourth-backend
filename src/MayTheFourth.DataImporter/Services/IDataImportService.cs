using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayTheFourth.DataImporter.Services
{
    public interface IDataImportService
    {
        public Task ImportDataAsync(CancellationToken cancellationToken);
    }
}
