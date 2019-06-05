using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormStudents.Model;

namespace WinFormStudents.Services
{
    class MarkService
    {
        public async Task<bool> Add(Mark mark)
        {

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("insert into ocena(stopien,przedmiotid,studentid,wykladowcaid) " +
                "values(@stopien,@przedmiotid,@studentid,@wykladowcaid)", conn);

                command.Parameters.AddWithValue("@stopien", mark.MarkNumber);
                command.Parameters.AddWithValue("@przedmiotid", mark.Lesson.LessonId);
                command.Parameters.AddWithValue("@studentid", mark.Student.StudentId);
                command.Parameters.AddWithValue("@wykladowcaid", mark.Professor.ProfessorId);
                int number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }
        }

        public async Task<bool> Edit(Mark mark)
        {
            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("update ocena " +
                "set stopien = @stopien, przedmiotId = @przedmiotid, studentid = @studentId, wykladowcaid = @wykladowcaid where ocenaId = @ocenaid", conn);
                command.Parameters.AddWithValue("@ocenaId", mark.MarkId);
                command.Parameters.AddWithValue("@stopien", mark.MarkNumber);
                command.Parameters.AddWithValue("@przedmiotid", mark.Lesson.LessonId);
                command.Parameters.AddWithValue("@studentid", mark.Student.StudentId);
                command.Parameters.AddWithValue("@wykladowcaid", mark.Professor.ProfessorId);
                int number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }

        }

        public async Task<IList<Mark>> GetMarks()
        {
            var list = new List<Mark>();

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("select ocenaid, stopien, przedmiot.przedmiotId, nazwaprzedmiotu,student.studentId,student.imie,student.Nazwisko, "+
                   "wykladowca.WykladowcaId, wykladowca.Imie, wykladowca.Nazwisko, nazwatypu from ocena inner join przedmiot on ocena.przedmiotid = przedmiot.przedmiotId "+
                    "inner join wykladowca on wykladowca.wykladowcaId = ocena.wykladowcaId inner join student on student.StudentId = ocena.StudentId" +
                    " inner join TypZajec on przedmiot.typzajecid  = typzajec.typzajecid", conn);

                var dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    list.Add(new Mark(dr.GetInt32(0), dr.GetInt32(1), new Student(dr.GetInt32(4), "", dr.GetString(5), dr.GetString(6), null),
                        new Professor(dr.GetInt32(7), dr.GetString(8), dr.GetString(9),null), new Lesson(dr.GetInt32(2), dr.GetString(3), dr.GetString(10))));
                }
            }
            return list;
        }

        public async Task<bool> Remove(Mark makr)
        {

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("delete from ocena" +
              " where ocenaid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", makr.MarkId);
                int number = await command.ExecuteNonQueryAsync();
               
                return number == 1 ? true : false;
            }

        }
    }
}
