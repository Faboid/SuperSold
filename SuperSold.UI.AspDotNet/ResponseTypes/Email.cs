namespace SuperSold.UI.AspDotNet.ResponseTypes;

public class Email {
    
    public Email(string value) {
        EmailAddress = value;
    }

    public string EmailAddress { get; set; }

    public static implicit operator Email(string value) => new(value);
    
}