﻿using Bogus;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Enums;

namespace ChatTeamChallenge.Testing.Common.Helpers;

public static class TestHelper
{
    public static RegisterRequest BuildTestRegisterRequest() => 
        new Faker<RegisterRequest>().CustomInstantiator(f => new RegisterRequest
        {
            Email = f.Internet.Email(),
            Password = "Passw0rd",
            IsRemote = false,
            Username = f.Internet.UserName(),
            City = f.Address.City(),
            Roles = CreativeRoles.None
        }).Generate();
}