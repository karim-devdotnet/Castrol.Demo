using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castrol.Context
{
    public class UserData
    {
        [Key,Index, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserDataId { get; set; }

        [Required, StringLength(100)]
        public string UserId { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string UserPassword { get; set; }
    }
}
