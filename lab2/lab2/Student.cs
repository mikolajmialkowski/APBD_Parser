using System;
using System.Collections.Generic;

namespace lab2 {
    public class Student : IEqualityComparer<Student> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FieldOfStudy { get; set; }
        public string ModeOfStudy { get; set; }
        public string IndexNumber { get; set; }
        public string BirthDay { get; set; }
        public string Email { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }


        public bool Equals(Student x, Student y) {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.FirstName == y.FirstName && x.LastName == y.LastName && x.IndexNumber == y.IndexNumber;
        }

        public int GetHashCode(Student obj) {
            return HashCode.Combine(obj.FirstName, obj.LastName, obj.IndexNumber);
        }
    }
}