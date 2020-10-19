using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Lab4Application
{
    class Program
    {
        private static CloudTable studentsTable;
        private static CloudTableClient tableClient;
        private static TableOperation tableOperation;
        private static TableResult tableResult;
        private static List<StudentEntity> students  = new List<StudentEntity>();
        static void Main(string[] args)
        {
            Task.Run(async () => { await Initialize(); })
                .GetAwaiter()
                .GetResult();
        }
        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
            +"AccountName=datcstorage;"
            +"AccountKey=gF+0yY8DH+XEvpP8WBf1+DJiKq2u68yFcs8b8nLTR9uQlIA2MBzBX2PK0QoZt6CCaFhd5MaMh4aOzKsWdaMj2A==;"
            +"EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("Students");

            await studentsTable.CreateIfNotExistsAsync();
            
            int option = -1;
            do
            {
                System.Console.WriteLine("1.Adauga student.");
                System.Console.WriteLine("2.Update student.");
                System.Console.WriteLine("3.Sterge student.");
                System.Console.WriteLine("4.Arata toti studentii.");
                System.Console.WriteLine("5.Iesi");
                System.Console.WriteLine("Optiunea ta este : ");
                string opt = System.Console.ReadLine();
                option =Int32.Parse(opt);
                switch(option)
                {
                    case 1:
                        await AddNewStudent();
                        break;
                    case 2:
                        await EditStudent();
                        break;
                    case 3:
                        await DeleteStudent();
                        break;
                    case 4:
                        await DisplayStudents();
                        break;
                    case 5:
                        System.Console.WriteLine("Thanks !");
                        break;
                }
            }while(option != 5);
            
        }
        private static async Task<StudentEntity> RetrieveRecordAsync(CloudTable table,string partitionKey,string rowKey)
        {
            tableOperation = TableOperation.Retrieve<StudentEntity>(partitionKey, rowKey);
            tableResult = await table.ExecuteAsync(tableOperation);
            return tableResult.Result as StudentEntity;
        }
        private static async Task AddNewStudent()
        {
            System.Console.WriteLine("Adauga universitate");
            string university = Console.ReadLine();
            System.Console.WriteLine("Adauga cnp:");
            string cnp = Console.ReadLine();
            System.Console.WriteLine("Adauga prenume");
            string firstName = Console.ReadLine();
            System.Console.WriteLine("Adauga numele de familie");
            string lastName = Console.ReadLine();
            System.Console.WriteLine("Adauga facultatea");
            string faculty = Console.ReadLine();
            System.Console.WriteLine("Adauga an studiu");
            string year = Console.ReadLine();

            StudentEntity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
            if(stud == null)
            {
                var student = new StudentEntity(university,cnp);
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Faculty = faculty;
                student.Year = Convert.ToInt32(year);
                var insertOperation = TableOperation.Insert(student);
                await studentsTable.ExecuteAsync(insertOperation);
                System.Console.WriteLine("Data introdusa");
            }
            else
            {
                System.Console.WriteLine("Date existente");
            }
        }
        private static async Task EditStudent()
        {
            System.Console.WriteLine("Adauga universitate");
            string university = Console.ReadLine();
            System.Console.WriteLine("Adauga cnp:");
            string cnp = Console.ReadLine();
            StudentEntity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
            if(stud != null)
            {
                System.Console.WriteLine("Data existenta");
                var student = new StudentEntity(university,cnp);
                System.Console.WriteLine("Adauga prenume");
                string firstName = Console.ReadLine();
                System.Console.WriteLine("Adauga nume familie");
                string lastName = Console.ReadLine();
                System.Console.WriteLine("Adauga facultate");
                string faculty = Console.ReadLine();
                System.Console.WriteLine("Adauga an studdiu");
                string year = Console.ReadLine();
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Faculty = faculty;
                student.Year = Convert.ToInt32(year);
                student.ETag = "*";
                var updateOperation = TableOperation.Replace(student);
                await studentsTable.ExecuteAsync(updateOperation);
                System.Console.WriteLine("Date updatate");
            }
            else
            {
                System.Console.WriteLine("Datele nu exista");
            }
        }
        private static async Task DeleteStudent()
        {
            System.Console.WriteLine("Adauga universitate");
            string university = Console.ReadLine();
            System.Console.WriteLine("Adauga cnp:");
            string cnp = Console.ReadLine();

            StudentEntity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
            if(stud != null)
            {
                var student = new StudentEntity(university,cnp);
                student.ETag = "*";
                var deleteOperation = TableOperation.Delete(student);
                await studentsTable.ExecuteAsync(deleteOperation);
                System.Console.WriteLine("Data stearsa");
            }
            else
            {
                System.Console.WriteLine("Datele nu exista");
            }
        }
        private static async Task<List<StudentEntity>> GetAllStudents()
        {
            TableQuery<StudentEntity> tableQuery = new TableQuery<StudentEntity>();
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> result = await studentsTable.ExecuteQuerySegmentedAsync(tableQuery,token);
                token = result.ContinuationToken;
                students.AddRange(result.Results);
            }while(token != null);
            return students;
        }
        private static async Task DisplayStudents()
        {
            await GetAllStudents();

            foreach(StudentEntity std in students)
            {
                Console.WriteLine("Facultatea : {0}", std.Faculty);
                Console.WriteLine("Prenume : {0}", std.FirstName);
                Console.WriteLine("Nume : {0}", std.LastName);
                Console.WriteLine("An studiu : {0}", std.Year);
                Console.WriteLine("******************************");
            }
            students.Clear();
            
        }
    }
}