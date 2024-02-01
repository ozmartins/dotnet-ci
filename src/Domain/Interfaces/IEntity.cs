namespace Domain.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; }
        public Guid Version { get; }
        public bool Removed { get; }
        public void DefineId(Guid id);
        public void DefineIdAndVersion(Guid id, Guid version);
        public void Remove();
    }
}
