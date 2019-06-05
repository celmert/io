using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using WinFormStudents.Model;

namespace WinFormStudents.Services
{
    class ProfessorService
    {
        public async Task<bool> Add(Professor professor)
        {

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("insert into wykladowca(imie,nazwisko,stopiennaukowyid) " +
                "values(@imie,@nazwisko,@stopiennaukowyid)", conn);

                command.Parameters.AddWithValue("@imie", professor.FirstName);
                command.Parameters.AddWithValue("@nazwisko", professor.LastName);
                command.Parameters.AddWithValue("@stopiennaukowyid", professor.TitleScience.TitleScienceId);
                int number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }
        }

        public async Task<bool> Edit(Professor professor)
        {
            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("update wykladowca " +
                "set imie = @imie, nazwisko = @nazwisko, stopiennaukowyid = @stopiennaukowyid where wykladowcaid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", professor.ProfessorId);
                command.Parameters.AddWithValue("@imie", professor.FirstName);
                command.Parameters.AddWithValue("@nazwisko", professor.LastName);
                command.Parameters.AddWithValue("@stopiennaukowyid", professor.TitleScience.TitleScienceId);
                int number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }

        }

        public async Task<IList<Professor>> GetProfessors()
        {
            var list = new List<Professor>();

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("select wykladowcaId, imie,  nazwisko, StopienNaukowy.StopienNaukowyId," +
                    "StopienNaukowy.NazwaStopnia" +
                    " from wykladowca" +
                " inner join StopienNaukowy on StopienNaukowy.StopienNaukowyId = Wykladowca.StopienNaukowyId order by wykladowcaId", conn);

                var dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    list.Add(new Professor
                        (dr.GetInt32(0), dr.GetString(1),
                        dr.GetString(2), new TitleScience(dr.GetInt32(3), dr.GetString(4))));
                }
            }
            return list;
        }

        public async Task<bool> Remove(Professor professor)
        {

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("delete from ocena" +
              " where wykladowcaid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", professor.ProfessorId);
                int number = await command.ExecuteNonQueryAsync();
                command = new SQLiteCommand("delete from wykladowca" +
                " where wykladowcaid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", professor.ProfessorId);
                number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }

        }
    }
}
