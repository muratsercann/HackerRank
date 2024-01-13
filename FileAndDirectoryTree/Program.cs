using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        Directory root = new Directory()
        {
            Id = 0,
            ParentId = null,
            Name = "root",
            Childrens =
            {
                new Directory(){
                    Id = 1,
                    ParentId = 0,
                    Name = "D1" ,
                    Childrens = {new Directory(){ Id = 11, ParentId = 1, Name = "D11",
                                     Childrens={
                                            new Directory(){ Id = 111, ParentId = 11, Name = "D111" },
                                            new Directory(){ Id = 112, ParentId = 11, Name = "D112" },
                                            new Directory(){ Id = 113, ParentId = 11, Name = "D113" },
                                            new Directory(){ Id = 114, ParentId = 11, Name = "D114",Childrens={
                                                new Directory(){Id=1141, ParentId=114, Name="D1141"}
                                            } }
                                      }
                                },
                                 new Directory(){ Id = 12, ParentId = 1, Name = "D12",
                                                Childrens = {new Directory(){ Id = 121, ParentId = 12, Name = "D121" }}
                                                } }
                },
                new Directory(){
                    Id = 2,
                    ParentId = 0,
                    Name = "D2"
                },
                new Directory(){
                    Id = 3,
                    ParentId = 0,
                    Name = "D3",
                    Childrens={
                                            new Directory(){ Id = 31, ParentId = 3, Name = "D31" },
                                            new Directory(){ Id = 32, ParentId = 3, Name = "D32" },
                                            new Directory(){ Id = 33, ParentId = 3, Name = "D33" },
                                            new Directory(){ Id = 34, ParentId = 3, Name = "D34" }
                                      }
                }
            }

        };


        ;

        int count = 0;
        int? maxDepth = 0;
        var dir = CreateRandomDirectory(-1, ref count, ref maxDepth);

        WriteDirectoryTree(root, 0, 100, "├");

        /*Console.WriteLine("max depth : " + (maxDepth+1));
        int number;
        do
        {
            Console.Write("max depth : ");
            string? input = Console.ReadLine();
            int.TryParse(input, out number);

            WriteDirectoryTree(dir, 0, number);

        } while (number != 0);*/

    }


    public static Directory CreateRandomDirectory(int? parentId, ref int count, ref int? maxDepth, int depth = 0)
    {
        var dir = new Directory();
        dir.Name = count.ToString() + "(" + parentId + ")";
        dir.Id = count;
        dir.ParentId = parentId;
        count++;
        if (maxDepth < parentId)
            maxDepth = parentId;
        //Console.WriteLine($"count : {count},Id : {dir.Id}, parent : {dir.ParentId} name : {dir.Name} ");

        if (count > 6)
        {
            return dir;
        }

        var random = new Random();
        var ch = random.Next(0, 5);

        for (int j = 0; j < 3; j++)
        {
            if (count > 50) break;
            var r = CreateRandomDirectory(dir.Id, ref count, ref maxDepth, depth++);
            dir.Childrens.Add(r);
        }
        depth++;
        return dir;
    }
    public static void WriteDirectoryTree(Directory dir, int depth, int lastDepth,string linkChar, string prefix = "")
    {
        var depthPlus = 4;
        //Console.WriteLine($"prefix : {prefix}");
        string text = $"{dir.Name} c:{dir.Childrens.Count}";
        //└
        if (depth > 0)
            text = prefix.Substring(0, prefix.Length - depthPlus) + linkChar + "".PadRight(depthPlus-1, '─') + text;

        Console.WriteLine(text);

        for (int i = 0; i < dir.Childrens.Count; i++)
        {
            var t = "";
            var linkCh = "";
            if (i != dir.Childrens.Count - 1)
            {
                t = prefix + "│".PadRight(depthPlus,' ');
                linkCh = "├";
            }
            else {
                t = prefix + "".PadRight(depthPlus, ' ');
                linkCh = "└";
            }

            if (depth + depthPlus <= lastDepth * depthPlus)
                WriteDirectoryTree(dir.Childrens[i], depth + depthPlus, lastDepth,linkCh, t);
        }

    }
}


public class Directory
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Node { get; set; }

    public List<Directory> Childrens { get; set; } = new List<Directory>();

}