using System.ComponentModel;

namespace WinFormStudents.Model
{
    public class Professor
    {
        [DisplayName("ID profesora")]
        public int ProfessorId { get; private set; }
     
        [DisplayName("Imie")]
        public string FirstName { get; private set; }
        [DisplayName("Nazwisko")]
        public string LastName { get; private set; }
        [DisplayName("Tytuł naukowy")]
        public TitleScience TitleScience { get; private set; }

        public Professor(int professorId, string firstName, string lastName, TitleScience titleScience)
        {

            this.ProfessorId = professorId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.TitleScience = titleScience;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}"; 
        }

    }
}
