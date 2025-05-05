use [dbWheelofDeath]
go

insert into [tblAccount] ([FirstName], [LastName], [Password], [IsActive]) values
('John', 'Doe', 'password123', 1),
('Jane', 'Smith', 'password456', 1),
('Robert', 'Brown', 'password789', 1),
('Emily', 'Davis', 'password000', 1),
('Michael', 'Wilson', 'password111', 1);

-- Insert dummy data into tblAdminType
insert into [tblAdminType] ([Name]) values
('SuperAdmin'),
('GameMaster'),
('Moderator');

-- Insert dummy data into tblPlayer
insert into [tblPlayer] ([FkAccountId], [Username]) values
(1, 'johnnyD'),
(2, 'janeS'),
(3, 'robertB'),
(4, 'emilyD'),
(5, 'mikeW');

-- Insert dummy data into tblAdmin
insert into [tblAdmin] ([FkAccountId], [FkAdminTypeId], [Username]) values
(1, 1, 'superAdminUser'),
(2, 2, 'gameMaster1'),
(3, 3, 'moderatorX');

-- Insert dummy data into tblDifficulty
insert into [tblDifficulty] ([DifficultyType], [FkAdminId]) values
('Easy', 1),
('Medium', 2),
('Hard', 3);

-- Insert dummy data into tblGame
insert into [tblGame] ([Name], [FkAdminId], [FkDifficultyId], [MinBalloons], [MaxBalloons], [MaxMisses], [MaxDuration], [GameDateTime], [MaxThrows], [IsActive]) values
('BalloonPop Classic', 1, 1, 5, 10, 3, 30, '2025-04-01 10:00:00', 10, 1),
('BalloonPop Challenge', 2, 2, 8, 12, 2, 20, '2025-04-01 12:00:00', 8, 1),
('BalloonPop Ultimate', 3, 3, 10, 15, 1, 15, '2025-04-01 14:00:00', 5, 1);

-- Insert dummy data into tblResultType
insert into [tblResultType] ([Name], [IsWin]) values
('Victory', 1),
('Defeat', 0);

-- Insert dummy data into tblResult
insert into [tblResult] ([FkGameId], [FkPlayerId], [Duration], [Misses], [BalloonsPopped], [FkResultTypeId]) values
(1, 1, 15, 1, 9, 1),
(1, 2, 18, 3, 7, 0),
(2, 3, 12, 2, 10, 1),
(3, 4, 10, 1, 12, 1),
(2, 5, 25, 4, 5, 0);

-- Insert dummy data into tblConfig
insert into [tblConfig] ([ResultsShown]) values
(10);