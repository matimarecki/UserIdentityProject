using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentityProject.Areas.FileExplorerUser.Models {
    public class FileUploaderModel {
        public string Path { get; set; }
        public string Name { get; set; }
        public int? FatherId { get; set; }
        public DateTime DateCreated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}