using Assessment;
using LibWebWheelOfDeath;
using LibWheelOfDeath;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEntity.NetFramework;

namespace TestBed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CAccount account = new CAccount();
            //Console.WriteLine($"All Accounts");
            //foreach (var acc in account.ListAccounts())
            //{
                
            //    Console.WriteLine($"{acc.ToString()}{Environment.NewLine}");
            //}

            try
            {
                Model.Initialize();
                //CPlayer player = new CPlayer();
                //player.FirstName = "John";
                //player.LastName = "Doe";
                //player.Username = "johnnyD";
                //player.Password = "password123";
                
                //Console.WriteLine($"{CPlayer.Login(player.Username, player.Password)}");

                //string sql = @"select [tblDifficulty].DfficultyType as DifficultyName,
                //            [tblGame].*
                //         from [tblDifficulty]
                //        inner join
                //         [tblGame] on [tblDifficulty].Id = [tblGame].FkDifficultyId
                //        order by
                //         [tblGame].FkDifficultyId,
                //         [tblGame].Name
                //        ;";
                //DataTable table = sql.Fetch<DataTable>();
                //long priorFkDifficultyId = -1;
                //int i = 0;

                //while (i < table.Rows.Count)
                //{
                //    DataRow row = table.Rows[i];            
                //    if ((long)row["FkDifficultyId"] != priorFkDifficultyId )
                //    {
                //        priorFkDifficultyId = (long)(row["FkDifficultyId"]);
                //        string difficultyName = (string)row["DifficultyName"];
                //        Console.WriteLine(difficultyName);

                //        do
                //        {
                //            long gameId = (long)row["Id"];
                //            string gameName = (string)row["Name"];
                //            Console.WriteLine(gameId + gameName);
                //            i = i + 1;
                //            if (i >= table.Rows.Count) {
                //                break;
                //            }
                //            row = table.Rows[i];
                //            long nextFkDifficultyId = (long)row["FkDifficultyId"];

                //        } while (priorFkDifficultyId == (long)row["FkDifficultyId"]);

                //    }
                //}


                //Console.ReadKey();
                //Environment.Exit(0);


                //InsertResultType();
                //UpdateResultType();
                //DeleteResultType();

                //InsertResult();
                //UpdateResult();
                //DeleteResult();

                //InsertGame();
                //UpdateGame();
                //DeleteGame();

                //InsertDifficulty();
                //UpdateDifficulty();
                //DeleteDifficulty();

                //DeleteAdmin();
                //UpdateAdmin();
                //InsertAdmin();

                InsertAccount();
                //UpdateAccount();
                //DeleteAccount();

                //InsertPlayer();
                //UpdatePlayer();
                //DeletePlayer();

                //---TODO ADMIN TYPE---

            }
            catch (Exception E)
            {
                Console.Write(E);
                Console.ReadLine();

                Environment.Exit(0);
            }
            


        }

        //-----------------tblAccount--------------------
        static void InsertAccount() // Pass
        {
            CAccount account = new CAccount();
            CPlayer player = new CPlayer();
            try
            {
                Console.WriteLine("Enter First Name");
                player.FirstName = Console.ReadLine();

                Console.WriteLine("Enter Last Name");
                player.LastName = Console.ReadLine();

                Console.WriteLine("Enter User Name");
                player.Username = Console.ReadLine();

                Console.WriteLine("Enter Password");
                player.Password = Console.ReadLine();
                
                
                player.Create();
                if (player.FkAccountId > 0L)
                {
                    Console.WriteLine("Successful");
                }

            } catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            
            Environment.Exit(0);

        }

        static void UpdateAccount() // Pass
        {
            CAccount account = new CAccount();
            try
            {
                Console.WriteLine("Enter Id to Update");
                account.Id = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter First Name");
                account.FirstName = Console.ReadLine();

                Console.WriteLine("Enter Last Name");
                account.LastName = Console.ReadLine();

                Console.WriteLine("Enter Password");
                account.Password = Console.ReadLine();

                
                if (account.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }

            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }
        static void DeleteAccount() // Pass
        {
            try
            {

                CAccount account = new CAccount();



                Console.WriteLine("Enter ID to delete: ");
                account.Id = long.Parse(Console.ReadLine());


                if (account.Delete() > 0)
                {
                    Console.WriteLine("Account Deleted");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);

        }
        //-------------PLAYER----------------------
        static void InsertPlayer() // Pass
        {
            try
            {
                CPlayer player = new CPlayer();

                Console.WriteLine("Enter Account Id");
                player.FkAccountId = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter Username");
                player.Username = Console.ReadLine();
                player.Create();
                if (player.Id > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }

        static void UpdatePlayer() // Pass
        {
            try
            {
                CPlayer player = new CPlayer();

                Console.WriteLine("Enter Account Id to Update");
                player.FkAccountId = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter new Username");
                player.Username = Console.ReadLine();
               
                if (player.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }
        static void DeletePlayer() // Pass
        {
            try
            {
                CResult result = new CResult(); // will need this when we have results in the table
                CPlayer player = new CPlayer();
                CAccount account = new CAccount();



                Console.WriteLine("Enter ID to delete: ");
                account.Id = long.Parse(Console.ReadLine());
                result.Id = account.Id;
                player.Id = account.Id;

                if (player.Delete() > 0 && account.Delete() > 0)
                {
                    Console.WriteLine("Account Deleted");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }

        //------------ADMIN-------------------
       
        static void InsertAdmin() // Pass
        {
            
            try
            {
                CAdmin admin = new CAdmin();

                Console.WriteLine("Enter Account Id");
                admin.FkAccountId = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter Username");
                admin.Username = Console.ReadLine();

                Console.WriteLine("Enter Type of Admin (1 super, 2 standard)");
                admin.FkAdminTypeId = long.Parse(Console.ReadLine());

                admin.Create();
                if (admin.FkAccountId > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }
        static void UpdateAdmin() // Pass
        {
            try
            {
                CAdmin admin = new CAdmin();

                Console.WriteLine("Enter Id to Update");
                admin.FkAccountId = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter Username");
                admin.Username = Console.ReadLine();

                Console.WriteLine("Enter Type of Admin (1 super, 2 standard)");
                admin.FkAdminTypeId = long.Parse(Console.ReadLine());

                
                if (admin.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }
        static void DeleteAdmin() // Pass
        {
            try
            {
                CAdmin admin = new CAdmin();
                CAccount account = new CAccount();
                CDifficulty difficulty = new CDifficulty();
                CGame game = new CGame();



                Console.WriteLine("Enter ID to delete: ");
                account.Id = long.Parse(Console.ReadLine());
                difficulty.Id = account.Id;
                game.Id = account.Id;
                admin.Id = account.Id;

                if (game.Delete() > 0 && difficulty.Delete() > 0 && admin.Delete() > 0 && account.Delete() > 0 )
                {
                    Console.WriteLine("Account Deleted");
                }
            }
            catch (Exception E)
            {

                Console.WriteLine(E.Message);
            }
            Console.ReadLine();

            Environment.Exit(0);
        }
        //-------------Difficulty----------------
        static void InsertDifficulty() // Pass
        {
            try
            {
                CDifficulty difficulty = new CDifficulty();
                Console.WriteLine("Enter Admin Id");
                difficulty.FkAdminId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Difficulty Type");
                difficulty.DifficultyType = Console.ReadLine();
                difficulty.Create();
                if (difficulty.FkAdminId > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void UpdateDifficulty() 
        {
            try
            {
                CDifficulty difficulty = new CDifficulty();
                Console.WriteLine("Enter Id to Update");
                difficulty.Id = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Difficulty Type");
                difficulty.DifficultyType = Console.ReadLine();
                Console.WriteLine("Enter Admin Id");
                difficulty.FkAdminId = long.Parse(Console.ReadLine());

                if (difficulty.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void DeleteDifficulty() // Pass
        {
            try
            {
                CDifficulty difficulty = new CDifficulty();
                Console.WriteLine("Enter ID to delete: ");
                difficulty.Id = long.Parse(Console.ReadLine());

                if (difficulty.Delete() > 0)
                {
                    Console.WriteLine("Difficulty Deleted");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }
        //---------------GAME------------------
        static void InsertGame() // Pass
        {
            try
            {
                CGame game = new CGame();
                Console.WriteLine("Enter Game Name");
                game.Name = Console.ReadLine();
                Console.WriteLine("Enter Admin Id");
                game.FkAdminId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Difficulty Id");
                game.FkDifficultyId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Min Balloons");
                game.MinBalloons = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Balloons");
                game.MaxBalloons = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Misses");
                game.MaxMisses = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Duration");
                game.MaxDuration = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Throws");
                game.MaxThrows = short.Parse(Console.ReadLine());
            
                game.GameDateTime = DateTime.Now;
                

                game.Create();
                if (game.FkAdminId > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void UpdateGame() // Pass
        {
            try
            {
                CGame game = new CGame();
                Console.WriteLine("Enter Id to Update");
                game.Id = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Game Name");
                game.Name = Console.ReadLine();
                Console.WriteLine("Enter Admin Id");
                game.FkAdminId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Difficulty Id");
                game.FkDifficultyId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Min Balloons");
                game.MinBalloons = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Balloons");
                game.MaxBalloons = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Misses");
                game.MaxMisses = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Duration");
                game.MaxDuration = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Max Throws");
                game.MaxThrows = short.Parse(Console.ReadLine());

                game.GameDateTime = DateTime.Now;


                if (game.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void DeleteGame() // Pass
        {
            try
            {
                CGame game = new CGame();
                //CResult result = new CResult();
                Console.WriteLine("Enter ID to delete: ");
                game.Id = long.Parse(Console.ReadLine());
                
                //result.Id = game.Id;

                if (game.Delete() > 0)
                {
                    Console.WriteLine("Game Deleted");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }
        //----------------RESULT---------------------
        static void InsertResult() //PASS
        {
            try
            {
                CResult result = new CResult();
                Console.WriteLine("Enter Game Id");
                result.FkGameId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Player Id");
                result.FkPlayerId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Duration");
                result.Duration = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Misses");
                result.Misses = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Balloons Popped");
                result.BalloonsPopped = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Result Type Id");
                result.FkResultTypeId = long.Parse(Console.ReadLine());

                result.Create();
                if (result.FkGameId > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void UpdateResult() // PASS
        {
            try
            {
                CResult result = new CResult();
                Console.WriteLine("Enter Id to Update");
                result.Id = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Game Id");
                result.FkGameId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Player Id");
                result.FkPlayerId = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Duration");
                result.Duration = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Misses");
                result.Misses = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Balloons Popped");
                result.BalloonsPopped = short.Parse(Console.ReadLine());
                Console.WriteLine("Enter Result Type Id");
                result.FkResultTypeId = long.Parse(Console.ReadLine());

                if (result.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void DeleteResult()
        {
            try
            {
                CResult result = new CResult();
                Console.WriteLine("Enter ID to delete: ");
                result.Id = long.Parse(Console.ReadLine());

                if (result.Delete() > 0)
                {
                    Console.WriteLine("Result Deleted");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        //----------ADMIN TYPE-----------
        static void InsertAdminType() 
        {
            try
            {
                CAdminType adminType = new CAdminType();
                Console.WriteLine("Enter Admin Type Name");
                adminType.Name = Console.ReadLine();
                adminType.Create();
                if (!string.IsNullOrWhiteSpace(adminType.Name))
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void UpdateAdminType() 
        {
            try
            {
                CAdminType adminType = new CAdminType();
                Console.WriteLine("Enter Id to Update");
                adminType.Id = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Admin Type Name");
                adminType.Name = Console.ReadLine();

                if (adminType.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void DeleteAdminType() 
        {
            try
            {
                CAdminType adminType = new CAdminType();
                Console.WriteLine("Enter ID to delete: ");
                adminType.Id = long.Parse(Console.ReadLine());

                if (adminType.Delete() > 0)
                {
                    Console.WriteLine("Admin Type Deleted");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        //-----------------RESULT TYPE----------------

        static void InsertResultType() //PASS???
        {
            try
            {
                CResultType resultType = new CResultType();
                Console.WriteLine("Enter Result Type ID");
                resultType.Id = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Result Type Name");
                resultType.Name = Console.ReadLine();
                Console.WriteLine("Is Win? (true/false)");
                resultType.IsWin = bool.Parse(Console.ReadLine());

                resultType.Create();
                if (resultType.Id > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void UpdateResultType() //PASS
        {
            try
            {
                CResultType resultType = new CResultType();
                Console.WriteLine("Enter Id to Update");
                resultType.Id = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Result Type Name");
                resultType.Name = Console.ReadLine();
                Console.WriteLine("Is Win? (true/false)");
                resultType.IsWin = bool.Parse(Console.ReadLine());

                if (resultType.Update() > 0L)
                {
                    Console.WriteLine("Successful");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void DeleteResultType() //PASS
        {
            try
            {
                CResultType resultType = new CResultType();
                Console.WriteLine("Enter ID to delete: ");
                resultType.Id = long.Parse(Console.ReadLine());

                if (resultType.Delete() > 0)
                {
                    Console.WriteLine("Result Type Deleted");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }

    }
}