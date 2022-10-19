using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework
{
    public class EfOrderEntity
    {
        public string Hash { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public EfUserEntity User { get; set; }

        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public EfOrderStatusEntity Status { get; set; }
    }
}