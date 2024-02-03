namespace Logger;

public record class FullName
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }

    public FullName(string firstName, string lastName, string? middleName = null)
    {
        ArgumentNullException.ThrowIfNull(firstName);
        ArgumentNullException.ThrowIfNull(lastName);
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
}