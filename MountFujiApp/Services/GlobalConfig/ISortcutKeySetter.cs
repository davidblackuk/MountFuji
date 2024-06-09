using MountFuji.Models.Keyboard;

namespace MountFuji.Services.GlobalConfig;

public interface ISortcutKeySetter
{
    void SetShortcutKey(KeyboardShortcuts shortcuts, ShortcutKey key, string newValue);
}