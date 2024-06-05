using System.ComponentModel.DataAnnotations;

namespace SoruCevapPortali.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionName { get; set; } = null!;

        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}
