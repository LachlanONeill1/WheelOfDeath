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

    public class CPlayer : CAccount
    {

        public string Username { get; set; } = string.Empty;
        public long FkAccountId { get; set; } = 0L;

        public bool IsLoggedIn => FkAccountId != 0L;
        public CPlayer()
        {
            this.TableName = "tblPlayer";
        }

        public static CPlayer Login(string userName, string password)
        {
            //if (string.IsNullOrWhiteSpace(firstName)){ throw new CEntityException("First Name must be supplied"); }
            //if (string.IsNullOrWhiteSpace(lastName)) { throw new CEntityException("Last Name must be supplied"); }
            if (string.IsNullOrWhiteSpace(userName)) { throw new CEntityException("Username must be supplied"); }
            if (string.IsNullOrWhiteSpace(password)) { throw new CEntityException("Password must be supplied"); }
            if (password.Length < 1) { throw new CEntityException("Password must be supplied"); }

            CPlayer userSearch = new CPlayer()
            {
                Username = userName,
                Password = password
            };

            List<IEntity> list = userSearch.Search();

            if (list.Count > 0)
            {
                return (CPlayer)list.FirstOrDefault();
            }
            else
            {
                throw new CEntityException("Login failed");
            }


        }

        public override void Create()
        {

            base.Create();
            FkAccountId = Id;

            CommandText = $@"
                            insert into [tblPlayer] ([FkAccountId], [Username]) values (
                            @pFkAccountId, @pUsername);";
            Parameters.Clear();
            Parameters.AddWithValue("@pFkAccountId", FkAccountId);
            Parameters.AddWithValue("@pUsername", Username);
            Validate();
            Create(false);
            
        }

        public override int Update()
        {
            CommandText = $@"
                            Update [tblPlayer]
                            set
                                [FkAccountId] = @pFkAccountId,
                                [Username] = @pUsername;
                            ";
            Parameters.Clear();
            Parameters.AddWithValue("@pFkAccountId", FkAccountId);
            Parameters.AddWithValue("@pUsername", Username);
            return (int)CommandText.Execute(Parameters);
        }

        public override List<IEntity> Search()
        {
            string whereClause = "(1 = 1) ";

            if (Id != 0L)
            {
                whereClause += "and tblAccount.Id = @pId ";
                Parameters.AddWithValue("@pId", Id);
            }
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                whereClause += "and tblAccount.FirstName like @pFirstName ";
                Parameters.AddWithValue("@pFirstName", $"%{FirstName}%");
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                whereClause += "and tblAccount.Lastname like @pLastname ";
                Parameters.AddWithValue("@pLastname", $"%{LastName}%");
            }
            if (!string.IsNullOrWhiteSpace(Username))
            {
                whereClause += "and tblPlayer.Username = @pUsername ";
                Parameters.AddWithValue("@pUsername", Username);
            }
            if (FkAccountId > 0L)
            {
                whereClause += "and tblPlayer.FkAccountId = @pFkAccountId ";
                Parameters.AddWithValue("@pFkAccountId", FkAccountId);
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                whereClause += "and tblAccount.Password = @pPassword ";
                Parameters.AddWithValue("@pPassword", Password);
            }

            CommandText = $@"
            select
	            * 
            from
	            tblAccount
            inner join
	            tblPlayer on tblAccount.Id = tblPlayer.FkAccountId
            where
              {whereClause};";

            return base.Search();
        }

    
        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CPlayer player = (CPlayer)entity ?? new CPlayer();
            player.Id = (long)reader["Id"];
            player.FkAccountId = (long)reader["FkAccountId"];
            player.FirstName = (string)reader["FirstName"];
            player.LastName = (string)reader["LastName"];
            player.Username = (string)reader["Username"];
            player.Password = (string)reader["Password"];

            return player;
        }

        public override void Validate()
        {
            string message = "";
            if (this.FkAccountId < 0L)
            {
                message += "FkAccountId must be set";
            }
            if (Username.Length < 2 || Username.Length > 100)
            {
                message += "Username min length 2 characters Max length 100";
            }
            if (!PasswordValidate(out string passwordMessage))
            {
                message += passwordMessage;
            }
            if (message.Length > 0)
            {
                throw new CEntityException(message);
            }
            
        }
        public override string ToString()
        {
            return $"Id {Id}, FkAccountId {FkAccountId}, Username {Username}, Password {Password}";
        }
    }
}
