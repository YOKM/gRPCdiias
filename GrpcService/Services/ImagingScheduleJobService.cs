using Grpc.Core;
using GrpcService.Data;
using GrpcService.Models;
using GrpcService.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class ImagingScheduleJobService : RemoteImagingScheduleJob.RemoteImagingScheduleJobBase
        
        {
        private readonly ILogger<ImagingScheduleJobService> _logger;
        private readonly ImagingDbContext _context;

        public ImagingScheduleJobService(ILogger<ImagingScheduleJobService> logger, ImagingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<ImagingScheduleJobModel> GetImagingScheduleJobInfo(ImagingScheduleJobLookupModel request,ServerCallContext context)
        {
            ImagingScheduleJobModel output = new ImagingScheduleJobModel();

            var ScheduleJob = _context.ImagingScheduleJob.Find(request.Id);

            _logger.LogInformation("Sending Imaging ScheduleJob request to find ");

            if (ScheduleJob != null)
            {
                output.Id = ScheduleJob.Id;
                output.IsActive = ScheduleJob.IsActive;
                output.ScheduleTIME = ScheduleJob.scheduleTIME;
                output.Jobname = ScheduleJob.Jobname;
                output.JOBTYPE = ScheduleJob.JOBTYPE;
               
            }

            return Task.FromResult(output);

        }
        public override Task<ImagingScheduleJobList> RetrieveAllImagingScheduleJobs(EmptyJob request, ServerCallContext context)
        {
            _logger.LogInformation("Retrieving all ImagingJobs");

            ImagingScheduleJobList list = new ImagingScheduleJobList();

       

            try
            {
                List< ImagingScheduleJobModel> imagingScheduleJobsList = new List<ImagingScheduleJobModel>();

                var scheduleJobs = _context.ImagingScheduleJob.ToList();

                foreach (var c in scheduleJobs)
                {
                    imagingScheduleJobsList.Add(new ImagingScheduleJobModel()
                    {
                       
                        Id = c.Id,
                        Jobname = c.Jobname,
                        Description = c.Description,
                        ScheduleTIME = c.scheduleTIME,
                        JOBTYPE = c.JOBTYPE,
                        IsActive = c.IsActive,
                        
                    });
                }

                list.Items.AddRange(imagingScheduleJobsList);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(list);
        }
        
        public override Task<ReplyJob> UpdateImagingScheduleJob(ImagingScheduleJobModel request, ServerCallContext context)
        {
          
            var s = _context.ImagingScheduleJob.Find(request.Id);

            if (s == null)
            {
                return Task.FromResult(
                  new ReplyJob()
                  {
                      Result = $"ImagingScheduleTask {request.Id} {request.Jobname} cannot be found.",
                      IsOk = false
                  }
                );
            }


            s.Jobname = request.Jobname;
            s.IsActive = request.IsActive;
            s.JOBTYPE = request.JOBTYPE;
            s.scheduleTIME = request.ScheduleTIME;
            s.Description = request.Description;


            _logger.LogInformation("Update ImagingSchedule Task");

            try
            {
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new ReplyJob()
               {
                   Result = $"ImagingTask {request.Id} {request.Jobname} was successfully updated.",
                   IsOk = true
               }
            );
        }

        public override Task<ReplyJob> InsertImagingScheduleJob(ImagingScheduleJobModel request, ServerCallContext context)
        {
            
            var s = _context.ImagingScheduleJob.Find(request.Id);

            if (s != null)
            {
                return Task.FromResult(
                  new ReplyJob()
                  {
                      Result = $"Task {request.Id} {request.Jobname} already exists.",
                      IsOk = false
                  }
                );
            }

            ImagingScheduleJob NewTask = new ImagingScheduleJob()
            {
                Jobname = request.Jobname,
                JOBTYPE = request.JOBTYPE,
                Description = request.Description,
                scheduleTIME =request.ScheduleTIME,
                IsActive = request.IsActive,

            };

            _logger.LogInformation("Insert new task");

            try
            {

                _context.ImagingScheduleJob.Add(NewTask);

                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new ReplyJob()
               {
                   Result = $"New task {request.Jobname} {request.Description}  was successfully inserted.",
                   IsOk = true
               }
            );
        }

        public override Task<ReplyJob> DeleteImagingScheduleJob(ImagingScheduleJobLookupModel request, ServerCallContext context)
        {
            var s = _context.ImagingScheduleJob.Find(request.Id);

            if (s == null)
            {
                return Task.FromResult(
                  new ReplyJob()
                  {
                      Result = $"Task with ID {request.Id} cannot be found.",
                      IsOk = false
                  }
                );
            }

            _logger.LogInformation("Delete Task");

            try
            {
                _context.ImagingScheduleJob.Remove(s);
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new ReplyJob()
               {
                   Result = $"Task with ID {request.Id} was successfully deleted.",
                   IsOk = true
               }
            );
        }


    }
}
