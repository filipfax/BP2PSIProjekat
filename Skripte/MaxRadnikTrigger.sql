/****** Script for SelectTopNRows command from SSMS  ******/
CREATE TRIGGER [MaxRadnikTrigger]
ON [dbo].[RADNICI]
instead of insert
AS
BEGIN
RAISERROR ('Ne prima se vise zaposlenih!',15, 1);
END