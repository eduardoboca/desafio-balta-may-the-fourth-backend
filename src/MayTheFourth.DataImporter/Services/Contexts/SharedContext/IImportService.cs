using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayTheFourth.DataImporter.Services.Contexts.SharedContext
{
    public interface IImportService
    {
        void LoadList(string jsonList);
        Task ImportAsync(CancellationToken cancellationToken);
    }
}
