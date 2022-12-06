﻿using System.Collections.Generic;
using Moq;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Data.Local.DataSource
{
    public class LocalSteamDataSourceTest
    {
        private readonly Mock<ISteamConfig> _steamConfigMock;
        private readonly Mock<ILoginUsersDao> _loginUsersDaoMock;
        private readonly ILocalSteamDataSource _sut;

        public LocalSteamDataSourceTest()
        {
            _loginUsersDaoMock = new Mock<ILoginUsersDao>(behavior: MockBehavior.Strict);
            _steamConfigMock = new Mock<ISteamConfig>(behavior: MockBehavior.Strict);
            _sut = new LocalSteamDataSource(
                steamConfig: _steamConfigMock.Object, 
                loginUsersDao: _loginUsersDaoMock.Object
                );
        }

        [Fact]
        public void Get_Steam_Dir_returns_Steam_Dir_Path()
        {
            const string steamPath = @"C:\Steam\";
            
            _steamConfigMock.Setup(cfg => cfg.GetSteamPath())
                .Returns(steamPath)
                .Verifiable();
            
            Assert.Equal(expected: steamPath, actual: _sut.GetSteamDir());
        }
        
        [Fact]
        public void Get_Steam_Executable_Path_returns_Executable_Path()
        {
            const string steamExecutablePath = @"C:\Steam\Steam.exe";
            
            _steamConfigMock.Setup(cfg => cfg.GetSteamExecutablePath())
                .Returns(steamExecutablePath)
                .Verifiable();
            
            Assert.Equal(expected: steamExecutablePath, actual: _sut.GetSteamExecutablePath());
        }

        [Fact]
        public void Update_Auto_Login_User_Calls_Set_Auto_Login_User()
        {
            const string steamId = "4156434124314";
            
            _loginUsersDaoMock.Setup(dao => dao.SetAutoLoginUser(steamId))
                .Returns(true)
                .Verifiable();

            _sut.UpdateAutoLoginUser(steamId);
            
            _loginUsersDaoMock.Verify(dao => dao.SetAutoLoginUser(steamId), Times.Once);
        }

        [Fact]
        public async void Get_Logged_In_Users_calls_Get_Logged_In_Users()
        {
            var peterUser = new LoginUserDto()
            {
                SteamId = "133",
                AccountName = "Peter",
                MostRecent = true,
                PasswordRemembered = true,
                PersonaName = "PeterMieter",
                Timestamp = "1312345612"
            };

            List<LoginUserDto> users = new List<LoginUserDto>() { peterUser };
            _loginUsersDaoMock.Setup(dao => dao.GetLoggedUsers())
                .ReturnsAsync(users);
            
            await _sut.GetLoggedInUsers();
            
            _loginUsersDaoMock.Verify(dao => dao.GetLoggedUsers(), Times.Once);
        }
        
        [Fact]
        public async void Get_Logged_In_Users_Returns_Users()
        {
            var peterUser = new LoginUserDto()
            {
                SteamId = "133",
                AccountName = "Peter",
                MostRecent = true,
                PasswordRemembered = true,
                PersonaName = "PeterMieter",
                Timestamp = "1312345612"
            };

            List<LoginUserDto> users = new List<LoginUserDto>() { peterUser };
            _loginUsersDaoMock.Setup(dao => dao.GetLoggedUsers())
                .ReturnsAsync(users);
            
            var result = await _sut.GetLoggedInUsers();
            
            Assert.True(result.Count == 1);
            Assert.Equal(expected: peterUser, actual: result[0]);
        }
    }
}