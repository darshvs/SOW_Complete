namespace CandidateSoW.Models
{
    public class SecurityQuestionModel
    {
        public int QuestionId { get; set; } = 0;
        public string Question { get; set; } = "";
        public string Type { get; set; } = "";
    }

    public class SecurityAnswerModel
    {
        public int AnswerId { get; set; } = 0;
        public int LoginId { get; set; } = 0;
        public int QuestionId { get; set; } = 0;
        public string Answer { get; set; } = "";
        public string Type { get; set; } = "";
    }
}