using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApplication.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public DateTime dateSent { get; set; }
        public NotificationType type { get; set; }
        public UserProfile sender { get; set; }
        public UserProfile recipient { get; set; }
        public string message { get; set; }
        [NotMapped]
        public Object associatedObject { get; set; }
    }

    public enum NotificationType
    {
        PositionRequested,
        PositionRequesStatusChanged,
        ApplicationSubmitted,
        ApplicationStatusChanged,
        InterviewScheduled,
        InterviewScheduleStatusChanged
    }
}

