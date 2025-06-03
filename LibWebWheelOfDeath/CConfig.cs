using LibEntity.NetFramework;
using LibWheelOfDeath;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWebWheelOfDeath
{
    public class CConfig : CEntity
    {
        
        public long ResultsShown { get; set; } = 10;
        public CConfig() : base("tblConfig")
        {
            Id = 1;
        }
        public override int Update()
        {
            CommandText = $@"
                            Update [tblConfig]
                        set
                            [ResultsShown] = @pResultsShown
                            ";
            Parameters.AddWithValue("@pResultsShown", ResultsShown);
            return base.Update();
        }
      

        public override IEntity Populate(SqlDataReader reader, IEntity entity = null)
        {
            CConfig config = (CConfig)entity ?? new CConfig();        
            config.ResultsShown = (long)reader["ResultsShown"];
            return config;
        }

        public override void Validate()
        {
            string message = "";
            if (ResultsShown < 1)
            {
                message += "Results shown must be greater than 0";
            }
            if (ResultsShown > 150) 
            {
                message += "Results shown must be less than 150";
            }
            if (!string.IsNullOrWhiteSpace(message))
            {
                throw new CEntityException(message);
            }   
        }
    }
}
