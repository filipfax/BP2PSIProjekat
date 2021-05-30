/****** Script for SelectTopNRows command from SSMS  ******/
ALTER PROCEDURE ServiserPopravkaOstecenjeJoin
	@mbr int
AS
BEGIN TRY
 SELECT S.*, P.*, O.* FROM [dbo].[RADNICI] S
 JOIN [dbo].[POPRAVKAs] P ON P.SERVISERMBR = S.MBR 
 JOIN [dbo].[OSTECENJA] O ON P.OSTECENJEOST_ID = O.OST_ID
 WHERE P.SERVISERMBR =@mbr
END TRY

BEGIN CATCH
	SELECT
	ERROR_NUMBER() AS ErrorNumber
	,ERROR_MESSAGE() AS ErrorMessage;
END CATCH
