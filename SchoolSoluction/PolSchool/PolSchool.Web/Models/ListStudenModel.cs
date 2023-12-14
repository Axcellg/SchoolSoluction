﻿namespace PolSchool.Web.Models
{
    public class ListStudenModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CreationDate { get; set; }
        public string? EnrollmentDateDisplay { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
