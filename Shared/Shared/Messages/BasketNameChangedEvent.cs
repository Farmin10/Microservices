using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Messages
{
    public class BasketNameChangedEvent
    {
        public string  UserId { get; set; }
        public string  CourseId { get; set; }
        public string  UpdatedName { get; set; }
    }
}
