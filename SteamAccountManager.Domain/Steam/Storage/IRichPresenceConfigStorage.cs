﻿using SteamAccountManager.Domain.Common.Storage;
using SteamAccountManager.Domain.Steam.Configuration.Model;

namespace SteamAccountManager.Domain.Steam.Storage;

public interface IRichPresenceConfigStorage : IObjectStorage<RichPresenceConfig>
{
}