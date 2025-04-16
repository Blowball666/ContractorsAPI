using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsAPI.Models
{
    /// <summary>
    /// Модель подрядчика.
    /// </summary>
    [Table("contractors")]
    public class Contractor
    {
        /// <summary>
        /// Уникальный идентификатор подрядчика.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Тип подрядчика.
        /// </summary>
        [Column("type_id")]
        [Required(ErrorMessage = "Тип подрядчика обязателен.")]
        public int TypeId { get; set; }

        /// <summary>
        /// Название подрядчика.
        /// </summary>
        [Column("name")]
        [Required(ErrorMessage = "Название подрядчика обязательно.")]
        public string Name { get; set; }

        /// <summary>
        /// Город юридического адреса.
        /// </summary>
        [Column("city")]
        [Required(ErrorMessage = "Город обязателен.")]
        public string City { get; set; }

        /// <summary>
        /// Улица юридического адреса.
        /// </summary>
        [Column("street")]
        public string Street { get; set; }

        /// <summary>
        /// Дом юридического адреса.
        /// </summary>
        [Column("building")]
        public string Building { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [Column("tax_id")]
        [Required(ErrorMessage = "ИНН обязателен.")]
        public string TaxId { get; set; }

        /// <summary>
        /// Фамилия подрядчика.
        /// </summary>
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя подрядчика.
        /// </summary>
        [Column("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество подрядчика.
        /// </summary>
        [Column("middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фото.
        /// </summary>
        [Column("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Почта подрядчика.
        /// </summary>
        [Column("email")]
        [EmailAddress(ErrorMessage = "Некорректный формат email.")]
        public string Email { get; set; }

        /// <summary>
        /// Рэйтинг.
        /// </summary>
        [Column("rating")]
        [Range(0, 5, ErrorMessage = "Рейтинг должен быть в диапазоне от 0 до 5.")]
        public decimal Rating { get; set; }
    }
}