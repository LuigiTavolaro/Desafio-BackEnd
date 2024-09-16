using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEndProject.Domain.Common
{
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
