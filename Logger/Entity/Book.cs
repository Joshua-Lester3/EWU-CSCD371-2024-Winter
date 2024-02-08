using System;


namespace Logger.Entity;

public record class Book(Guid Id) : BaseEntity(Id)
{
    // Id is implemented explicitly because we don't want the developers using this
    // API to confuse it with a different Id, such as ISBN

    // Name is implemented explicitly because we don't want developers using this
    // API to confuse it with a different name, such as Title
    protected override string CalculateName()
    {
        return nameof(Book);
    }
}
