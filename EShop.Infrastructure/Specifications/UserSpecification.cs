using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification() { }

        public UserSpecification(string Id) : base(User => User.Id == Id) { }

    }
}
