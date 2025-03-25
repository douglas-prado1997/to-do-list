CREATE DATABASE todolist;
GO

USE todolist;
GO
CREATE DATABASE todolist;
GO

USE todolist;
GO

CREATE TABLE [dbo].[users] (
    [id]            INT IDENTITY(1,1) NOT NULL,
    [name]          VARCHAR(500) NOT NULL,
    [email]         VARCHAR(500) NOT NULL UNIQUE,
    [password]      VARCHAR(300) NOT NULL,
	[is_sys_admin]  BIT NOT NULL,
    [creation_date] DATETIMEOFFSET(7) NOT NULL,
    [removed]       BIT NOT NULL,
    CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED ([id] ASC)
    WITH (
        PAD_INDEX = OFF, 
        STATISTICS_NORECOMPUTE = OFF, 
        IGNORE_DUP_KEY = OFF, 
        ALLOW_ROW_LOCKS = ON, 
        ALLOW_PAGE_LOCKS = ON, 
        OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
);
GO

INSERT INTO [dbo].[users] 
    ([name], [email], [password], [creation_date], [is_sys_admin], [removed])
VALUES 
    ('Douglas Prado', 'douglaspaiao1@gmail.com', '+pbj4WR2p0tVBTaRXRbKmsthi5KYEp0jftxr0tXP2b4=', SYSDATETIMEOFFSET(), 1, 0);
GO

CREATE TABLE [dbo].[tasks] (
    [id]            INT IDENTITY(1,1) NOT NULL,
    [name]          VARCHAR(500) NOT NULL,
    [description]   VARCHAR(1000) NOT NULL,
    [id_user]       INT NOT NULL,
    [create_date]   DATETIMEOFFSET(7) NOT NULL,
    [is_completed]  BIT NOT NULL DEFAULT 0, -- Coluna de conclusão
    [removed]       BIT NOT NULL, -- Coluna de remoção
    CONSTRAINT [PK_tasks] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tasks_users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[users]([id])
) ON [PRIMARY];
GO

INSERT INTO [dbo].[tasks] ([name], [description], [id_user], [create_date], [is_completed], [removed])
VALUES 
('Tarefa 1', 'Descrição da Tarefa 1', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 2', 'Descrição da Tarefa 2', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 3', 'Descrição da Tarefa 3', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 4', 'Descrição da Tarefa 4', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 5', 'Descrição da Tarefa 5', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 6', 'Descrição da Tarefa 6', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 7', 'Descrição da Tarefa 7', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 8', 'Descrição da Tarefa 8', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 9', 'Descrição da Tarefa 9', 1, SYSDATETIMEOFFSET(), 0, 0),
('Tarefa 10', 'Descrição da Tarefa 10', 1, SYSDATETIMEOFFSET(), 0, 0);
