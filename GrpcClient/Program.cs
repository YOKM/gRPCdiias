using Grpc.Net.Client;
using GrpcService;
using GrpcService.Protos;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{

     // Test 
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Jane Bond" };

            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            //StudentModel newStudent = new StudentModel()
            // {
            //      FirstName = "AAAA",
            //      LastName = "BBBB",
            //      School = "Tourism",
            //  };
            //  await insertStudent(channel, newStudent);


            //StudentModel updStudent = new StudentModel()
            //{
            //    StudentId = 56,
            //    FirstName = "AAAA",
            //    LastName = "ZZZ",
            //    School = "Medicine",
            //};
            //await updateStudent(channel, updStudent);

            //await deleteStudent(channel, 56);

         //   await displayAllStudents(channel);


            // Test ImagingSchedule Jobs
        //   await displayAllImagingTask(channel);


         //  await displayAllImagingTask_Detail(channel);

           //   await findScheduleTaskById(channel,58 );

         //   Console.WriteLine("Before delete ");

            ImagingScheduleJobModel updTask = new ImagingScheduleJobModel()
            {
               // Id = 58,
                Jobname = "TestJob_A GRPC",
                ScheduleTIME = "0 0/1 10-23 ? * * *",
                IsActive = "Test-False",
                Description = "Yeah gRPC insert new job success",
                JOBTYPE = "NA"
                

            };

            //  await UpdateImagingScheduleJob(channel, updTask);

            // await InsertImagingScheduleJob(channel, updTask);

          //  await DeleteImagingScheduleJob(channel, 77);

        //    Console.WriteLine("After delete ");

            await findScheduleTaskById(channel, 68);

            Console.ReadLine();
        }

        static async Task findStudentById(GrpcChannel channel, int id)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var input = new StudentLookupModel { StudentId = id };
            var reply = await client.GetStudentInfoAsync(input);
            Console.WriteLine($"{reply.FirstName} {reply.LastName}");
        }

        static async Task insertStudent(GrpcChannel channel, StudentModel student)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var reply = await client.InsertStudentAsync(student);
            Console.WriteLine(reply.Result);
        }

        static async Task updateStudent(GrpcChannel channel, StudentModel student)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var reply = await client.UpdateStudentAsync(student);
            Console.WriteLine(reply.Result);
        }

        static async Task deleteStudent(GrpcChannel channel, int id)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var input = new StudentLookupModel { StudentId = id };
            var reply = await client.DeleteStudentAsync(input);
            Console.WriteLine(reply.Result);
        }

        static async Task displayAllStudents(GrpcChannel channel)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var empty = new Empty();
            var list = await client.RetrieveAllStudentsAsync(empty);

            Console.WriteLine(">>>>>>>>>>>>>>>>>>++++++++++++<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            foreach (var item in list.Items)
            {
                Console.WriteLine($"{item.StudentId}: {item.FirstName} {item.LastName}");
            }
        }



        //test Imaging Job

            // [Get All ImagingTask]
        static async Task displayAllImagingTask(GrpcChannel channel)
        {
            var client = new RemoteImagingScheduleJob.RemoteImagingScheduleJobClient(channel);
                      

            var empty = new EmptyJob();
            var list = await client.RetrieveAllImagingScheduleJobsAsync(empty);

            Console.WriteLine(">>>>>>>>>>>>>Imaging Schedule>>>>>++++++++++++<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            foreach(var item in list.Items)
            {
                Console.WriteLine($"{item.Id}: {item.Description} {item.Jobname} {item.ScheduleTIME}");

            }

        }


        static async Task displayAllImagingTask_Detail(GrpcChannel channel)
        {
          

            var client = new RemoteImagingScheduleJob_Detail.RemoteImagingScheduleJob_DetailClient(channel);

            var empty = new EmptyJob_Detail();

            var list = await client.RetrieveAllImagingScheduleJobs_DetailAsync(empty);        

            Console.WriteLine(">>>>>>>>>>>>>Detailts Imaging Schedule>>>>>++++++++++++<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            foreach (var item in list.Items)
            {
                Console.WriteLine($"{item.Id}: {item.EmailNotificationAddress} {item.Jobname} {item.Jobname}");

            }

        }

        //[Query Task by ID]
        static async Task findScheduleTaskById(GrpcChannel channel, int id)
        {
            var client = new RemoteImagingScheduleJob.RemoteImagingScheduleJobClient(channel);
    
            var input = new ImagingScheduleJobLookupModel { Id = id };

            var reply = await client.GetImagingScheduleJobInfoAsync(input);
  

            Console.WriteLine($"{reply.Jobname} {reply.ScheduleTIME} {reply.IsActive} {reply.Description}");


            // Adding Detail info for task


            Console.WriteLine("Details");

            var client_detail = new RemoteImagingScheduleJob_Detail.RemoteImagingScheduleJob_DetailClient(channel);

            var input_Detail = new ImagingScheduleJobLookupModel_Detail { Jobid = id };


            var reply_Detail = await client_detail.GetImagingScheduleJobInfo_DetailAsync(input_Detail);

            Console.WriteLine($"{reply_Detail.Jobname}");
            Console.WriteLine($"{reply_Detail.EmailNotificationAddress}");
            Console.WriteLine($"{reply_Detail.FileExtensiontoUpload}");
            Console.WriteLine($"{reply_Detail.SFtpdownloadFrom}");
            Console.WriteLine($"{reply_Detail.SFtpdownloadTo}");
            Console.WriteLine($"{reply_Detail.SFtphost}");
            Console.WriteLine($"{reply_Detail.TimeSpanWait}");
            Console.WriteLine($"{reply_Detail.UsernamesFtp}");
            Console.WriteLine($"{reply_Detail.SFtpuploadFrom}");
            Console.WriteLine($"{reply_Detail.SFtpuploadto}");




        }

        //[Update Task by ID]
        static async Task UpdateImagingScheduleJob(GrpcChannel channel, ImagingScheduleJobModel ImagingShceduleTask_toUpdate)
        {
            var client = new RemoteImagingScheduleJob.RemoteImagingScheduleJobClient(channel);

            var reply = await client.UpdateImagingScheduleJobAsync(ImagingShceduleTask_toUpdate);
           
            Console.WriteLine(reply.Result);
        }
        

        //[Insert new Task]
        static async Task InsertImagingScheduleJob(GrpcChannel channel, ImagingScheduleJobModel NewImagingShceduleTask)
        {
            var client = new RemoteImagingScheduleJob.RemoteImagingScheduleJobClient(channel);

            var reply = await client.InsertImagingScheduleJobAsync(NewImagingShceduleTask);

            Console.WriteLine(reply.Result);
        }

        //[Delete a Task]
        static async Task DeleteImagingScheduleJob(GrpcChannel channel, int id)
        {
            var clinet = new RemoteImagingScheduleJob.RemoteImagingScheduleJobClient(channel);
            var input = new ImagingScheduleJobLookupModel { Id = id };
            var reply = await clinet.DeleteImagingScheduleJobAsync(input);
            Console.WriteLine(reply.Result);
        }





    }
}
