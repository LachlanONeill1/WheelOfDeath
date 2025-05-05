using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
