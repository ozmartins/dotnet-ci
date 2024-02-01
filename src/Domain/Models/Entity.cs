using Domain.Interfaces;

namespace Domain.Models
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

            Version = Guid.NewGuid();

            Removed = false;
        }

        protected Entity(Guid id, Guid version, bool removed)
        {
            Id = id;
            Version = version;
            Removed = removed;
        }

        public Guid Id { get; private set; }

        public Guid Version { get; private set; }

        public bool Removed { get; private set; }

        public void DefineId(Guid id)
        {
            Id = id;
        }

        public void DefineIdAndVersion(Guid id, Guid version)
        {
            Id = id;
            Version = version;
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
