using System.Net;
using ChatTeamChallenge.Testing.Common.Client.Apis;
using ChatTeamChallenge.Testing.Common.Client.Session;
using ChatTeamChallenge.Testing.Common.Helpers;
using FluentAssertions;

namespace ChatTeamChallenge.Testing.Unit.Authorization;

public sealed class RegisterUserTest
{
    private readonly SessionApi _sessionApi;
    private readonly AuthorizationApi _authorizationApi;
    private readonly UserApi _userApi;

    public RegisterUserTest()
    {
        _sessionApi = new SessionApi();
        _authorizationApi = new AuthorizationApi(_sessionApi.SessionStorage);
    }

    [Fact]
    public async Task RegisterValidRequest_Should_ReturnCreatedRequest()
    {
        // Arrange
        var registerRequest = TestHelper.BuildTestRegisterRequest();

        // Act
        var response = await _authorizationApi.RegisterAsync(registerRequest);
        
        await _userApi.DeleteAsync(response.Data.User.Id);
        var userResponse = await _userApi.GetByIdAsync(response.Data.User.Id);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Data.Should().NotBeNull();
        response.Data.User.Should().BeEquivalentTo(registerRequest);
        
        userResponse.Data.Should().BeNull();
    }
}