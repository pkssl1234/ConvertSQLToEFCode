# ConvertSQLToEFCode
First create a folder at C://Source.
Make a SQL file and named ToEFCode.sql.
Only for INSERT statement.

Example:

input(from MS SQL Server):

INSERT [dbo].[FuncPrograms] ([FuncProgramId], [FuncSubGroupId], [Name], [Url], [DisplaySeq]) VALUES (N'Airports', N'testSub', N'Airports', N'/Airports', 1401)

result
modelBuilder.Entity<FuncProgram>().HasData(new FuncProgram {FuncProgramId =  "Airports", FuncSubGroupId =  "testSub", Name =  "Airports", Url =  "/Airports", DisplaySeq =  1401});
