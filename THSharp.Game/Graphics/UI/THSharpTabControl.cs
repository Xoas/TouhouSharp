using osu.Framework.Graphics.UserInterface;

namespace THSharp.Game.Graphics.UI
{
    public class THSharpTabControl<T> : TabControl<T>
        where T : struct 
    {
        protected override Dropdown<T> CreateDropdown()
        {
            throw new System.NotImplementedException();
        }

        protected override TabItem<T> CreateTabItem(T value)
        {
            throw new System.NotImplementedException();
        }
    }
}
