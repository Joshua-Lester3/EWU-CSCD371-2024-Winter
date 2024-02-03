namespace Logger;

public record class FullName(string firstName, string lastName, string? middleName = null)
{
    public string FirstName 
    { 
        get => _FirstName; 
        init => _FirstName = value ?? throw new ArgumentNullException(nameof(value));
    }
    private string _FirstName;
    public string LastName
    {
        get => _LastName;
        init => _LastName = value ?? throw new ArgumentNullException(nameof(value));
    }
    private string _LastName;
}