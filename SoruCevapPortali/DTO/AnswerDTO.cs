using Microsoft.AspNetCore.Identity;
using SoruCevapPortali.Models;
using System.ComponentModel.DataAnnotations;

namespace SoruCevapPortali.DTO
{
    public class AnswerDTO
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerName { get; set; } = null!;

        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}
