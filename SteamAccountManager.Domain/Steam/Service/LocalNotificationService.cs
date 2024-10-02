using SteamAccountManager.Domain.Steam.Storage;

namespace SteamAccountManager.Domain.Steam.Service;

public class LocalNotificationService
{
    private readonly ILocalNotificationClient _client;
    private readonly INotificationConfigStorage _config;

    public LocalNotificationService(ILocalNotificationClient client, INotificationConfigStorage config)
    {
        _client = client;
        _config = config;
    }

    public async Task Send(Notification notification)
    {
        if ((await _config.Get())?.IsAllowedToSendNotification != true)
            return;
            
        _client.Send(notification);
    }
}