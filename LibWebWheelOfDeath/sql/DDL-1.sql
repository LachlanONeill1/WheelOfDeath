use [master];
go

drop database if exists [dbWheelofDeath];
go

create database [dbWheelofDeath];
go

use [dbWheelofDeath];
go

create table [tblAccount](
    [Id] bigint not null identity constraint [PkAccount] primary key,
    [FirstName] nvarchar(100) not null,
    [LastName] nvarchar(100) not null,
    [Password] nvarchar(100) not null,
    [IsActive] bit not null constraint [Account-IsActive-Default-True] default(1)
);

create table [tblAdminType](
    [Id] bigint not null identity constraint [PkAdminType] primary key,
    [Name] nvarchar(255) not null,
    constraint [AdminType_Name_Unique] unique([Name])
);

create table [tblPlayer](
    [FkAccountId] bigint not null constraint [PkPlayer] primary key constraint[FkPlayerAccount]foreign key references[tblAccount]([Id]),
    [Username] nvarchar(100) not null,
    constraint [Player_Username_Unique] unique([Username])
);

create table [tblAdmin](
    [FkAccountId] bigint not null constraint [PkAdmin] primary key constraint [FkAdminAccount] foreign key references[tblAccount]([Id]),
    [FkAdminTypeId] bigint not null constraint [FkAdminType] foreign key references[tblAdminType]([Id]),
    [Username] nvarchar(100) not null,
    constraint [Admin_Username_Unique] unique([Username])
);
create table [tblDifficulty](
    [Id] bigint not null identity constraint [PkDifficulty] primary key,
    [DifficultyType] nvarchar(255) not null,
    [FkAdminId] bigint not null,
    constraint [DifficultyType_Name_Unique] unique([DifficultyType])
);
create table [tblGame](
    [Id] bigint not null identity constraint [PkGame] primary key,
    [Name] nvarchar(255) not null,
    [FkAdminId] bigint not null constraint [FkGameCreatedby] foreign key references [tblAdmin]([FkAccountId]),
    [FkDifficultyId] bigint not null constraint [FkGameDifficulty] foreign key references [tblDifficulty]([Id]),
    [MinBalloons] smallint not null,
    [MaxBalloons] smallint not null,
    [MaxMisses] bigint not null,
    [MaxDuration] bigint not null,
    [GameDateTime] datetime not null,
    [MaxThrows] smallint not null,
    [IsActive] bit not null constraint [Game-IsActive-Default-True] default(1),
    constraint [Game_Name_Unique] unique([Name])
);
create index [idxFkAdminId] on [tblGame]([FkAdminId])
create index [idxFkDifficultyId] on [tblGame]([FkDifficultyId])

create table [tblResultType](
    [Id] bigint constraint [PkResultType] primary key not null,
    [Name] nvarchar(255) not null,
    [IsWin] bit not null,
    constraint [ResultType_Name_Unique] unique([Name])
    
);
create table [tblResult](
    [Id] bigint not null identity constraint [PkResult] primary key  ,
    [FkGameId] bigint not null constraint[FkGameResult] foreign key references [tblGame]([Id]),
    [FkPlayerId] bigint not null,
    [Duration] smallint not null,
    [Misses] smallint not null,
    [BalloonsPopped] smallint not null,
    [FkResultTypeId] bigint not null constraint [FkResultResultType] foreign key references [tblResultType]([Id])
);

create table [tblConfig](
	[Id] bigint not null identity primary key,
	[ResultsShown] bigint not null constraint[DefaultResultsShown] default (10) 
);
create index [idxFkGameId] on [tblResult]([FkGameId])
create index [idxFkPlayerId] on [tblResult]([FkPlayerId])
create index [idxFkResultTypeId] on [tblResult]([FkResultTypeId])


select 
    count(*) as ResultCount,
    R.[FkPlayerId], 
    R.[Duration], 
    R.[Misses], 
    R.[BalloonsPopped], 
    G.[Name] as GameName,
    P.[Username] as PlayerName
from tblResult R
inner join tblGame G on R.FkGameId = G.Id
inner join tblPlayer P on R.FkPlayerId = P.FkAccountId
group by 
    R.[FkPlayerId], 
    R.[Duration], 
    R.[Misses], 
    R.[BalloonsPopped], 
    G.[Name], 
    P.[Username];



SELECT 
    A.Id,
    P.Username,
    A.FirstName,
    A.LastName,
    A.IsActive
FROM tblPlayer P
inner join tblAccount A ON P.FkAccountId = a.Id
ORDER BY P.Username;




