using System;

namespace StudentPortal.Web.Models.Entities
{
    public class Course
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
    }
}
