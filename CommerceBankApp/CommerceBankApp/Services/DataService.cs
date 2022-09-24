using CommerceBankApp.Data;

namespace CommerceBankApp.Services
{
    public class DataService : IDataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }



    }
}
