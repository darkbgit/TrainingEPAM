namespace CsvManager.DAL.Core.Entities
{
    public interface IEntityWithName : IBaseEntity
    {
        public string Name { get; set; }
    }
}