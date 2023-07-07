using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using UdemyAuthServer.Core.Configuration;
using UdemyAuthServer.Core.DTOs;
using UdemyAuthServer.Core.Models;
using UdemyAuthServer.Core.Services.Abstract;

namespace UdemyAuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        //(Claim: Kullanıcıya ait bilgileri tutan jwt içerisindeki nesne)
        //Guid.NewGuid().ToString() : Kullanıcıya ait unique bir id oluşturuyoruz.
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(UserApp userApp, List<String> audiences)
        {
            //
            //Özet
            //
            // Kullanıcı girişi gerektiren işlemler için kullanıyoruz.
            // Örneğin : Bir uygulamada kullanıcı giriş yaptıktan sonra sepetteki
            // ürünleri görmek istiyor ise oluşturduğumuz bu token ile apiye istek atmasını sağlayabiliriz.
            //
            // 1. Kullanıcıya ait claimleri oluşturuyoruz. 
            // 2. Kullanıcıya ait claimleri listeye ekliyoruz.
            // 3. Audience listesini claimlere ekliyoruz.
            // 4. Listeyi döndürüyoruz.
            //

            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email??""),
                new Claim(ClaimTypes.Name,userApp.UserName??""),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            //
            //Özet
            //
            // Kullanıcı olmadan token oluşturmak için kullanıyoruz.
            // Örneğin : Bir uygulamada ana sayfada döviz kurlarını göstermek istiyoruz.
            // Bu durumda kullanıcı olmadan token oluşturup
            // kullanıcının bu token ile apiye erişmesini sağlayabiliriz.
            //
            // 1. Client'a ait claimleri oluşturuyoruz.
            // 2. Client'a ait claimleri listeye ekliyoruz.
            // 3. Audience listesini claimlere ekliyoruz.
            // 4. Listeyi döndürüyoruz.
            //

            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            //
            //Özet
            //
            // Kullanıcıya ait token oluşturmak için kullanıyoruz.
            // 
            // 1. Token'ı oluşturuyoruz.
            // 2. Token'ı oluştururken kullanacağımız securityKey'i oluşturuyoruz.
            // 3. Token'ı oluştururken kullanacağımız signingCredentials'ı oluşturuyoruz.
            // 4. Token'ı oluşturuyoruz.
            // 5. Token'ı yazdırıyoruz.
            // 6. TokenDto'yu oluşturuyoruz.
            // 7. TokenDto'yu döndürüyoruz.
            //

            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            //
            //Özet
            //
            // Client'a ait token oluşturmak için kullanıyoruz.
            //
            // 1. Token'ı oluşturuyoruz.
            // 2. Token'ı oluştururken kullanacağımız securityKey'i oluşturuyoruz.
            // 3. Token'ı oluştururken kullanacağımız signingCredentials'ı oluşturuyoruz.
            // 4. Token'ı oluşturuyoruz.
            // 5. Token'ı yazdırıyoruz.
            // 6. TokenDto'yu oluşturuyoruz.
            // 7. TokenDto'yu döndürüyoruz.
            //

            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }
    }
}