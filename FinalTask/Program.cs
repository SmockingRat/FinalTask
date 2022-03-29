using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файла");
            string path = Console.ReadLine();
            BinaryReader(path);
            Console.ReadKey();

        }

        public static void BinaryReader(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    BinaryFormatter form = new BinaryFormatter();
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        Student[] newStudent = (Student[])form.Deserialize(fs);
                        foreach (Student stu in newStudent)
                        {
                            Console.WriteLine($"Name - {stu.Name}, Group - {stu.Group}, Birthday - {stu.DateOfBirth}");
                            string pathF = "C:\\Users\\Михаил\\Desktop\\Students\\"+stu.Group+".txt";
                            CreateTheFiles(pathF, stu);
                        }
                        Console.WriteLine("Files created");

                    }
                }
                catch(Exception message)
                {

                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine("User, file does not exist");

            }
        }

        public static void CreateTheFiles(string path, Student stu)
        {
            try
            {
                DirectoryInfo cr = new DirectoryInfo("C:\\Users\\Михаил\\Desktop\\Students");
                if(cr.Exists)
                {
                    FileInfo file = new FileInfo(path);
                    if(file.Exists)
                    {
                        using (StreamWriter wr = file.AppendText())
                            wr.WriteLine($"Name - {stu.Name}, Birthdate - {stu.DateOfBirth}");
                    }
                    else
                    {
                        using (StreamWriter wr = file.CreateText())
                            wr.WriteLine($"Name - {stu.Name}, Birthdate - {stu.DateOfBirth}");
                    }
                }
                else
                {
                    cr.Create();
                    CreateTheFiles(path, stu);
                }
            }
            catch(Exception message)
            {
                Console.WriteLine(message);
            }
        }

    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }


    }
}
