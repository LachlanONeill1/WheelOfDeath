use [dbWheelofDeath];
go
-- ADMIN
insert into [tblAdminType] ([Name]) values 
('super'),
('standard');

insert into [tblAccount]([FirstName], [LastName], [Username], [Password],[isActive]) values(
'Joe',
'Smith',
'JSmithAdmin',
'WheelOfDeathAdmin1',
1);
declare @AdminAccountJoe bigint = scope_identity();

insert into [tblAdmin] ([FKAccountId],[FKAdminTypeId]) values(
@AdminAccountJoe,
1);

--PLAYER
insert into [tblAccount]([FirstName], [LastName], [Username], [Password],[isActive]) values(
'Michael',
'Jordan',
'MJordan21NBA',
'Password1',
1);
declare @PlayerAccountMJ bigint = scope_identity();

insert into [tblPlayer] ([FKAccountID]) values (
@PlayerAccountMJ);

-- Difficulty
insert into [tblDifficulty] ([Dfficultytype], [FKAdminId]) values
(
'Hard',
@AdminAccountJoe
)
declare @HardDifficulty bigint = scope_identity();

--Game
insert into [tblGame] ([Name], [FKAdminId], [FKDifficultyId], [Minballoons], [Maxballoons], [Maxmisses], [Maxduration], [Gamedatetime], [Maxthrows], [isActive]) values
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
insert into [tblResultType] ([Id],[Name], [isWin]) values
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


