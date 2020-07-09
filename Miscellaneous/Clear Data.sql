﻿BEGIN TRANSACTION T1
	BEGIN TRY
		--DELETE FROM DocumentData
		--DELETE FROM Document
		DELETE FROM CourseCohort
		DELETE FROM UserCourse
		DELETE FROM UserCohort
		DELETE FROM AspNetUserRoles
		DELETE FROM AspNetUserClaims
		DELETE FROM AspnetUsers
		DELETE FROM AspnetRoles
		DELETE FROM Course
		DELETE FROM Cohort
		DELETE FROM Program
		DELETE FROM ProgramLevel
		DELETE FROM Institution
		DELETE FROM [Address]
		DELETE FROM ContactInfo

		COMMIT TRANSACTION
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
