using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyModel;
using UserIdentityProject.Areas.FileExplorerAdmin.Models;

namespace UserIdentityProject.Areas.FileExplorerAdmin.Services {
    public interface IFileExplorerPathingService {
        public int CurrentId();
        public void ChangeCurrId(int newCurrId);
        public List<FolderModel> ShowCurrentPath ();
        public void AddFolderToCurrentPath(FolderModel newFolder);
        public void RemoveFolderFromCurrentPath (FolderModel yeetFolder);
    } 
    public class FileExplorerPathingService : IFileExplorerPathingService {
        private int _currParentId;
        private List<FolderModel> _currPathList;

        public FileExplorerPathingService() {
            this._currPathList = new List<FolderModel>();
        }

        public int CurrentId() {
            return _currParentId;
        }

        public void ChangeCurrId(int newCurrId) {
            this._currParentId = newCurrId;
        }

        public List<FolderModel> ShowCurrentPath() {
            return this._currPathList;
        }

        public void AddFolderToCurrentPath(FolderModel newFolder) {
            this._currPathList.Add(newFolder);
        }

        public void RemoveFolderFromCurrentPath(FolderModel yeetFolder) {
            this._currPathList.Remove(yeetFolder);
        }
        
    }
}