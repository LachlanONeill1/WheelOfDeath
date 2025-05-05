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
    [Username] nvarchar(100) not null,
    [Password] nvarchar(100) not null,
    [IsActive] bit not null constraint [Account-IsActive-Default-True] default(1)
);

create table [tblAdminType](
    [Id] bigint not null identity constraint [PkAdminType] primary key,
    [Name] nvarchar(255) not null
);

create table [tblPlayer](
    [FkAccountId] bigint not null identity constraint [PkPlayer] primary key constraint[FkPlayerAccount]foreign key references[tblAccount]([Id])
);

create table [tblAdmin](
    [FkAccountId] bigint not null identity constraint [PkAdmin] primary key constraint [FkAdminAccount] foreign key references[tblAccount]([Id]),
    [FkAdminTypeId] bigint not null constraint [FkAdminType] foreign key references[tblAdminType]([Id])
);
create table [tblDifficulty](
    [Id] bigint not null identity constraint [PkDifficulty] primary key,
    [DfficultyType] nvarchar(255) not null,
    [FkAdminId(createdby)] bigint not null
);
create table [tblGame](
    [Id] bigint not null identity constraint [PkGame] primary key,
    [Name] nvarchar(255) not null,
    [FkAdminId(createdby)] bigint not null constraint [FkGameCreatedby] foreign key references [tblAdmin]([FkAccountId]),
    [FkDifficultyId] bigint not null constraint [FkGameDifficulty] foreign key references [tblDifficulty]([Id]),
    [MinBalloons] smallint not null,
    [MaxBalloons] smallint not null,
    [MaxMisses] smallint not null,
    [MaxDuration] smallint not null,
    [GameDateTime] datetime not null,
    [MaxThrows] smallint not null,
    [IsActive] bit not null constraint [Game-IsActive-Default-True] default(1)
);

create table [tblResultType](
    [Id] bigint constraint [PkResultType] primary key not null,
    [Name] nvarchar(255) not null,
    [IsWin] bit not null,
    
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





