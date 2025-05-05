using Assessment;
using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWheelOfDeath
{
    public class CAdmin : CAccount
    {
        public CAdmin() {
            this.TableName = "tblAdmin";
        }

       

        #region Properties
        public long FkAccountId { get; set; } = 0L;

        public long FkAdminTypeId { get; set; } = 0L;

        public string Username { get; set; } = string.Empty;
        
        #endregion
   

        #region CRUDS
        public override void Create()
        {
          
            CommandText = $@"insert into [tblAdmin] ([FkAccountId],[FkAdminTypeId], [Username]) values(
                            @pFkAccountId,
                            @pFkAdminTypeId,
                            @pUsername);";
            Parameters.Clear();
            Parameters.AddWithValue("@pFkAccountId", FkAccountId);
            Parameters.AddWithValue("@pFkAdminTypeId", FkAdminTypeId);
            Parameters.AddWithValue("@pUsername", Username);
            CommandText.Insert(Parameters);
        }

        public override int Update()
        {
            
            CommandText = $@"Update [tblAdmin]
                            set
                                [FkAdminTypeId] = @pFkAdminTypeId,
                                [Username] = @pUsername
                            where
                                [FkAccountId] = @pFkAccountId;
                            ";
            Parameters.Clear();
            Parameters.AddWithValue("@pFkAccountId", FkAccountId);
            Parameters.AddWithValue("@pFkAdminTypeId", FkAdminTypeId);
            Parameters.AddWithValue("@pUsername", Username);
            return (int)CommandText.Execute(Parameters);
        }

        public override List<IEntity> Search()
        {
            string fromClause = "[tblAdmin] A ";
            string whereClause = "(1 = 1) ";

            if (FkAccountId > 0L)
            {
                whereClause += " and A.FkAccountId = @pFkAccountId";
                Parameters.AddWithValue("@pFkAccountId", FkAccountId);
            }
            if (FkAdminTypeId > 0L)
            {
                whereClause += " and A.FkAdminTypeId = @pFkAdminTypeId";
                Parameters.AddWithValue("@pFkAdminTypeId", FkAdminTypeId);
            }
            if (!string.IsNullOrWhiteSpace(Username))
            {
                whereClause += " and A.Username like @pUsername";
                Parameters.AddWithValue("@pUsername", $"%{Username}%");
            }

            CommandText = $@"
            select * 
            from {fromClause} 
            where {whereClause}";

            return base.Search();
        }

        #endregion


        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CAdmin admin = (CAdmin)entity ?? new CAdmin();
            admin.FkAccountId = (long)reader["FkAccountId"];
            admin.FkAdminTypeId = (long)reader["FkAdminTypeId"];
            admin.Username = (string)reader["Username"];


            return admin;
        }

        public override void Validate()
        {
            string message = "";
            if (FkAccountId < 0L)
            {
                message += "Must set Account Id";
            }
            if (FkAdminTypeId < 0L)
            {
                message += "Must set the Admin Type Id";
            }
            if (Username.Length < 2 || Username.Length > 100)
            {
                message += "Username min length 2, Max length 100";
            }
            if (message.Length > 0)
            {
                throw new CEntityException(message);
            }
        }

       
    }
}
