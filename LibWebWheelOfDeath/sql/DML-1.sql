use [dbWheelofDeath];
go
-- ADMIN
insert into [tblAdminType] ([Name]) values 
('super'),
('standard');

insert into [tblAccount]([FirstName], [LastName], [Password],[IsActive]) values(
'Joe',
'Smith',
'WheelOfDeathAdmin1',
1);
declare @AdminAccountJoe bigint = scope_identity();

insert into [tblAdmin] ([FkAccountId],[FkAdminTypeId], [Username]) values(
@AdminAccountJoe,
1,
'JSmith21');

--PLAYER
insert into [tblAccount]([FirstName], [LastName], [Password],[IsActive]) values(
'Michael',
'Jordan',
'Password1',
1);
declare @PlayerAccountMJ bigint = scope_identity();

insert into [tblPlayer] ([FkAccountId], [Username]) values (
@PlayerAccountMJ, 'MJordan21');

-- Difficulty
insert into [tblDifficulty] ([Difficultytype], [FkAdminId]) values
(
'Hard',
@AdminAccountJoe
)
declare @HardDifficulty bigint = scope_identity();

--Game
insert into [tblGame] ([Name], [FkAdminId], [FkDifficultyId], [MinBalloons], [MaxBalloons], [MaxMisses], [MaxDuration], [GameDateTime], [MaxThrows], [IsActive]) values
(
'Fun Game',
@AdminAccountJoe,
@HardDifficulty,
10,
15,
3,
20,
'2025-03-10 10:39:01',
18,
1
);
--Result Type
insert into [tblResultType] ([Id],[Name], [IsWin]) values
(
1,
'Stopped',
0
),
(
2,
'Running',
0
),
(
3,
'Won',
1
),
(
4,
'Killed',
0
),
(
5,
'Timed Out',
0
),
(
6,
'Exceeded Throws',
0
);

insert into [tblResult] ([FkGameId], [FkPlayerId], [Duration], [Misses], [BalloonsPopped], [FkResultTypeId]) values
(1, 3, 100, 2, 8, 1),
(1, 4, 110, 3, 6, 2),
(2, 3, 85, 1, 10, 1),
(2, 4, 90, 2, 9, 3);

select
	tblPlayer.Username, tblAccount.FirstName, tblAccount.LastName, tblAccount.Password
from
	tblAccount
inner join
	tblPlayer on tblAccount.Id = tblPlayer.FkAccountId
where
  tblPlayer.Username like '%M%';

select * from tblGame
inner join
tblDifficulty on tblGame.FkDifficultyId = tblDifficulty.Id
where FkDifficultyId = 2

select [tblDifficulty].DfficultyType as DifficultyName,
	   [tblGame].*
	from [tblDifficulty]
inner join
	[tblGame] on [tblDifficulty].Id = [tblGame].FkDifficultyId
order by
	[tblGame].FkDifficultyId,
	[tblGame].Name
;

select G.MaxDuration, G.MaxBalloons, G.MaxMisses, G.MaxThrows
from tblGame G
where G.Id = 3

select R.Duration,
	   R.Misses,
	   R.BalloonsPopped,
	   RT.Name,
	   RT.IsWin,
       P.Username
from [tblResultType] RT
inner join
[tblResult] R on RT.Id = R.FkResultTypeId
inner join
[tblPlayer] P on R.FkPlayerId = P.Username;

----------AI DATA --------------------
	

	
