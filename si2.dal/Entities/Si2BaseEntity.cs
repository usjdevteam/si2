using System.ComponentModel.DataAnnotations;

namespace si2.dal.Entities
{
    public abstract class Si2BaseEntity<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }
    }
}
