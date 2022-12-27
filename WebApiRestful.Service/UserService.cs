using System.Threading.Tasks;
using WebApiRestful.Data.Abstract;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CheckLogin(string username, string password)
        {
            return await _unitOfWork.RepositoryUser.GetSingleByConditionAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User> FindByUsername(string username)
        {
            return await _unitOfWork.RepositoryUser.GetSingleByConditionAsync(x => x.Username == username);
        }

        public async Task<User> FindById(int userId)
        {
            return await _unitOfWork.RepositoryUser.GetSingleByConditionAsync(x => x.Id == userId);
        }
    }
}
