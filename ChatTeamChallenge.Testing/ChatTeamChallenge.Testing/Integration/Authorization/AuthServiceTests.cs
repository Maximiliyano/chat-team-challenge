using ChatTeamChallenge.Domain.Reviews;
using ChatTeamChallenge.Testing.Common.Helpers;
using Moq;

namespace ChatTeamChallenge.Testing.Integration.Authorization;

public sealed class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    
    [Fact]
    public async Task RegisterNewUser_Should_ReturnSuccessfulRegisteredUserWithToken()
    {
        // Arrange
        var registerRequest = TestHelper.BuildTestRegisterRequest();

        // Act

        // Assert
        _userRepository.Verify(x => x.ReadByEmailAsync(registerRequest.Email));
    }
}