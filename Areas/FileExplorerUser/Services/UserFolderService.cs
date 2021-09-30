using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserIdentityProject.Areas.FileExplorerAdmin.Models;
using UserIdentityProject.Data;

namespace UserIdentityProject.Areas.FileExplorerUser.Services {
    public interface IUserFolderService {
        public FolderModel GetRootFolder();
            public int CountMyFolders();
        public List<FolderModel> ShowAllFolders();
        public List<FolderModel> ShowFolders(int parentFolder);
        public FolderModel GetSpecificFolder(int thatFolderId);
        public List<FolderModel> ShowPathToFolder(int currentId);
    }
    public class UserFolderService : IUserFolderService {
        private readonly ApplicationDbContext _dbContext;
        public UserFolderService (ApplicationDbContext dbContext) {
            this._dbContext = dbContext;
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
                    .Where(n => n.ParentId == null)
                    // .Where(n => n.Accessibility == "Public")
                    .ToList();
            }
            return this._dbContext.Folders
                .Include(n => n.Parent)
                .Where(n => n.Parent.Id == parentFolderId)
                // .Where(n => n.Accessibility == "Public")
                .ToList();
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