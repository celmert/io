using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using WinFormStudents.Model;

namespace WinFormStudents.Services
{
    class ScienceTitleService
    {
        public async Task<IList<TitleScience>> Get()
        {
            var list = new List<TitleScience>();

            using (var conn = new SQLiteConnection(ConnectionService.ConnectionName))
            {
                await conn.OpenAsync();
                var command = new SQLiteCommand("select stopiennaukowyid, nazwastopnia from stopiennaukowy", conn);

                var dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    list.Add(
                        new TitleScience(dr.GetInt32(0), dr.GetString(1)));
                }
            }
            return list;
        }
    }
}
