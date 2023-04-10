CREATE LOGIN devuser WITH PASSWORD = 'Blitz#8888';
GO

CREATE USER devuser FOR LOGIN devuser;  
GO 

grant update, insert, select, delete on schema :: dbo to devuser;
