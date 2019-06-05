using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormStudents.Model;

namespace WinFormStudents.Services
{
    class LessonService
    {
        public async Task<IList<Lesson>> Get()
        {
            var list = new List<Lesson>();

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("select przedmiotid,nazwaprzedmiotu, nazwatypu from przedmiot" +
                    " inner join typzajec on przedmiot.typzajecid = typzajec.typzajecid order by nazwaprzedmiotu", conn);

                var dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    list.Add(
                        new Lesson(dr.GetInt32(0), dr.GetString(1),dr.GetString(2)));
                }
            }
            return list;
        }
    }
}
