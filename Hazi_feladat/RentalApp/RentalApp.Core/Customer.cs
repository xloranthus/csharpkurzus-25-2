
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace RentalApp.Core;

internal class Customer : ICustomer
{

    private readonly string _name = default!;
    public required string Name
    {
        get
        {
            return _name;
        }
        init
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException("Customer name cannot be null or empty.");
            }
            _name = value;
        }
    }

    private readonly string _email = default!;
    public required string Email
    {
        get
        {
            return _email;
        }
        init
        {
            if (string.IsNullOrEmpty(value) || Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") is false)
            {
                throw new InvalidOperationException("Customer email is invalid.");
            }
            _email = value;
        }
    }

    [JsonIgnore]
    public string Identifier => _email;

    private readonly string _phone = default!;
    public string Phone
    {
        get
        {
            return _phone;
        }
        init
        {
            // +1 123-456-789
            // +36 (20) 123 456
            // 06201234567
            if (string.IsNullOrEmpty(value) || Regex.IsMatch(value, @"^\+?[\d\s\-()]{7,20}$") is false)
            {
                throw new InvalidOperationException("Customer phone is invalid.");
            }
            _phone = value;
        }
    }

    
}
