using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("ClientRegions")]
    public class ClientRegion
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name cannot be longer than 60 characters")]
        public string Name { get; set; }
        public string TranslationKey { get; set; }
        public bool Enabled { get; set; }

        [Required(ErrorMessage = "Client is required")]
        [Column("Client_ID")]
        public Guid ClientId { get; set; }
    }
}
