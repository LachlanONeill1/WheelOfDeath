using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibWheelOfDeath
{
    public class CResultType : CEntity
    {
        public CResultType() : base("tblResultType")
        {

        }

        public string Name { get; set; } = string.Empty;
        public bool IsWin { get; set; } = false;

        public override void Create()
        {
            CommandText = $@"
                            Insert Into
                                [tblResultType]
                            (
                                [Id],
                                [Name],
                                [IsWin]
                            )
                                Values
                            (
                                @pId,
                                @pName,
                                @pIsWin
                            )
                            ;";
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pName", Name);
            Parameters.AddWithValue("@pIsWin", IsWin);
            base.Create();
        }

        public override int Update()
        {
            CommandText = $@"
                            Update
                                [tblResultType]
                            set
                                [Name] = @pName,
                                [IsWin] = @pIsWin
                            where
                                [Id] = @pId
                            ;";
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pName", Name);
            Parameters.AddWithValue("@pIsWin", IsWin);
            return base.Update();
        }

        public override List<IEntity> Search()
        {
            string fromClause = "[tblResultType] RT ";
            string whereClause = "(1 = 1) ";
            if (Id != 0L)
            {
                whereClause += "and RT.Id = @pId";
                Parameters.AddWithValue("@pId", Id);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                whereClause += "and RT.Name like @pName";
                Parameters.AddWithValue("@pName", $"%{Name}%");
            }
            CommandText = $@"
            select * 
            from {fromClause} 
            where {whereClause}";
            return base.Search();
        }

        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CResultType resultType = (CResultType)entity ?? new CResultType();
            resultType.Id = (long)reader["Id"];
            resultType.Name = (string)reader["Name"];
            resultType.IsWin = (bool)reader["IsWin"];
            return resultType;
        }

        public override void Validate()
        {
            if (Id <= 0)
            {
                throw new CEntityException("Result Type ID must be specified.");
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new CEntityException("Result Type name cannot be empty.");
            }
        }
    }
}
