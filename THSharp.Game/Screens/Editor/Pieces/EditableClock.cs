using osu.Framework.Timing;

namespace THSharp.Game.Screens.Editor.Pieces
{
    public class EditableClock : DecoupleableInterpolatingFramedClock
    {
        public double DifficultyLength { get; set; } = 60 * 1000;

        public EditableClock()
        {
            IsCoupled = false;
        }
    }
}
