using LibEntity;
using LibEntity.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
namespace Assessment
{
    public class CAccount : CEntity
    {
        #region Constructors
        public CAccount() : base("tblAccount")
        {
            
        }

        #endregion

        #region Properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        private string _password = string.Empty;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public bool IsActive { get; set; } = true;

        #endregion

       

        public new virtual void Create()
        {
            Validate();
            CommandText = $@"insert into [tblAccount]
                        (
                            [FirstName], 
                            [LastName],      
                            [Password], 
                            [IsActive]
                        ) 
                        values
                        (
                            @pFirstName,
                            @pLastName,                        
                            @pPassword,
                             @pIsActive  
                        );";
           
            Parameters.AddWithValue("@pFirstName", FirstName);
            Parameters.AddWithValue("@pLastName", LastName );
            Parameters.AddWithValue("@pPassword", Password );
            Parameters.AddWithValue("@pIsActive", IsActive);
           
            base.Create(true);
        }

        public override int Update()
        {
            if (Password.Length >0)
            {
                CommandText = $@"Update
                                [tblAccount]
                            set
                                [Firstname] = @pFirstname,
                                [LastName] = @pLastname,
                                [Password] = @pPassword,
                                [IsActive] = @pIsActive
                            where
                                [Id] = @pId
                                ";
                Parameters.AddWithValue("@pPassword", Password);
            }
            else
            {
                CommandText = $@"Update
                                [tblAccount]
                            set
                                [Firstname] = @pFirstname,
                                [LastName] = @pLastname,
                                [IsActive] = @pIsActive
                            where
                                [Id] = @pId";
            }
            Parameters.AddWithValue("@pId", Id);
            Parameters.AddWithValue("@pFirstname", FirstName);
            Parameters.AddWithValue("@pLastname", LastName);
            Parameters.AddWithValue("@pIsActive", IsActive);
            return base.Update();
        }

        

        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CAccount account = (CAccount)entity ?? new CAccount();
            account.Id = (long)reader["Id"];
            account.FirstName = (string)reader["FirstName"];
            account.LastName = (string)reader["LastName"];
            account.Password = (string)reader["Password"];
            account.IsActive = (bool)reader["IsActive"];

            return account;
        }

        public override void Validate()
        {
            string message = "";
            if (!PasswordValidate(out string passwordMessage))
            {
                message += passwordMessage;
            }
            if (Id < 0L)
            {
                message += $"Id must be set.{Environment.NewLine}";
            }

            if (FirstName.Length < 2 || FirstName.Length > 100)
            {
                message += $"Firstname min length 2, max length 100{Environment.NewLine}";
            }

            if (LastName.Length < 2 || LastName.Length > 100)
            {
                message += $"Lastname min length 2, max length 100{Environment.NewLine}";
            }
            if (IsActive == false)
            {
                message += $"Account must be active{Environment.NewLine}";
            }
            
            if (message.Length > 0)
            {
                throw new CEntityException(message);
            }

            
        }

        public List<IEntity> ListAccounts()
        {

            CommandText = $@"select * from [tblAccount]";

            return base.Search();
        }

       


        public bool PasswordValidate(out string message)
        {
            message = "";
            int nums = 0;
            int lowers = 0;
            int uppers = 0;
            int special = 0;

            if (Password.Length < 12)
            {
                message += $"Password must be atleast 12 characters minimum{Environment.NewLine}";
            }

            foreach (char nextChar in Password)
            {
                if (char.IsNumber(nextChar)){
                    nums++;
                }
                if (char.IsLower(nextChar))
                {
                    lowers++;
                }
                if (char.IsUpper(nextChar))
                {
                    uppers++;
                }
                if (!char.IsLetterOrDigit(nextChar))
                {
                    special++;
                }
            }
            if (uppers< 2)
            {
                message += $"Must contain atleast 2 uppercase{Environment.NewLine}";
            }
            if (lowers < 2)
            {
                message += $"Must contain atleast 2 lowercase{Environment.NewLine}";
            }
            if (nums < 2)
            {
                message += $"Must contain atleast 2 numbers{Environment.NewLine}";
            }
            if (special < 2)
            {
                message += $"Must contain atleast 2 special characters{Environment.NewLine}";
            }

            return message == "";
        }
        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}";
        }


    }
}
