using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWheelOfDeath
{
    public class CDifficulty : CEntity
    {
        public CDifficulty() : base("tblDifficulty")
        {
             
        }

        public string DifficultyType { get; set; } = string.Empty;
        public long FkAdminId { get; set; } = 0L;

        public override void Create()
        {
            CommandText = $@"
                            Insert Into
                                [tblDifficulty]
                            (
                                [DifficultyType],
                                [FkAdminId]
                            )
                                Values
                            (
                                @pDifficultyType,
                                @pFkAdminId
                            )
                            ;";
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pDifficultyType", DifficultyType);
            Parameters.AddWithValue("@pFkAdminId", FkAdminId); 
            base.Create();
        }
        public override int Update()
        {
            CommandText = $@"
                            Update
                                [tblDifficulty]
                            set
                                [DifficultyType] = @pDifficultyType,
                                [FkAdminId] = @pFkAdminId
                            where
                                [Id] = @pId
                            ;";
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pDifficultyType", DifficultyType);
            Parameters.AddWithValue("@pFkAdminId", FkAdminId);
            return base.Update();
        }
        public override List<IEntity> Search()
        {
            string fromClause = "[tblDifficulty] D";
            string whereClause = "(1 = 1)";

            if (Id != 0L)
            {
                whereClause += " and D.Id = @pId";
                Parameters.AddWithValue("@pId", Id);
            }
            if (!string.IsNullOrWhiteSpace(DifficultyType))
            {
                whereClause += " and D.DifficultyType like @pDifficultyType";
                Parameters.AddWithValue("@pDifficultyType", $"%{DifficultyType}%");
            }
            if (FkAdminId > 0L)
            {
                whereClause += " and D.FkAdminId = @pFkAdminId";
                Parameters.AddWithValue("@pFkAdminId", FkAdminId);
            }

            CommandText = $@"
            select * 
            from {fromClause} 
            where {whereClause}";

            return base.Search();
        }

        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CDifficulty difficulty = (CDifficulty)entity ?? new CDifficulty();

            difficulty.Id = (long)reader["Id"];
            difficulty.DifficultyType = (string)reader["DifficultyType"];
            difficulty.FkAdminId = (long)reader["FkAdminId"];

            return difficulty;
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(DifficultyType))
            {
                throw new CEntityException("Difficulty type cannot be empty.");
            }
            if (FkAdminId <= 0)
            {
                throw new CEntityException("Invalid Admin ID.");
            }
                
        }

        public DataSet GamesFilter()
        {
            CommandText = $@"select * 
	                        from [tblDifficulty]
                        inner join
	                        [tblGame] on [tblDifficulty].Id = [tblGame].FkDifficultyId
                        order by
	                        [tblGame].FkDifficultyId,
	                        [tblGame].Name
                            ";
           
            DataSet dataSet = CommandText.Fetch<DataSet>();
            return dataSet;
          
        }
       
    }
}
