
namespace FCGagarin.PL.Admin.Jobs
{
    internal interface IImportRoundsJob
    {
        void Process();
    }

    public class ImportRoundsJob : IImportRoundsJob
    {
        //TODO: Kireev. inject
        private BLL.Services.Interfaces.IImportService _importService;

        public ImportRoundsJob()
        {
            //_importService = importService;
        }

        public void Process()
        {
            _importService = new BLL.Services.ImportService();
            _importService.ImportRounds(915, "2017-2018");
        }
    }
}