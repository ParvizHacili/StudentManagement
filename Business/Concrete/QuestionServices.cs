using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IQuestionDal _questionDal;
        private readonly IUserServices _userServices;
        public QuestionServices(IQuestionDal questionDal, IUserServices userServices)
        {
            _questionDal = questionDal;
            _userServices = userServices;
        }

        public void AddQuestion(AddQuestionDTO addQuestion,string email)
        {
            var user =_userServices.GetUserByEmail(email); 
            Question question = new()
            {
                Description = addQuestion.Description,
                Title = addQuestion.Title,
                PhotoUrl = addQuestion.PhotoUrl,
                UserId = user.Id,
            };
            _questionDal.Add(question);
        }

        public Question GetQuestion(int id)
        {
            return _questionDal.Get(x => x.Id == id);
        }

        public QuestionDetailDTO GetQuestionDetail(int id)
        {
            return _questionDal.GetQuestionDetail(id);
        }

        public List<QuestionsDTO> GetQuestions()
        {
            return _questionDal.GetAllQuestions();
        }
    }
}
