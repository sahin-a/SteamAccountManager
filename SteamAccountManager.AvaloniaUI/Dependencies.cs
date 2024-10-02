using Autofac;
using DI;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common.Discord;
using SteamAccountManager.AvaloniaUI.Mappers;
using SteamAccountManager.AvaloniaUI.Notifications;
using SteamAccountManager.AvaloniaUI.Services;
using SteamAccountManager.AvaloniaUI.ViewModels;
using SteamAccountManager.Domain.Steam.Observables;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.AvaloniaUI
{
    internal static class Dependencies
    {
        public static IContainer? Container { get; set; }

        public static void RegisterDependencies()
        {
            Container = new ContainerBuilder()
                .RegisterModules()
                .RegisterAvaloniaModule()
                .RegisterViewModels()
                .Build()
                .StartWatchers();
        }

        public static IContainer StartWatchers(this IContainer container)
        {
            container.Resolve<IAccountStorageWatcher>().Start();
            return container;
        }

        public static ContainerBuilder RegisterAvaloniaModule(this ContainerBuilder builder)
        {
            builder.RegisterType<DiscordRpcService>().SingleInstance();
            builder.RegisterType<InfoService>().SingleInstance();
            builder.RegisterType<AvatarService>().SingleInstance();
            builder.RegisterType<AccountMapper>().SingleInstance();
            builder.RegisterType<SwitcherViewSorter>().SingleInstance();
#if WINDOWS10_0_17763_0_OR_GREATER
            builder.RegisterType<WindowsLocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
#else
            builder.RegisterType<LegacyWindowsLocalNotificationClient>().As<ILocalNotificationClient>().SingleInstance();
#endif
            
            builder.RegisterType<LocalNotificationService>().SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterViewModel<AccountSwitcherViewModel>();
            builder.RegisterViewModel<SettingsViewModel>();

            return builder;
        }

        private static ContainerBuilder RegisterViewModel<ViewModel>(this ContainerBuilder builder)
            where ViewModel : RoutableViewModel
        {
            builder.RegisterType<ViewModel>()
                .WithParameter(new TypedParameter(typeof(IScreen), "screen"));

            return builder;
        }
    }
}