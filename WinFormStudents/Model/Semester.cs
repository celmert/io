using WinFormStudents.Utils;

namespace WinFormStudents.Model
{
    public class Semester
    {
        public int SemesterId { get; private set; }
        public string SemesterName { get; private set; }

        public Semester(int semesterId, string semesterName)
        {
            this.SemesterName = semesterName;
            this.SemesterId = semesterId;
        }

        public override string ToString()
        {
            return this.SemesterName;
        }
    }
}
