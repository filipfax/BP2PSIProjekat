
CREATE FUNCTION OdrediRacun(
	@musid int
	) RETURNS INT
AS
BEGIN 
DECLARE
	@ukupnacena AS INT;
 SELECT @ukupnacena = SUM(P.CENA+ N.CENA) FROM (SELECT * FROM [dbo].[MOBILNI_TELEFONI] WHERE [dbo].[MOBILNI_TELEFONI].MUSTERIJAMUS_ID = @musid) M 
 JOIN [dbo].[OSTECENJA] O ON O.MOBILNI_TELEFONMOB_ID = M.MOB_ID
 JOIN [dbo].[POPRAVKAs] P ON P.OSTECENJEOST_ID = O.OST_ID
 JOIN [dbo].[NABAVKAs] N  ON N.POPRAVKAOSTECENJEOST_ID = P.OSTECENJEOST_ID
 RETURN @ukupnacena
END 

