using apiTest.Model;

namespace apiTest.Interface
{
    public interface ITableRepository
    {
        IEnumerable<Table> GetTables();
        Table GetTable(int id);
        void UpdateTableStatus(int id, string status);
    }
}
