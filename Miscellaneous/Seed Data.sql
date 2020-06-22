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
		
		DECLARE @id_ContactIfor_1 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_ContactIfor_2 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_ContactIfor_3 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Address_1 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Address_2 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Address_3 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Institution_1 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Institution_2 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Institution_3 UNIQUEIDENTIFIER = NEWID()

		INSERT INTO ContactInfo (Id, Email, Fax, Phone)
		VALUES	(@id_ContactIfor_1, 'usj@usj.com.lb', '96179029376','9614567567' ),
				(@id_ContactIfor_2, 'inci@inciusj.com.lb', '9615647333','96198765423' ),
				(@id_ContactIfor_3, 'ffm@inciusj.com.lb', '96156765234','9619876567' )
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT
		
		INSERT INTO [Address] (Id, CityAr, CityFr, CountryAr, CountryFr, StreetAr, StreetFr, Latitude, Longitude )
		VALUES	(@id_Address_1, N'بيروت', 'Beyrouth', N'لبنان', 'Liban', N'شارع جامعة القديس يوسف', 'Rue Université Saint Joseph', 33.88894, 35.49442 ),
				(@id_Address_2, N'اينسي بيروت', 'INCI Beyrouth', N'اينسي لبنان', 'INCI Liban', N'اينسي شارع جامعة القديس يوسف', 'INCI Rue Université Saint Joseph', 34.88894, 36.49442 ),
				(@id_Address_3, N'اففم بيروت', 'FFM Beyrouth', N'اففم لبنان', 'FFM Liban', N'اففم شارع جامعة القديس يوسف', 'FFM Rue Université Saint Joseph', 35.88894, 37.49442 )
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT
		
		INSERT INTO Institution (Id, NameAr, NameEn, NameFr, Code, AddressId, ContactInfoId, ParentId)
		VALUES	(@id_Institution_1, N'جامعة القديس يوسف في بيروت', 'Saint Joseph University of Beirut', 'Université Saint Joseph de Beyrouth', 'USJ', @id_Address_1, @id_ContactIfor_1, NULL),
				(@id_Institution_2, N'اينسي جامعة القديس يوسف في بيروت', 'INCI Saint Joseph University of Beirut', 'INCI Université Saint Joseph de Beyrouth', 'INCI', @id_Address_2, @id_ContactIfor_2, @id_Institution_1),
				(@id_Institution_3, N'اففم جامعة القديس يوسف في بيروت', 'FFM Saint Joseph University of Beirut', 'FFM Université Saint Joseph de Beyrouth', 'FFM', @id_Address_3, @id_ContactIfor_3, @id_Institution_1)
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		COMMIT TRANSACTION
	
		SELECT CAST(@RowsTotal AS VARCHAR(100)) + ' rows added'
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