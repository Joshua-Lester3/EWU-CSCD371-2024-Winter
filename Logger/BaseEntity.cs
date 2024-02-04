namespace Logger;

    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public abstract string Name { get; }

        string IEntity.Name => Name;
    }
