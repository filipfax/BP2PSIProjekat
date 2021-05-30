
Alter PROCEDURE ProveriInventar
AS
DECLARE Inventar_Cursor CURSOR
FOR SELECT KLC FROM [dbo].[SERVISNI_ALATI]
DECLARE
@i int;

BEGIN 
		OPEN Inventar_Cursor;

		
		FETCH NEXT FROM Inventar_Cursor
			INTO @i;
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @i<=0
				SELECT A.ALAT_ID, A.NAZ from [dbo].[SERVISNI_ALATI] A WHERE A.KLC <= @i
			FETCH NEXT FROM Inventar_Cursor
			INTO @i;
		END;

END;
CLOSE Inventar_Cursor;
DEALLOCATE Inventar_Cursor;
