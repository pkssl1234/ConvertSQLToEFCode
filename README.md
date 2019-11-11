# ConvertSQLToEFCode

Step

1.create a folder at C://Source.

2.Make a SQL file and named ToEFCode.sql.

3.input T-SQL statement(Only for INSERT statement).

Example:

input(from MS SQL Server):

INSERT [dbo].[FuncPrograms] ([FuncProgramId], [FuncSubGroupId], [Name], [Url], [DisplaySeq]) VALUES (N'Airports', N'testSub', N'Airports', N'/Airports', 1401)

result

modelBuilder.Entity<FuncProgram>().HasData(new FuncProgram {FuncProgramId =  "Airports", FuncSubGroupId =  "testSub", Name =  "Airports", Url =  "/Airports", DisplaySeq =  1401});
