using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IQuestionServices
    {
        void AddQuestion(AddQuestionDTO question,string email);
        QuestionDetailDTO GetQuestionDetail(int id);
        Question GetQuestion(int id);
        List<QuestionsDTO> GetQuestions();
    }
}
