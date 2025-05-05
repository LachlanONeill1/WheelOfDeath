using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWheelOfDeath
{
    public class CResult : CEntity
    {
        public CResult() : base("tblResult") { }

        public long FkGameId { get; set; } = 0L;
        public long FkPlayerId { get; set; } = 0L;
        public short Duration { get; set; } = 0;
        public short Misses { get; set; } = 0;
        public short BalloonsPopped { get; set; } = 0;
        public long FkResultTypeId { get; set; } = 0L;

        public override void Create()
        {
            CommandText = @"insert into [tblResult] ([FkGameId], [FkPlayerId], [Duration], [Misses], [BalloonsPopped], [FkResultTypeId]) 
                        values (@pFkGameId, @pFkPlayerId, @pDuration, @pMisses, @pBalloonsPopped, @pFkResultTypeId);";

            Parameters.AddWithValue("@pFkGameId", FkGameId);
            Parameters.AddWithValue("@pFkPlayerId", FkPlayerId);
            Parameters.AddWithValue("@pDuration", Duration);
            Parameters.AddWithValue("@pMisses", Misses);
            Parameters.AddWithValue("@pBalloonsPopped", BalloonsPopped);
            Parameters.AddWithValue("@pFkResultTypeId", FkResultTypeId);

            base.Create();
        }

        public override int Update()
        {
            CommandText = @"Update
                                [tblResult] 
                            set 
                                [FkGameId] = @pFkGameId, 
                                [FkPlayerId] = @pFkPlayerId, 
                                [Duration] = @pDuration,
                                [Misses] = @pMisses, 
                                [BalloonsPopped] = @pBalloonsPopped,  
                                [FkResultTypeId] = @pFkResultTypeId 
                            where [Id] = @pId;";
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pFkGameId", FkGameId);
            Parameters.AddWithValue("@pFkPlayerId", FkPlayerId);
            Parameters.AddWithValue("@pDuration", Duration);
            Parameters.AddWithValue("@pMisses", Misses);
            Parameters.AddWithValue("@pBalloonsPopped", BalloonsPopped);
            Parameters.AddWithValue("@pFkResultTypeId", FkResultTypeId);
            return base.Update();
        }

        public override List<IEntity> Search()
        {
            string fromClause = "[tblResult] R ";
            string whereClause = "(1 = 1) ";

            if (FkGameId > 0L)
            {
                whereClause += "and R.[FkGameId] = @pFkGameId ";
                Parameters.AddWithValue("@pFkGameId", FkGameId);
            }
            if (FkPlayerId > 0L)
            {
                whereClause += "and R.[FkPlayerId] = @pFkPlayerId ";
                Parameters.AddWithValue("@pFkPlayerId", FkPlayerId);
            }
            if (FkResultTypeId > 0L)
            {
                whereClause += "and R.[FkResultTypeId] = @pFkResultTypeId ";
                Parameters.AddWithValue("@pFkResultTypeId", FkResultTypeId);
            }
            if (Duration > 0)
            {
                whereClause += "and R.[Duration] = @pDuration ";
                Parameters.AddWithValue("@pDuration", Duration);
            }
            if (Misses > 0)
            {
                whereClause += "and R.[Misses] = @pMisses ";
                Parameters.AddWithValue("@pMisses", Misses);
            }
            if (BalloonsPopped > 0)
            {
                whereClause += "and R.[BalloonsPopped] = @pBalloonsPopped ";
                Parameters.AddWithValue("@pBalloonsPopped", BalloonsPopped);
            }

            CommandText = $@"
        select 
            *
        from                 
            {fromClause}
        where
            {whereClause}
    ";

            return base.Search();
        }

        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CResult result = (CResult)entity ?? new CResult();
            result.Id = (long)reader["Id"];
            result.FkGameId = (long)reader["FkGameId"];
            result.FkPlayerId = (long)reader["FkPlayerId"];
            result.Duration = (short)reader["Duration"];
            result.Misses = (short)reader["Misses"];
            result.BalloonsPopped = (short)reader["BalloonsPopped"];
            result.FkResultTypeId = (long)reader["FkResultTypeId"];
            return result;
        }

        public override void Validate()
        {
            if (FkGameId <= 0)
                throw new CEntityException("Valid Game ID is required.");
            if (FkPlayerId <= 0)
                throw new CEntityException("Valid Player ID is required.");
            if (FkResultTypeId <= 0)
                throw new CEntityException("Valid Result Type ID is required.");
        }
        
    }
    

}
