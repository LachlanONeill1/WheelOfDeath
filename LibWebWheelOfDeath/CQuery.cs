using Assessment;
using LibEntity.NetFramework;
using LibWheelOfDeath;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibWebWheelOfDeath
{
    public static class CQuery
    {
        public static string GetGameSettings { get; set; } = @"select G.MaxDuration, G.MaxBalloons, G.MaxMisses, G.MaxThrows
                                                        from tblGame G
                                                        where G.Id = @pId";

        public static string difficultyGameOptGroup { get; set; } = @"select [tblDifficulty].DifficultyType as DifficultyName,
                                                                    [tblGame].*
                                                                    from [tblDifficulty]
                                                                    inner join
                                                                    [tblGame] on [tblDifficulty].Id = [tblGame].FkDifficultyId
                                                                    order by
                                                                    [tblGame].FkDifficultyId,
                                                                    [tblGame].Name
                                                                    ;";
        public static string LeaderBoardResults = "";

        public static string Difficulties = "Select [tblDifficulty].Id, [tblDifficulty].DifficultyType from [tblDifficulty]";

        public static string UserFiler = $@"SELECT 
                                            A.Id,
                                            P.Username,
                                            A.FirstName,
                                            A.LastName,
                                            A.IsActive
                                        FROM tblPlayer P
                                        inner JOIN tblAccount A ON P.FkAccountId = a.Id
                                        ORDER BY P.Username;";
        public static string Admins = $@"select tblAdminType.Name, tblAdminType.Id from [tblAdminType];";

        public static DataTable FilterQuery(string username, string firstName, string lastName)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbWheelofDeath;";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    string query = @"
                select p.[FkAccountId], p.[Username], a.[FirstName], a.[LastName], a.[IsActive]
                from [tblPlayer] p
                inner join [tblAccount] a on p.[FkAccountId] = a.[Id]
                where 1 = 1";

                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        query += " and p.[Username] like @Username";
                        cmd.Parameters.AddWithValue("@Username", "%" + username + "%");
                    }
                    if (!string.IsNullOrWhiteSpace(firstName))
                    {
                        query += " and a.[FirstName] like @FirstName";
                        cmd.Parameters.AddWithValue("@FirstName", "%" + firstName + "%");
                    }
                    if (!string.IsNullOrWhiteSpace(lastName))
                    {
                        query += " and a.[LastName] like @LastName";
                        cmd.Parameters.AddWithValue("@LastName", "%" + lastName + "%");
                    }

                    cmd.CommandText = query;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable result = new DataTable();
                    adapter.Fill(result);
                    return result;
                }
            }

        }
        public static string LeaderboardQuery {get; set;} = 
                            $@"select 
                            count(*) as ResultCount,
                            P.[Username],
                            R.[Duration], 
                            G.[Name] as GameName,
                            R.[Misses], 
                            R.[BalloonsPopped]       
                            from tblResult R
                            inner join tblGame G on R.FkGameId = G.Id
                            inner join tblPlayer P on R.FkPlayerId = P.FkAccountId
                            group by 
                            R.[FkPlayerId], 
                            R.[Duration], 
                            R.[Misses], 
                            R.[BalloonsPopped], 
                            G.[Name], 
                            P.[Username];";
    }
    
}
