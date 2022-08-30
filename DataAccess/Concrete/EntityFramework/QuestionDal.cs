using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class QuestionDal : EfRepositoryBase<Question, AppDbContext>, IQuestionDal
    {
        public List<QuestionsDTO> GetAllQuestions()
        {
            using var _context =new AppDbContext();
            var questions = _context.Questions.Include(x => x.User).ToList();
            List<QuestionsDTO> questionsDTOList = new();
            for (int i = 0; i < questions.Count; i++)
            {
                QuestionsDTO questionsDTO = new()
                {
                    Description = questions[i].Description,
                    Title= questions[i].Title,
                    Comments = 0,
                    Likes = 0,
                    Hit = 0,
                    PhotoUrl = questions[i].PhotoUrl,
                    Id = questions[i].Id,
                    Username = questions[i].User.Name
                };
                questionsDTOList.Add(questionsDTO);
            }
            return questionsDTOList;
        }

        public QuestionDetailDTO GetQuestionDetail(int id)
        {
            using var _context = new AppDbContext();
            var question = _context.Questions.Include(x=>x.User).FirstOrDefault(x => x.Id == id);
            var comments = _context.Comments.Include(x => x.User).Where(x => x.QuestionId == id).ToList();

            List<CommentDTO> questionComments = new();
            foreach (var comment in comments)
            {
                CommentDTO commentDTO = new()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    UserName = comment.User.Name
                };
                questionComments.Add(commentDTO);
            }

            QuestionDetailDTO questionDetail = new()
            {
                Id = question.Id,
                Username = question.User.Name,
                Title = question.Title,
                Description = question.Description,
                Hit = question.Hit,
                Likes = question.Likes,
                PhotoUrl = question.PhotoUrl,
                Comments = questionComments
            };
            return questionDetail;
        }
    }
}
