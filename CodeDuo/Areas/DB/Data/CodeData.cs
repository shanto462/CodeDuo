using CodeDuo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeDuo.Areas.DB.Data
{
    /*
     *    line -> col => 
     *    IMemoryDb singleton
     */
    public class CodeData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public DateTime LastAccessed { get; set; }

        [Required]
        public string CodeSegment { get; set; }

        public IList<CodeDataShare> SharedUsers { get; set; }

        public CodeDuoUser GetOwner() => SharedUsers.FirstOrDefault(s => s.Permission == SharePermission.OWNER).User;
    }

    public enum SharePermission
    {
        READ,
        WRITE,
        OWNER
    }

    public class CodeDataShare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public CodeDuoUser User { get; set; }
        public SharePermission Permission { get; set; }
        public Guid CodeDataId { get; set; }
        public CodeData CodeData { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
