namespace FCGagarin.DAL.Entities.Abstract
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}