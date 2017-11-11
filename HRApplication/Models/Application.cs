﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class Application
    {
        public string ID { get; set; }
        public string ApplicantID { get; set; }
        public string PositionID { get; set; }
        public string ResumePath { get; set; }
        public string CoverLetterPath { get; set; }
        public string Experience { get; set; }
        
        public ApplicationStatus Status { get; set; }
    }

    public enum ApplicationStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
