using System.ComponentModel.DataAnnotations;

namespace KetNoiDB.Models.ViewModels
{
    public partial class VMStudent
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? ImgUrl { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Mssv { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
