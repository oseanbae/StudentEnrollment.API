using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.API.Models
{
    public class Course
    {
        public long Id { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        public string Title
        {
            get => _title;
            set => _title = value?.Trim() ?? throw new ArgumentNullException(nameof(Title));
        }
        private string _title = null!;

        [Required, Range(1, 6)]
        public int Credits { get; set; }

    }
}