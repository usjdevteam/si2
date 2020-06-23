BEGIN TRANSACTION T1
	BEGIN TRY
		DECLARE @RowsTotal  INT = 0
	
		DECLARE @Id_Role_SuperAdmin UNIQUEIDENTIFIER = NEWID()
		DECLARE @Id_Role_Administrator UNIQUEIDENTIFIER = NEWID()
		DECLARE @Id_Role_User UNIQUEIDENTIFIER = NEWID()
		
		SET @RowsTotal = @RowsTotal + @@ROWCOUNT

		DECLARE @Id_User_SuperAdmin UNIQUEIDENTIFIER = NEWID() -- password Super_123
		DECLARE @Id_User_Administrator1 UNIQUEIDENTIFIER = NEWID() -- password Super_123
		DECLARE @Id_User_User1 UNIQUEIDENTIFIER = NEWID() -- password Super_123
		
		DECLARE @id_ContactIfor_1 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_ContactIfor_2 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_ContactIfor_3 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Address_1 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Address_2 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Address_3 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Institution_1 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Institution_2 UNIQUEIDENTIFIER = NEWID()
		DECLARE @id_Institution_3 UNIQUEIDENTIFIER = NEWID()
		
		--BULK
		--INSERT Institution
		--FROM 'C:/Users/Administrator/source/repos/si2/Miscellaneous/get institutions.csv'
		--WITH
		--(
		--FIELDTERMINATOR = ',',
		--ROWTERMINATOR = '\n'
		--)

		$CSV = Import-Csv "get institutions.csv"

		$SQLServer   = "dbserver.corp.company.tld"
		$SQLDatabase = "database"

		--# Set up a string format template
		$InsertTemplateAddress = "INSERT INTO Address([id], [StreetFr], [StreetAr], [NameAr] , [NameEn]) VALUES (NEWID(),'{0}','{1}','{2}','{3}','{4}')"
		$InsertTemplateContactInfo = "INSERT INTO Institution([id], [Code], [NameFr], [NameAr] , [NameEn]) VALUES (NEWID(),'{0}','{1}','{2}','{3}','{4}')"
		$InsertTemplateInstitution = "INSERT INTO Institution([id], [Code], [NameFr], [NameAr] , [NameEn]) VALUES (NEWID(),'{0}','{1}','{2}','{3}','{4}')"

		--# Generate all insert statements and store in string array
		$AllInserts = foreach($Row in $CSV){
			$InsertTemplate -f $Row.id,$Row.'First Name',$Row.'Last Name'
		}

		--# Split array into an array of 1000 (or fewer) string arrays
		$RowArrays = for($i=0; $i -lt $AllInserts.Length; $i+=1000){
			,@($AllInserts[$i..($i+999)])
		}

		--# Foreach array of 1000 (or less) insert statements, concatenate them with a new line and invoke it
		foreach($RowArray in $RowArrays){
			$Query = $RowArray -join [System.Environment]::NewLine
			Invoke-QsSqlQuery -query $Query -SQLServer $SQLServer -database $SQLDatabase
		}

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