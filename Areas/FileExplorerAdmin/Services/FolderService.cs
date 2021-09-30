using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserIdentityProject.Areas.FileExplorerAdmin.Models;
using UserIdentityProject.Data;

namespace UserIdentityProject.Areas.FileExplorerAdmin.Services {
    public interface IFolderService {
        public void AddFolder(FolderModel newFolder);
        public void CleanseEverything();
        public void RemoveFolder (int folderId);
        public void EditFolder(FolderModel editedFolder);
        public FolderModel GetRootFolder();
            public int CountMyFolders();
        public List<FolderModel> ShowAllFolders();
        public List<FolderModel> ShowFolders(int parentFolder);
        public FolderModel GetSpecificFolder(int thatFolderId);
        public List<FolderModel> ShowPathToFolder(int currentId);
    }
    public class FolderService : IFolderService {
        private readonly ApplicationDbContext _dbContext;
        public FolderService (ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public void AddFolder(FolderModel newFolder) {
            this._dbContext.Folders.Add(newFolder);
            this._dbContext.SaveChanges();
        }

        public void CleanseEverything() {
            foreach (var target in this._dbContext.Folders) {
                this._dbContext.Folders.Remove(target);
            }
            this._dbContext.SaveChanges();
        }

        public void RemoveFolder(int folderId) {
            FolderModel deletedFolder = this._dbContext.Folders
                .FirstOrDefault(n => n.Id == folderId);
            this._dbContext.Folders.Remove(deletedFolder);
            this._dbContext.SaveChanges();
        }

        public void EditFolder(FolderModel editedFolder) {
            var result = this._dbContext.Folders
                .SingleOrDefault(n => n.Id == editedFolder.Id);
            if (result != null) {
                Console.WriteLine(result.FolderName);
                this._dbContext.Entry(result).CurrentValues.SetValues(editedFolder);
                Console.WriteLine(result.FolderName);
                this._dbContext.SaveChanges();
            }
        }

        public FolderModel GetRootFolder() {
            return this._dbContext.Folders
                .OrderBy(n=> n.Id)
                .FirstOrDefault();
        }
        public FolderModel GetSpecificFolder(int thatFolderId) {
            return this._dbContext.Folders
                .Include(n => n.Parent)
                .FirstOrDefault(n => n.Id == thatFolderId);
        }

        public int CountMyFolders() {
            return this._dbContext.Folders.Count();
        }

        public List<FolderModel> ShowAllFolders() {
            return this._dbContext.Folders.ToList();
        }
        

        public List<FolderModel> ShowFolders(int parentFolderId) {
            if (parentFolderId == 0) {
                return this._dbContext.Folders
                    .Include(n => n.Parent)
                    .Where(n => n.ParentId == null).ToList();
            }
            return this._dbContext.Folders
                .Include(n => n.Parent)
                .Where(n => n.Parent.Id == parentFolderId).ToList();
        }

        public List<FolderModel> ShowPathToFolder(int currentId) {
            FolderModel inspectingFolder = this.GetSpecificFolder(currentId);
            if (currentId == 0) {
                return new List<FolderModel>();
            }
            if (inspectingFolder.ParentId == null) {
                List<FolderModel> firstReturning = new List<FolderModel>();
                firstReturning.Add(inspectingFolder);
                return firstReturning;
            }
            else {
                List<FolderModel> pathReturning = ShowPathToFolder(inspectingFolder.Parent.Id);
                pathReturning.Add(inspectingFolder);
                return pathReturning;
            }
        }
    }
}