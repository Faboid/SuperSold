using System.Runtime.Serialization;

namespace SuperSold.UI.AspDotNet.Exceptions;

public class SecretsConfigurationException : ConfigurationException {
    public SecretsConfigurationException() {}
    public SecretsConfigurationException(string? message) : base(message) {}
    public SecretsConfigurationException(string? message, Exception? innerException) : base(message, innerException) {}
    protected SecretsConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) {}
}
