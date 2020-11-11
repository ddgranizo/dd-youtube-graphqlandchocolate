using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQLWithHotChocolate
{

    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<Opportunity> Opportunities { get; set; }
    }


    public class Opportunity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Subject { get; set; }

        [Required]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
        public virtual SystemUser Owner { get; set; }

    }

    public class SystemUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public virtual List<Opportunity> Opportunities { get; set; }

    }

}
