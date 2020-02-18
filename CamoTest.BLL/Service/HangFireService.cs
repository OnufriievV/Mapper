using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace CamoTest.BLL.Service
{
    public interface IHangFireService
    {
        string CreateDeletionJobForFile(int requestId, string filePath, string fileExtension, int temporaryFileStorageTime);

        //void DeleteFile(int requestId, string filePath, string fileExtension);
    }

    public class HangFireService : IHangFireService
    {
        //public IRequestService RequestService { get; set; }

        //public IFileService FileService { get; set; }

        public string CreateDeletionJobForFile(int requestId, string filePath, string fileExtension, int temporaryFileStorageTime)
        {
            return BackgroundJob.Schedule<IFileService>((m) => m.DeleteFile(requestId, filePath, fileExtension), DateTime.Now.AddMinutes(temporaryFileStorageTime));
            //var request = RequestService.GetByID(requestId);

            //if (request != null)
                
        }

        //public void DeleteFile(int requestId, string filePath, string fileExtension)
        //{
        //    FileService.DeleteFile(requestId, filePath, fileExtension);
        //}
    }
}
