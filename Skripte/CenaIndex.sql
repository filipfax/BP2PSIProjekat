/****** Script for SelectTopNRows command from SSMS  ******/
CREATE INDEX [CenaIndex] ON [dbo].[NABAVKAs] (POPRAVKAOSTECENJEOST_ID, CENA)

DROP INDEX [CenaIndex] ON [dbo].[NABAVKAs]