using System;
using CWBlazor.Domain.Enums;

namespace CWBlazor.Domain.Entities
{
    public class SecurityAudit
    {
        public SecurityAudit()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public long Id { get; set; }

        public SecurityAuditType Type { get; set; }

        public string Details { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}