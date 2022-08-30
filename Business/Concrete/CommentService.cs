using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CommentService : ICommentServices
    {
        private readonly ICommentDal _commentDal;
        private readonly IUserServices _userServices;

        public CommentService(ICommentDal commentDal,IUserServices userServices)
        {
            _commentDal = commentDal;
            _userServices = userServices;
        }

        public void AddComment(string comment, string email, int questionId)
        {
            var user = _userServices.GetUserByEmail(email);
            Comment newComment = new()
            {
                Message = comment,
                QuestionId = questionId,
                UserId=user.Id
            };
            _commentDal.Add(newComment);
        }
    }
}
