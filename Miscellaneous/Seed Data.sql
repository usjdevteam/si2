BEGIN TRANSACTION T1
	BEGIN TRY
		DECLARE @RowsTotal  INT = 0

		DECLARE @Id_Role_SuperAdmin UNIQUEIDENTIFIER = NEWID()
		DECLARE @Id_Role_Administrator UNIQUEIDENTIFIER = NEWID()
		DECLARE @Id_Role_User UNIQUEIDENTIFIER = NEWID()
		INSERT INTO AspNetRoles
		(Id,						Name,				NormalizedName,		ConcurrencyStamp)
		Values
		(@Id_Role_SuperAdmin,		'SuperAdmin',		'SUPSERADMIN',		NEWID()),
		(@Id_Role_Administrator,	'Administrator',	'ADMINISTRATOR',	NEWID()),
		(@Id_Role_User,				'User',				'USER',				NEWID())
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		DECLARE @Id_User_SuperAdmin UNIQUEIDENTIFIER = NEWID() -- password Super_123
		DECLARE @Id_User_Administrator1 UNIQUEIDENTIFIER = NEWID() -- password Super_123
		DECLARE @Id_User_User1 UNIQUEIDENTIFIER = NEWID() -- password Super_123
		
		INSERT INTO AspNetUsers
		(ID,		UserName,				NormalizeduserName,		Email,					NormalizedEmail,		PasswordHash,	
		SecurityStamp, ConcurrencyStamp,	 EmailConfirmed, PhoneNumberConfirmed,	TwoFactorEnabled,	LockoutEnabled, AccessFailedCount, FirstNameAr, FirstNameFr, LastNameAr, LastNameFr)
		VALUES
		(@Id_User_SuperAdmin,	'superadmin@mailinator.com',	'SUPERADMIN@MAILINATOR.COM',   'superadmin@mailinator.com',	'SUPERADMIN@MAILINATOR.COM',	'AQAAAAEAACcQAAAAEO0WixeZWeD5CFlJuskzYLYd+52mZgaHvJZM19TPL3vzYv501aT2QwzUS4XxrCFJQw==', 
		NEWID(),		NEWID(),		1,				0,						0,					0,				0,				N'سوبر',			'Super',		N'أدمن',		'Admin'),
		(@Id_User_Administrator1,	'administrator1@mailinator.com',	'ADMINISTRATOR1@MAILINATOR.COM',   'administrator1@mailinator.com',	'ADMINISTRATOR1@MAILINATOR.COM',	'AQAAAAEAACcQAAAAEO0WixeZWeD5CFlJuskzYLYd+52mZgaHvJZM19TPL3vzYv501aT2QwzUS4XxrCFJQw==', 
		NEWID(),		NEWID(),		1,				0,						0,					0,				0,				N'أدمنيسترتر',			'Admnistrator',		N'واحد',		'One'),
		(@Id_User_User1,	'user1@mailinator.com',	'USER1@MAILINATOR.COM',   'user1@mailinator.com',	'USER1@MAILINATOR.COM',	'AQAAAAEAACcQAAAAEO0WixeZWeD5CFlJuskzYLYd+52mZgaHvJZM19TPL3vzYv501aT2QwzUS4XxrCFJQw==', 
		NEWID(),		NEWID(),		1,				0,						0,					0,				0,				N'يوزر',			'User',		N'واحد',		'One')
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT
		
		INSERT INTO AspNetUserRoles (RoleId, UserId)
		VALUES	(@Id_Role_SuperAdmin, @Id_User_SuperAdmin),
				(@Id_Role_Administrator, @Id_User_Administrator1),
				(@Id_Role_User, @Id_User_User1)
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT
		
		BULK
		INSERT [Address]
		FROM 'C:/Users/Administrator/source/repos/si2_sprint2/Miscellaneous/address.csv'
		WITH
		(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		CODEPAGE = '65001'
		)

		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		BULK
		INSERT ContactInfo
		FROM 'C:/Users/Administrator/source/repos/si2_sprint2/Miscellaneous/contactInfo.csv'
		WITH
		(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		CODEPAGE = '65001'
		)

		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		BULK
		INSERT Institution
		FROM 'C:/Users/Administrator/source/repos/si2_sprint2/Miscellaneous/institutions.csv'
		WITH
		(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		CODEPAGE = '65001'
		)

		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		BULK
		INSERT ProgramLevel
		FROM 'C:/Users/Administrator/source/repos/si2_sprint2/Miscellaneous/programLevel.csv'
		WITH
		(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		CODEPAGE = '65001'
		)

		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		BULK
		INSERT Program
		FROM 'C:/Users/Administrator/source/repos/si2_sprint2/Miscellaneous/programs.csv'
		WITH
		(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		CODEPAGE = '65001'
		)

		SET @RowsTotal = @RowsTotal + @@ROWCOUNT
		
		BULK
		INSERT Course
		FROM 'C:/Users/Administrator/source/repos/si2_sprint2/Miscellaneous/courses.csv'
		WITH
		(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		CODEPAGE = '65001'
		)

		COMMIT TRANSACTION
	
		SELECT CAST(@RowsTotal AS VARCHAR(100)) + ' rows added'

		UPDATE Institution set ParentId = null where code = 'USJ'

END TRY
BEGIN CATCH 
	IF (@@TRANCOUNT > 0)
		ROLLBACK TRANSACTION T1
	SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage
END CATCH