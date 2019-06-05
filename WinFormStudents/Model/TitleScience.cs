namespace WinFormStudents.Model
{
    public class TitleScience
    {
        public int TitleScienceId { get; private set; }
        public string Title { get; private set; }

        public TitleScience(int titlescienceid, string title)
        {
            this.TitleScienceId = titlescienceid;
            this.Title = title;
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
