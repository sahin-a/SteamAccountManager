﻿namespace SteamAccountManager.Application.Steam.Model
{
    public class SteamAccount
    {
        public string SteamId { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public bool IsLoginValid { get; set; }
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
        
        private SteamAccount()
        {
            
        }
        
        public class Builder
        {
            private SteamAccount _steamAccount { get; set; }

            public Builder()
            {
                _steamAccount = new();
            }

            public Builder SetData(SteamProfile steamProfile)
            {
                _steamAccount.SteamId = steamProfile.Id;
                _steamAccount.Profile = steamProfile.Url;
                _steamAccount.Avatar = steamProfile.Avatar;
                _steamAccount.Username = steamProfile.Username;
                _steamAccount.IsVacBanned = steamProfile.IsVacBanned;
                _steamAccount.IsCommunityBanned = steamProfile.IsCommunityBanned;
                
                return this;
            }

            public Builder SetData(SteamLoginUser steamLoginUser)
            {
                _steamAccount.AccountName = steamLoginUser.AccountName;
                _steamAccount.IsLoginValid = steamLoginUser.IsLoginTokenValid;

                return this;
            }

            public SteamAccount Build()
            {
                return _steamAccount;
            }
        }
    }
}