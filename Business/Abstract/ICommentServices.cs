using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICommentServices
    {
        void AddComment(string comment, string email,int questionId);
    }
}
