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
    public class ImagingScheduleJob_DetailService : RemoteImagingScheduleJob_Detail.RemoteImagingScheduleJob_DetailBase
        {
        private readonly ILogger<ImagingScheduleJob_DetailService> _logger;
        private readonly ImagingDbContext _context;

        public ImagingScheduleJob_DetailService(ILogger<ImagingScheduleJob_DetailService> logger, ImagingDbContext context)
        {
            _logger = logger;
            _context = context;
        }


       
            //public static List<Seat> GetSeatsForReservation(Guid reservationId)
            //{
            //    var db = new EntityContext();
            //    return (from s in db.ReservationSeat
            //            where s.ReservationID == Guid
            //            select s.seat).ToList();
            //}

     

        public override Task <ImagingScheduleJobModel_Detail> GetImagingScheduleJobInfo_Detail(ImagingScheduleJobLookupModel_Detail request,ServerCallContext context)
        {
            ImagingScheduleJobModel_Detail output = new ImagingScheduleJobModel_Detail();

          //   var ScheduleJob_Detail = _context.imaging_JOBdetails.Find(request.Jobid);


            var ScheduleJob_Detailw = (from s in _context.imaging_JOBdetails
                                                                        where s.Jobid == request.Jobid
                                                                        select s.Id);

            var ScheduleJob_Detail = _context.imaging_JOBdetails.Find(ScheduleJob_Detailw.First());


            _logger.LogInformation("Sending Imaging ScheduleJob Detail request to find ");

            if (ScheduleJob_Detail != null)
            {
                output.Id = ScheduleJob_Detail.Id;
                output.Jobname = ScheduleJob_Detail.Jobname;
                output.Jobid = ScheduleJob_Detail.Jobid;
                output.EmailNotificationAddress = ScheduleJob_Detail.EmailNotificationAddress;
                output.JobdetailsType = ScheduleJob_Detail.JobdetailsType;
                output.SFtphost = ScheduleJob_Detail.SFtphost;
                output.SFtpuploadFrom = ScheduleJob_Detail.SFtpuploadFrom;
                output.SFtpuploadto = ScheduleJob_Detail.SFtpuploadto;
                output.SFtpdownloadFrom = ScheduleJob_Detail.SFtpdownloadFrom;
                output.SFtpdownloadTo = ScheduleJob_Detail.SFtpdownloadTo;
                output.UsernamesFtp = ScheduleJob_Detail.UsernamesFtp;
                output.PaswordsFtp = ScheduleJob_Detail.PaswordsFtp;
                output.SshfingerPrint = ScheduleJob_Detail.SshfingerPrint;
                output.Extra1 = ScheduleJob_Detail.Extra1;
                output.Extra2 = ScheduleJob_Detail.Extra2;
                output.Extra3 = ScheduleJob_Detail.Extra3;
                output.Extra4 = ScheduleJob_Detail.Extra4;
                output.Extra5 = ScheduleJob_Detail.Extra5;
                output.TimeSpanWait = ScheduleJob_Detail.TimeSpanWait;
                output.FileExtensiontoUpload = ScheduleJob_Detail.FileExtensiontoUpload;
                output.PortNumber = ScheduleJob_Detail.PortNumber;
                output.WordsToCheck = ScheduleJob_Detail.WordsToCheck;

             
               
            }

            return Task.FromResult(output);

        }
        public override Task<ImagingScheduleJobList_Detail> RetrieveAllImagingScheduleJobs_Detail(EmptyJob_Detail request, ServerCallContext context)
        {
            _logger.LogInformation("Retrieving all ImagingJobs");

            ImagingScheduleJobList_Detail list = new ImagingScheduleJobList_Detail();

       

            try
            {
                List< ImagingScheduleJobModel_Detail> imagingScheduleJobsList = new List<ImagingScheduleJobModel_Detail>();

                var scheduleJobs_Details = _context.imaging_JOBdetails.ToList();

                foreach (var c in scheduleJobs_Details)
                {
                    imagingScheduleJobsList.Add(new ImagingScheduleJobModel_Detail()
                    {

                        Id = c.Id,
                        Jobname = c.Jobname,
                        Jobid = c.Jobid,
                        EmailNotificationAddress = c.EmailNotificationAddress,
                        JobdetailsType = c.JobdetailsType,
                        SFtphost = c.SFtphost,
                        SFtpuploadFrom = c.SFtpuploadFrom,
                        SFtpuploadto = c.SFtpuploadto,
                        SFtpdownloadFrom = c.SFtpdownloadFrom,
                        SFtpdownloadTo = c.SFtpdownloadTo,
                        UsernamesFtp = c.UsernamesFtp,
                        PaswordsFtp = c.PaswordsFtp,
                        SshfingerPrint = c.SshfingerPrint,
                        Extra1 = c.Extra1,
                        Extra2 = c.Extra2,
                        Extra3 = c.Extra3,
                        Extra4 = c.Extra4,
                        Extra5 = c.Extra5,
                        TimeSpanWait = c.TimeSpanWait,
                        FileExtensiontoUpload = c.FileExtensiontoUpload,
                        PortNumber = c.PortNumber,
                        WordsToCheck = c.WordsToCheck,
                         
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
        
        public override Task<ReplyJob_Detail> UpdateImagingScheduleJob_Detail(ImagingScheduleJobModel_Detail request, ServerCallContext context)
        {
          
            var s = _context.imaging_JOBdetails.Find(request.Id);

            if (s == null)
            {
                return Task.FromResult(
                  new ReplyJob_Detail()
                  {
                      Result = $"ImagingScheduleTask Detail {request.Id} {request.Jobname} cannot be found.",
                      IsOk = false
                  }
                );
            }

                        s.Id = request.Id;
                        s.Jobname = request.Jobname;
                        s.Jobid = request.Jobid;
                        s.EmailNotificationAddress = request.EmailNotificationAddress;
                        s.JobdetailsType = request.JobdetailsType;
                        s.SFtphost = request.SFtphost;
                        s.SFtpuploadFrom = request.SFtpuploadFrom;
                        s.SFtpuploadto = request.SFtpuploadto;
                        s.SFtpdownloadFrom = request.SFtpdownloadFrom;
                        s.SFtpdownloadTo = request.SFtpdownloadTo;
                        s.UsernamesFtp = request.UsernamesFtp;
                        s.PaswordsFtp = request.PaswordsFtp;
                        s.SshfingerPrint = request.SshfingerPrint;
                        s.Extra1 = request.Extra1;
                        s.Extra2 = request.Extra2;
                        s.Extra3 = request.Extra3;
                        s.Extra4 = request.Extra4;
                        s.Extra5 = request.Extra5;
                        s.TimeSpanWait = request.TimeSpanWait;
                        s.FileExtensiontoUpload = request.FileExtensiontoUpload;
                        s.PortNumber = request.PortNumber;
                        s.WordsToCheck = request.WordsToCheck;


            _logger.LogInformation("Update ImagingSchedule Details Task");

            try
            {
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new ReplyJob_Detail()
               {
                   Result = $"ImagingTask Details {request.Id} {request.Jobname} was successfully updated.",
                   IsOk = true
               }
            );
        }

        public override Task<ReplyJob_Detail> InsertImagingScheduleJob_Detail(ImagingScheduleJobModel_Detail request, ServerCallContext context)
        {
            
            var s = _context.imaging_JOBdetails.Find(request.Id);

            if (s != null)
            {
                return Task.FromResult(
                  new ReplyJob_Detail()
                  {
                      Result = $"Task {request.Id} {request.Jobname} already exists.",
                      IsOk = false
                  }
                );
            }

            Models.ImagingScheduleJob_Detail NewTask_Detail = new Models.ImagingScheduleJob_Detail()
            {
               
                     // s.Id = request.Id;
                    Jobname = request.Jobname,
                    Jobid = request.Jobid,
                    EmailNotificationAddress = request.EmailNotificationAddress,
                    JobdetailsType = request.JobdetailsType,
                    SFtphost = request.SFtphost,
                    SFtpuploadFrom = request.SFtpuploadFrom,
                    SFtpuploadto = request.SFtpuploadto,
                    SFtpdownloadFrom = request.SFtpdownloadFrom,
                    SFtpdownloadTo = request.SFtpdownloadTo,
                    UsernamesFtp = request.UsernamesFtp,
                    PaswordsFtp = request.PaswordsFtp,
                    SshfingerPrint = request.SshfingerPrint,
                    Extra1 = request.Extra1,
                    Extra2 = request.Extra2,
                    Extra3 = request.Extra3,
                    Extra4 = request.Extra4,
                    Extra5 = request.Extra5,
                    TimeSpanWait = request.TimeSpanWait,
                    FileExtensiontoUpload = request.FileExtensiontoUpload,
                    PortNumber = request.PortNumber,
                    WordsToCheck = request.WordsToCheck,

            };

            _logger.LogInformation("Insert new task");

            try
            {

                _context.imaging_JOBdetails.Add(NewTask_Detail);

                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new ReplyJob_Detail()
               {
                   Result = $"New task {request.Jobname} {request.Id}  was successfully inserted.",
                   IsOk = true
               }
            );
        }

        public override Task<ReplyJob_Detail> DeleteImagingScheduleJob_Detail(ImagingScheduleJobLookupModel_Detail request, ServerCallContext context)
        {
            var s = _context.imaging_JOBdetails.Find(request.Jobid);

            if (s == null)
            {
                return Task.FromResult(
                  new ReplyJob_Detail()
                  {
                      Result = $"Task with ID {request.Jobid} cannot be found.",
                      IsOk = false
                  }
                );
            }

            _logger.LogInformation("Delete Task");

            try
            {
                _context.imaging_JOBdetails.Remove(s);
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new ReplyJob_Detail()
               {
                   Result = $"Task with ID {request.Jobid} was successfully deleted.",
                   IsOk = true
               }
            );
        }


    }
}
