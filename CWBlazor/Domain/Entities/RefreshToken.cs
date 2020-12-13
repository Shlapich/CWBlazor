using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CWBlazor.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public CWUser User { get; set; }
    }
}