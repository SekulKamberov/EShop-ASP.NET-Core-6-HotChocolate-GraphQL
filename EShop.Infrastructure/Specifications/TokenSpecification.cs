using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class TokenSpecification : BaseSpecification<Token>
    {
        public TokenSpecification(string refreshToken) 
            : base(Token => Token.RefreshToken == refreshToken) { }
    }
}
