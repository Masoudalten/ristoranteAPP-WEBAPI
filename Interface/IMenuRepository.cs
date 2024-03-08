using apiTest.Model;

namespace apiTest.Interface
{
    public interface IMenuRepository
    {
        IEnumerable<Piatto> GetMenuItems();
    }
}
