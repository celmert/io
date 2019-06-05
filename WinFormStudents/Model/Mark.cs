using System.ComponentModel;

namespace WinFormStudents.Model
{
    public class Mark
    {
        [DisplayName("Id ocena")]
        public int MarkId { get; private set; }
        [DisplayName("Stopien")]
        public int MarkNumber { get; private set; }
        [DisplayName("Student")]
        public Student Student { get; private set; }
        [DisplayName("Wykładowca")]
        public Professor Professor { get; private set; }
        [DisplayName("Przedmiot")]
        public Lesson Lesson { get; private set; }

        public Mark(int markId, int markNumber, Student student, Professor professor, Lesson lesson)
        {
            this.MarkId = markId;
            this.MarkNumber = markNumber;
            this.Student = student;
            this.Professor = professor;
            this.Lesson = lesson;
        }
    }
}
