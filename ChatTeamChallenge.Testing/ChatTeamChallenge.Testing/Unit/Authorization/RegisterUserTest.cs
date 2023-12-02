using System.Net;
using ChatTeamChallenge.Testing.Common.Client.Apis;
using ChatTeamChallenge.Testing.Common.Helpers;
using FluentAssertions;

namespace ChatTeamChallenge.Testing.Unit.Authorization;

public sealed class RegisterUserTest
{
    private readonly AuthorizationApi _authorizationApi;
    private readonly UserApi _userApi;
    
    public RegisterUserTest()
    {
        _authorizationApi = new AuthorizationApi();
        _userApi = new UserApi();
    }
    
    [Fact]
    public async Task RegisterValidRequest_Should_ReturnCreatedRequest()
    {
        // Arrange
        var registerRequest = TestHelper.BuildTestRegisterRequest();

        // Act
        var response = await _authorizationApi.RegisterAsync(registerRequest);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Data.Should().NotBeNull();

        var deleteResponse = await _userApi.DeleteAsync(response.Data.User.Id);
        var userResponse = await _userApi.GetByIdAsync(response.Data.User.Id);
        
//        deleteResponse.IsSuccessStatusCode.Should().BeTrue();
        userResponse.Data.Should().BeNull();

        //authUserResponse!.User.Should().BeEquivalentTo(registerRequest);
    }
}