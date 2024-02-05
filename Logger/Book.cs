using System;


namespace Logger;

public record Book : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name => $"Book - {Id}";
}
