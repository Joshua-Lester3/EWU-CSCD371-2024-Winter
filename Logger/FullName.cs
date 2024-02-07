namespace Logger;

// Full name record is a reference type because it can store three strings,
//     which can possibly add up to be a lot of data
// Furthermore, it is immutable because the guideline is for all value types
//     and record classes to be immutable (so their hashcode doesn't change)
public record class FullName(string FirstName, string LastName, string? MiddleName = null)
{
    public string FirstName { get; init; } = string.IsNullOrWhiteSpace(FirstName) ? 
        throw new ArgumentException($"'{nameof(FirstName)}' cannot be null or whitespace.", nameof(FirstName)) : FirstName;
    public string LastName { get; init; } = string.IsNullOrWhiteSpace(LastName) ?
        throw new ArgumentException($"'{nameof(LastName)}' cannot be null or whitespace.", nameof(LastName)) : LastName;
}