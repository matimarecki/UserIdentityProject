using System.Collections.Generic;
using UserIdentityProject.Areas.FileExplorerAdmin.Models;

namespace UserIdentityProject.Areas.FileExplorerUser.Models {
    public class FileExplorerUserModel {
        public FileExplorerFolderModel TheFolders { get; set; }
        public List<FileUploaderModel> TheFiles { get; set; }
    }
}