using System.ComponentModel.DataAnnotations;

namespace SoruCevapPortali.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerName { get; set; } = null!;

        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}
