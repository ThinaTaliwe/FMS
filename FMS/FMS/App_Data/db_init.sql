﻿/*
Deployment script for C:\USERS\MMELI\DESKTOP\SCHOOL\IFM3A\TEAM43\FMS\FMS\APP_DATA\DBFMS.MDF

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/

GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END

SET DatabaseName "C:\Users\Khanyisile\Desktop\team43\FMS\FMS\App_Data\FMSDB_1.MDF";

GO
USE [$(DatabaseName)];


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[CLIENTS]...';


GO
CREATE TABLE [dbo].[CLIENTS] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [NAME]      VARCHAR (255) NOT NULL,
    [COMPANY]   VARCHAR (255) NOT NULL,
    [TELEPHONE] CHAR (12)     NOT NULL,
    [EMAIL]     VARCHAR (255) NOT NULL,
    [LOCATION]  VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[DELIVERY]...';


GO
CREATE TABLE [dbo].[DELIVERY] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [ORDER_NUM]    VARCHAR (20)  NOT NULL,
    [TRUCK]        VARCHAR (10)  NOT NULL,
    [DRIVER]       VARCHAR (13)  NOT NULL,
    [CLIENT]       INT           NULL,
    [FROM]         VARCHAR (255) NOT NULL,
    [TO]           VARCHAR (255) NOT NULL,
    [DEPART_DAY]   DATETIME      NOT NULL,
    [DELIVERY_DAY] DATETIME      NULL,
    [MATERIAL]     VARCHAR (100) NOT NULL,
	[LOAD] 		   INT 			 NOT NULL,
	[LOCATION] 		   VARCHAR(100)   NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[DRIVERS]...';


GO
CREATE TABLE [dbo].[DRIVERS] (
    [ID]          VARCHAR (13) NOT NULL,
    [CODE]        VARCHAR (3)  NOT NULL,
    [FIRST_ISSUE] DATE         NOT NULL,
    [EXPIRY] DATE         NOT NULL,
    [RESTRICTION] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[TRUCKS]...';


GO
CREATE TABLE [dbo].[TRUCKS] (
    [ID]    VARCHAR (10) NOT NULL,
    [LOAD]  INT          NOT NULL,
    [SPEED] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[USERS]...';


GO
CREATE TABLE [dbo].[USERS] (
    [Id]        VARCHAR (13)  NOT NULL,
    [NAME]      VARCHAR (255) NOT NULL,
    [SURNAME]   VARCHAR (255) NOT NULL,
    [PASSWORD]   VARCHAR (255) NOT NULL,
    [EMAIL]     VARCHAR (255) NULL,
    [USER_TYPE] VARCHAR (15)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating unnamed constraint on [dbo].[DELIVERY]...';


GO
ALTER TABLE [dbo].[DELIVERY] WITH NOCHECK
    ADD FOREIGN KEY ([TRUCK]) REFERENCES [dbo].[TRUCKS] ([ID]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating unnamed constraint on [dbo].[DELIVERY]...';


GO
ALTER TABLE [dbo].[DELIVERY] WITH NOCHECK
    ADD FOREIGN KEY ([DRIVER]) REFERENCES [dbo].[DRIVERS] ([ID]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating unnamed constraint on [dbo].[DELIVERY]...';


GO
ALTER TABLE [dbo].[DELIVERY] WITH NOCHECK
    ADD FOREIGN KEY ([CLIENT]) REFERENCES [dbo].[CLIENTS] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating unnamed constraint on [dbo].[DRIVERS]...';


GO
ALTER TABLE [dbo].[DRIVERS] WITH NOCHECK
    ADD FOREIGN KEY ([ID]) REFERENCES [dbo].[USERS] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'The transacted portion of the database update succeeded.'
COMMIT TRANSACTION
END
ELSE PRINT N'The transacted portion of the database update failed.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.DELIVERY'), OBJECT_ID(N'dbo.DRIVERS'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO
