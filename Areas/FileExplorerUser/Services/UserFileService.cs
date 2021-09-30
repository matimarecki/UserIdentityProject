using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UserIdentityProject.Areas.FileExplorerUser.Models;
using UserIdentityProject.Data;

namespace UserIdentityProject.Areas.FileExplorerUser.Services {
    public interface IUserFileService {
        public List<FileUploaderModel> ReturnFilesInFolder(int fatherId);
        public void AddFileToFolder(int fatherId, IFormFile file);
        public void RemoveFile(int fileId);

    }
    public class UserFileService : IUserFileService {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _dbContext;

        public UserFileService(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) {
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        public List<FileUploaderModel> ReturnFilesInFolder(int fatherId) {
            List<FileUploaderModel> filesInFolder = new List<FileUploaderModel>();
            if (fatherId == 0) {
                filesInFolder = this._dbContext.FilesUploaded
                    .Where(n => n.FatherId == 0)
                    .ToList();
            }
            else {
                filesInFolder = this._dbContext.FilesUploaded
                    .Where(n => n.FatherId == fatherId)
                    .ToList();
            }
            return filesInFolder;
        }

        public void RemoveFile(int fileId) {
            FileUploaderModel thatFile = this._dbContext.FilesUploaded
                .SingleOrDefault(n => n.Id == fileId);
            File.Delete(thatFile.Path);
            this._dbContext.FilesUploaded.Remove(thatFile);
            this._dbContext.SaveChanges();
        }

        public void AddFileToFolder(int fatherId, IFormFile file) {
            string uploads = Path.Combine(this._webHostEnvironment.WebRootPath, "Uploads");
            FileUploaderModel newFile = new FileUploaderModel();
            if (file.Length > 0) {
                string filePath = Path.GetFileName(file.FileName);
                Console.WriteLine(Path.Combine(uploads, filePath));
                newFile.Path = Path.Combine(uploads, filePath);
                newFile.Name = file.FileName;
                newFile.DateCreated = new DateTime();
                newFile.FatherId = fatherId;
                this._dbContext.Add(newFile);
                this._dbContext.SaveChanges();
                using (FileStream stream = new FileStream(Path.Combine(uploads, filePath), FileMode.Create)) {
                    file.CopyTo(stream);
                }
            }
        }
    }
}