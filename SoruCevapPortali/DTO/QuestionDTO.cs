using Microsoft.AspNetCore.Identity;
using SoruCevapPortali.Models;
using System.ComponentModel.DataAnnotations;

namespace SoruCevapPortali.DTO
{
    public class QuestionDTO
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionName { get; set; } = null!;

        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}
