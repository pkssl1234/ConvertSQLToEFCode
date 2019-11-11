using System;
using System.Collections.Generic;

namespace ConvertSQLToEFCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin Time: " + DateTime.Now);
            var SqlLines = System.IO.File.ReadAllLines(@"C:\Source\ToEFCode.sql");
            string filePath = @"C:\Source\ConvertSQLToEFCodeResult.txt";
            System.IO.StreamWriter sw;
            //if (!System.IO.File.Exists(filePath))
            //{
            sw = System.IO.File.CreateText(filePath);
            //}


            string fianlResult;
            //INSERT[dbo].[Counties]([CountyId], [Name], [PhoneAreaNo], [DisplaySeq], [CreateUserId], [CreateTime], [UpdateUserId], [UpdateTime]) VALUES(N'CH', N'彰化縣', N'04', 110, N'System', CAST(N'2019-10-28 10:42:53.0600000' AS DateTime2), NULL, NULL)            
            //modelBuilder.Entity<UserGroupFuncProgram>().HasData(new UserGroupFuncProgram { UserGroupId = "System", FuncProgramId = ProgramId.TrainStations.ToString() });

            foreach (string line in SqlLines)
            {
                List<string> row = new List<string>();
                List<string> col = new List<string>();
                // Use a tab to indent each line of the file.
                var EFcode = line.Replace("[dbo].", "").Trim().Split("VALUES");
                //= line.Replace("INSERT", "modelBuilder.Entity");
                foreach (var result in EFcode)
                {
                    if (result.Contains("INSERT"))
                    {
                        var Insert = result.Replace("INSERT", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Replace(",", "").Trim().Split(" ");
                        foreach (var subResult in Insert)
                        {
                            //Console.WriteLine(subResult);
                            row.Add(subResult);
                        }
                    }
                    else
                    {
                        var Else = result.Replace("N'", "'").Replace("(", "").Replace(")", "").Split(",");
                        foreach (var subResult in Else)
                        {
                            if (subResult.Contains("DateTime2"))
                            {
                                //var newSubResult = subResult.Replace("CAST", "").Replace(" AS DateTime2", "").Replace("'", @"""");
                                //Console.WriteLine(newSubResult);
                                //col.Add(newSubResult);
                                col.Add("DateTime.Now");
                            }
                            else
                            {
                                if (subResult.Contains("NULL"))
                                    col.Add(subResult.ToLower().Replace("'", "\""));
                                else
                                    col.Add(subResult.Replace("'", "\""));
                            }
                        }
                    }
                }
                row[0] = row[1].Replace("Id", "");
                fianlResult = string.Format("modelBuilder.Entity<{0}>().HasData(new {0} ", row[0]);
                fianlResult = fianlResult + "{";
                for (int i = 1; i < row.Count - 1; i++)
                {
                    fianlResult = fianlResult + string.Format("{0} = {1}, ", row[i], col[i - 1]);
                }
                fianlResult = fianlResult + string.Format("{0} = {1}", row[row.Count - 1], col[col.Count - 1]);
                fianlResult = fianlResult + "});\n";
                //Console.WriteLine(fianlResult);
                sw.Write(fianlResult);
            }
            sw.Close();
            Console.WriteLine("Done");
            Console.WriteLine("End Time: " + DateTime.Now);
            Console.ReadLine();
        }
    }
}
