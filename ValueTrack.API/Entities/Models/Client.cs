using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Clients")]
    public class Client: IEntity
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Abrreviation is required")]
        [StringLength(6, ErrorMessage = "Abbreviation can't be longer than 6 characters")]
        public string Abbreviation { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        public Guid? Currency_ID { get; set; }
        public int PortfolioSize { get; set; }
        public Guid? UnitOfSpace_ID { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? Industry_ID { get; set; }
        public bool? IsCurrencyDefault { get; set; }
        public bool? IsParentActive { get; set; }
        public int? ContractType_ID { get; set; }
    }
}
