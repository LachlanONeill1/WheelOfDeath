using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWheelOfDeath
{
    public class CAdminType : CEntity
    {
        public string Name { get; set; } = string.Empty;

        public CAdminType() : base("tblAdminType")
        {
        }

        public override void Create()
        {
            CommandText = $@"
                        Insert into [tblAdminType]
                            (
                                [Name]
                            ) 
                        values
'                           (
                                @pName
                            )
                            ";
            Parameters.AddWithValue("@pName", Name);
            base.Create();
        }

        public override int Update()
        {
            CommandText = $@"
                            Update [tblAdminType]
                        set
                            [Name] = @pName
                        where
                            [Id] = @pId
                            ";
            return base.Update();
        }
        public override List<IEntity> Search()
        {
            string fromClause = "[tblAdminType] AT ";
            string whereClause = "(1 = 1) ";
            if (Id != 0L)
            {
                whereClause += "and AT.Id = @pId";
                Parameters.AddWithValue("@pId", Id);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                whereClause += "and AT.Name like @pName";
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
            CAdminType adminType = (CAdminType)entity ?? new CAdminType();
            adminType.Id = (long)reader["Id"];
            adminType.Name = (string)reader["Name"];

            return adminType;
        }

        public override void Validate()
        {
            string message = "";

            if (this.Name.Length < 2 || this.Name.Length > 100)
            {
                message += "Min name length 2, max length 100";
            }
            if (message.Length > 0) { throw new CEntityException(message); };
        }
    }
}
