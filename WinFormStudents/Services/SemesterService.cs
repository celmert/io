using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using WinFormStudents.Model;

namespace WinFormStudents.Services
{
    class SemesterService
    {
        public async Task<IList<Semester>> GetSemesters()
        {
            var list = new List<Semester>();

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("select SemestrId,NumerSemestru from Semestr", conn);

                var dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    list.Add(
                        new Semester(Convert.ToInt32(dr["SemestrId"].ToString()),
                        dr["NumerSemestru"].ToString()));
                }
            }
            return list;
        }
    }

}
