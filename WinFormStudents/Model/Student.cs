using System.ComponentModel;

namespace WinFormStudents.Model
{
    public class Student
    {
        [DisplayName("ID studenta")]
        public int StudentId { get; private set; }
        [DisplayName("Nr indeksu")]
        public string IndexNumber { get; private set; }
        [DisplayName("Imie")]
        public string FirstName { get; private set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; private set; }
        [DisplayName("Semestr")]
        public Semester Semester { get; private set; }

        public Student(int studentId, string indexNumber, string firstName, string lastName, Semester semester)
        {
            this.IndexNumber = indexNumber;
            this.StudentId = studentId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Semester = semester;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
