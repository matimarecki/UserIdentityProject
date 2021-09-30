using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserIdentityProject.Areas.FileExplorerAdmin.Models {
    public class FolderModel {
        public FolderModel? Parent { get; set; }
        public int? ParentId { get; set; }
        public string FolderName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Accessibility { get; set; }
        public string OptionalPassword { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
    }
}