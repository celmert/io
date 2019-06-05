namespace WinFormStudents.Model
{
    public class Lesson
    {
        public int LessonId { get; private set; }
        public string LessonName { get; private set; }
        public string LessonType { get; private set; }

        public Lesson(int lessonId, string lessonName, string lessonType)
        {
            this.LessonId = lessonId;
            this.LessonName = lessonName;
            this.LessonType = lessonType;
        }

        public override string ToString()
        {
            return $"{LessonName} ({LessonType})";
        }
    }
}
