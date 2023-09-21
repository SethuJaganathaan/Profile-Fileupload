--Inserting Info table--
INSERT INTO users (UserId, Username, Email, ProfilePicture)
VALUES ('E6245BF5-C580-459C-BF3D-AC8FCF7DD9C4', 'Dummy', 'dummy@example.com', null);

DECLARE @InformationId UNIQUEIDENTIFIER = NEWID();
DECLARE @UserId UNIQUEIDENTIFIER = '050CE1C2-0CD9-4141-B3EF-9E76336A023F';
DECLARE @Name NVARCHAR(50) = 'Dummy Text';
DECLARE @Description NVARCHAR(100) = 'Its a Text file';

DECLARE @FilePath NVARCHAR(MAX) = 'D:\download.txt';

DECLARE @FileContent VARBINARY(MAX);

DECLARE @Sql NVARCHAR(MAX);
SET @Sql = N'
    DECLARE @FileContent VARBINARY(MAX);
    SELECT @FileContent = BulkColumn
    FROM OPENROWSET(BULK N''' + @FilePath + ''', SINGLE_BLOB) AS Contents;

    INSERT INTO information (InformationId, UserId, Name, InfoFile, Description)
    VALUES (@InformationId, @UserId, @Name, @FileContent, @Description);';

EXEC sp_executesql @Sql, 
    N'@InformationId UNIQUEIDENTIFIER, @UserId UNIQUEIDENTIFIER, @Name NVARCHAR(50), @Description NVARCHAR(100)',
    @InformationId, @UserId, @Name, @Description;


--inserting User Details--
DECLARE @UseriId UNIQUEIDENTIFIER = NEWID();
DECLARE @Username NVARCHAR(50) = 'Luffy';
DECLARE @Email NVARCHAR(100) = 'luffy@gmail.com'
DECLARE @Password NVARCHAR(50) = 'Luffy@123'
DECLARE @ProfilePicture VARBINARY(MAX);

SET @ProfilePicture = (SELECT BulkColumn FROM OPENROWSET(BULK N'D:\kidluffy.jpg', SINGLE_BLOB) AS x);

IF SUBSTRING(@ProfilePicture, 1, 2) = 0x8950
    OR SUBSTRING(@ProfilePicture, 1, 2) = 0xFFD8
BEGIN
	INSERT INTO [Users] (UserId, Username, Email, Password, ProfilePicture) 
	VALUES (@UseriId, @Username, @Email, @Password, @ProfilePicture)
END
ELSE
BEGIN
    PRINT 'Only PNG and JPG formats are allowed.';
END;