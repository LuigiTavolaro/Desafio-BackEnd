using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class Notification
    {
        public string Message { get; set; }
        public string PropertyName { get; set; }

        public Notification(string message, string propertyName = null)
        {
            Message = message;
            PropertyName = propertyName;
        }
    }
}
