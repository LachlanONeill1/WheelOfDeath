using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWheelOfDeath
{
    public class CGame : CEntity
    {
        public string Name { get; set; } = string.Empty;
        public long FkAdminId { get; set; } = 0L;
        public long FkDifficultyId { get; set; } = 0L;
        public short MinBalloons { get; set; } = 0;
        public short MaxBalloons { get; set; } = 0;
        public short MaxMisses { get; set; } = 0;
        public short MaxDuration { get; set; } = 0;
        public short MaxThrows { get; set; } = 0;
        public DateTime GameDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public CGame() : base("tblGame") { }

        public override void Create()
        {
            CommandText = @"insert into [tblGame] ([Name], [FkAdminId], [FkDifficultyId], [MinBalloons], [MaxBalloons], [MaxMisses], [MaxDuration], [GameDateTime], [MaxThrows], [IsActive])
                        values (@pName, @pFkAdminId, @pFkDifficultyId, @pMinBalloons, @pMaxBalloons, @pMaxMisses, @pMaxDuration, @pGameDateTime, @pMaxThrows, @pIsActive);";
            Parameters.AddWithValue("@pName", Name);
            Parameters.AddWithValue("@pFkAdminId", FkAdminId);
            Parameters.AddWithValue("@pFkDifficultyId", FkDifficultyId);
            Parameters.AddWithValue("@pMinBalloons", MinBalloons);
            Parameters.AddWithValue("@pMaxBalloons", MaxBalloons);
            Parameters.AddWithValue("@pMaxMisses", MaxMisses);
            Parameters.AddWithValue("@pMaxDuration", MaxDuration);
            Parameters.AddWithValue("@pGameDateTime", GameDateTime);
            Parameters.AddWithValue("@pMaxThrows", MaxThrows);
            Parameters.AddWithValue("@pIsActive", IsActive);
            base.Create();
        }

        public override int Update()
        {
            CommandText = @"Update 
                                [tblGame] 
                            set 
                                [Name] = @pName, 
                                [FkAdminId] = @pFkAdminId, 
                                [FkDifficultyId] = @pFkDifficultyId, 
                                [MinBalloons] = @pMinBalloons,
                                [MaxBalloons] = @pMaxBalloons, 
                                [MaxMisses] = @pMaxMisses,  
                                [MaxDuration] = @pMaxDuration, 
                                [GameDateTime] = @pGameDateTime,
                                [MaxThrows] = @pMaxThrows, 
                                [IsActive] = @pIsActive 
                            Where [Id] = @pId;";
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pName", Name);
            Parameters.AddWithValue("@pFkAdminId", FkAdminId);
            Parameters.AddWithValue("@pFkDifficultyId", FkDifficultyId);
            Parameters.AddWithValue("@pMinBalloons", MinBalloons);
            Parameters.AddWithValue("@pMaxBalloons", MaxBalloons);
            Parameters.AddWithValue("@pMaxMisses", MaxMisses);
            Parameters.AddWithValue("@pMaxDuration", MaxDuration);
            Parameters.AddWithValue("@pGameDateTime", GameDateTime);
            Parameters.AddWithValue("@pMaxThrows", MaxThrows);
            Parameters.AddWithValue("@pIsActive", IsActive);
            return base.Update();
        }

        public override List<IEntity> Search()
        {
            string fromClause = "[tblGame] G ";
            string whereClause = "(1 = 1) " ;
            if (Id > 0L)
            {
                whereClause += "and G.Id = @pId";
                Parameters.AddWithValue("@pId", Id);
            }
            if (FkAdminId > 0L)
            {
                whereClause += "and G.FkAdminId = @pFkAdminId";
                Parameters.AddWithValue("@pFkAdminId", FkAdminId);
            }
            if (FkDifficultyId > 0L)
            {
                whereClause += "and G.FkDifficultyId = @pFkDifficultyId";
                Parameters.AddWithValue("@pFkDifficultyId", FkDifficultyId);
            }

            CommandText = $"select * from {fromClause} where {whereClause}";
            return base.Search();
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new CEntityException("Game name is required.");
            if (FkAdminId <= 0)
                throw new CEntityException("Valid Admin ID is required.");
            if (FkDifficultyId <= 0)
                throw new CEntityException("Valid Difficulty ID is required.");
        }

        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CGame game = (CGame)entity ?? new CGame();

            game.Id = (long)reader["Id"];
            game.Name = (string)reader["Name"];
            game.FkAdminId = (long)reader["FkAdminId"];
            game.FkDifficultyId = (long)reader["FkDifficultyId"];
            game.MinBalloons = (short)reader["MinBalloons"];
            game.MaxBalloons = (short)reader["MaxBalloons"];
            game.MaxMisses = (short)reader["MaxMisses"];
            game.MaxDuration = (short)reader["MaxDuration"];
            game.GameDateTime = (DateTime)reader["GameDateTime"];
            game.MaxThrows = (short)reader["MaxThrows"];
            game.IsActive = (bool)reader["IsActive"];

            return game;
        }


    }
}
