using System;
using System.Collections.Generic;

#nullable disable

namespace Auth_Lab.Models.DBEntity
{
    public partial class AddressBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
