﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
