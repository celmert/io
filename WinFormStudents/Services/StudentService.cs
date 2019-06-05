using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using WinFormStudents.Model;

namespace WinFormStudents.Services
{
    class StudentService 
    {
        public async Task<bool> AddStudent(Student Student)
        {
           
            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("insert into Student(imie,nazwisko,NrIndeksu,SemestrId) " +
                "values(@imie,@nazwisko,@nrindeksu,@idsemestr)", conn);

                command.Parameters.AddWithValue("@imie", Student.FirstName);
                command.Parameters.AddWithValue("@nazwisko", Student.LastName);
                command.Parameters.AddWithValue("@nrindeksu", Student.IndexNumber);
                command.Parameters.AddWithValue("@idsemestr", Student.Semester.SemesterId);
                int number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }
        }

        public async Task<bool> EditStudent(Student Student)
        {
            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("update Student " +
                "set imie = @imie, nazwisko = @nazwisko, semestrId = @idsemestr, nrindeksu = @nrindeksu where studentid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", Student.StudentId);
                command.Parameters.AddWithValue("@imie", Student.FirstName);
                command.Parameters.AddWithValue("@nazwisko", Student.LastName);
                command.Parameters.AddWithValue("@nrindeksu", Student.IndexNumber);
                command.Parameters.AddWithValue("@idsemestr", Student.Semester.SemesterId);
                int number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }

        }

        public async Task<IList<Student>> GetStudents()
        {
            var list = new List<Student>();

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("select studentid, imie, nrindeksu, nazwisko, semestr.semestrid, semestr.NumerSemestru  from Student" +
                " inner join Semestr on semestr.SemestrId = Student.SemestrId order by Student.studentId", conn);

                var dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    list.Add(new Student
                        (dr.GetInt32(0), dr.GetString(2),
                        dr.GetString(1), dr.GetString(3),
                        new Semester(dr.GetInt32(4), dr.GetString(5))));
                }
            }
            return list;
        }

        public async Task<bool> RemoveStudent(Student Student)
        {
      
            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("delete from ocena" +
              " where studentid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", Student.StudentId);
                int number = await command.ExecuteNonQueryAsync();
                command = new SQLiteCommand("delete from Student" +
                " where studentid = @idstudent", conn);
                command.Parameters.AddWithValue("@idstudent", Student.StudentId);
                number = await command.ExecuteNonQueryAsync();
                return number == 1 ? true : false;
            }

        }
    }
}
