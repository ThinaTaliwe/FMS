CREATE TABLE [dbo].[TRUCKS] (
    [ID]    VARCHAR (10) NOT NULL,
	[BRAND] VARCHAR (25) NOT NULL default ('Benz'),
    [LOAD]  INT          NOT NULL,
    [SPEED] INT          NOT NULL,
	[CLASS_CODE] varchar(10) 	NOT NULL default ('C1'),
    PRIMARY KEY  ([ID])
)

CREATE TABLE [dbo].[USERS] (
    [ID]        CHAR (13)     NOT NULL,
    [NAME]      VARCHAR (255) NOT NULL,
    [SURNAME]   VARCHAR (255) NOT NULL,
    [PASSWORD]  VARCHAR (255) NOT NULL,
    [EMAIL]     VARCHAR (255) NULL,
    [USER_TYPE] VARCHAR (15)  NOT NULL,
	[SALT] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[DRIVERS] (
    [ID]          CHAR (13)   NOT NULL,
    [CODE]        VARCHAR (3) NOT NULL,
    [FIRST_ISSUE] DATE        NOT NULL,
    [EXPIRY]      DATE        NOT NULL,
    [RESTRICTION] INT         NOT NULL,
    [MESSAGE]     VARCHAR(10),
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([ID]) REFERENCES [dbo].[USERS] ([ID])
);

CREATE TABLE [dbo].[CLIENTS] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [NAME]      VARCHAR (255) NOT NULL,
    [COMPANY]   VARCHAR (255) NOT NULL,
    [TELEPHONE] CHAR (12)     NOT NULL,
    [EMAIL]     VARCHAR (255) NOT NULL,
    [LOCATION]  VARCHAR (255) NOT NULL,
    PRIMARY KEY ([ID])
)

CREATE TABLE [dbo].[DELIVERY] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [ORDER_NUM]    VARCHAR (20)  NOT NULL,
    [TRUCK]        VARCHAR (10)  NOT NULL,
    [DRIVER]       CHAR (13)     NOT NULL,
    [CLIENT]       INT           NULL,
    [FROM]         VARCHAR (255) NOT NULL,
    [TO]           VARCHAR (255) NOT NULL,
    [DEPART_DAY]   DATETIME      NOT NULL,
    [DELIVERY_DAY] DATETIME      NULL,
    [MATERIAL]     VARCHAR (100) NOT NULL,
    [LOAD]         INT           NOT NULL,
    [AUTHORITY]    CHAR (13)     NOT NULL,
    [COMPLETED]    DATETIME      NULL,
    [ACCEPTED]     DATETIME      NULL,
    [STARTED]      DATETIME      NULL,
    [DISTANCE]     INT           DEFAULT ((0)) NULL,
	[CONFIRMATION] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([DRIVER]) REFERENCES [dbo].[DRIVERS] ([ID]),
    FOREIGN KEY ([DRIVER]) REFERENCES [dbo].[DRIVERS] ([ID]),
    FOREIGN KEY ([AUTHORITY]) REFERENCES [dbo].[USERS] ([ID]),
    FOREIGN KEY ([TRUCK]) REFERENCES [dbo].[TRUCKS] ([ID])
);



create table locations (
	[delivery] int not null,
	[driver] char(13) not null,
	[location] varchar(255) not null,
	[time] datetime not null,
	foreign key ([delivery]) references delivery,
	foreign key ([driver]) references drivers
);