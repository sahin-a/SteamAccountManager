using System.Collections.Generic;
using System.Linq;
using SteamAccountManager.AvaloniaUI.Models;

namespace SteamAccountManager.AvaloniaUI.ViewModels;

public class SwitcherViewSorter
{
    public List<Account> SortAccounts(IEnumerable<Account> accounts) => accounts
        .OrderByDescending(x => x.IsLoggedIn)
        .ThenByDescending(x => x.Rank.Level)
        .ThenBy(x => x.Name)
        .ThenBy(x => x.IsVacBanned).ToList();

    public List<Account> SortAccountsForManagement(IEnumerable<Account> accounts) => accounts
        .OrderBy(x => x.IsBlacklisted)
        .ToList();
}