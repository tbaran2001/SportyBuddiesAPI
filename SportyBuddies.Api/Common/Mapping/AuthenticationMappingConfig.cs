using Mapster;
using SportyBuddies.Application.Authentication.Commands.Register;
using SportyBuddies.Application.Authentication.Common;
using SportyBuddies.Application.Authentication.Queries.Login;
using SportyBuddies.Contracts.Authentication;

namespace SportyBuddies.Api.Common.Mapping;

public class AuthenticationMappingConfig:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        
        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest=>dest, src=>src.User);
    }
}