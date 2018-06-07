using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Screens.Editor.Pieces.Bars
{
    public class BottomBar : SymcolContainer
    {
        public BottomBar(EditableClock e)
        {
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;

            RelativeSizeAxes = Axes.Both;

            Height = 0.18f;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                    Alpha = 0.8f
                },
                new Scrubber(e)
            };
        }

        //Source: osu.Game.Screens.Edit.Components.Timelines.Summary.Parts.MarkerPart.cs
        private class Scrubber : SymcolContainer
        {
            private readonly Box line;

            private readonly EditableClock editableClock;

            public Scrubber(EditableClock e)
            {
                editableClock = e;

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativeSizeAxes = Axes.Both;

                Width = 0.5f;
                Height = 0.4f;

                Children = new Drawable[]
                {

                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        Height = 4
                    },
                    new Box
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreRight,
                        RelativeSizeAxes = Axes.Y,
                        Height = 0.8f,
                        Width = 4
                    },
                    new Box
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreLeft,
                        RelativeSizeAxes = Axes.Y,
                        Height = 0.8f,
                        Width = 4
                    },
                    line = new Box
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreRight,
                        RelativeSizeAxes = Axes.Y,
                        Height = 0.75f,
                        Width = 4
                    },
                    new SymcolClickableContainer
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreLeft,

                        Masking = true,
                        CornerRadius = 10,
                        Size = new Vector2(20),
                        Position = new Vector2(10, 0),

                        Child = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Red,
                            Alpha = 0.8f,
                        },

                        Action = togglePause
                    }
                };
            }

            private void togglePause()
            {
                if (editableClock.IsRunning)
                    editableClock.Stop();
                else
                    editableClock.Start();
            }

            protected override bool OnDragStart(InputState state) => true;
            protected override bool OnDragEnd(InputState state) => true;

            protected override bool OnDrag(InputState state)
            {
                seekToPosition(state.Mouse.NativeState.Position);
                return true;
            }

            protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
            {
                seekToPosition(state.Mouse.NativeState.Position);
                return true;
            }

            private void seekToPosition(Vector2 screenPosition)
            {
                float markerPos = MathHelper.Clamp(ToLocalSpace(screenPosition).X, 0, DrawWidth);
                editableClock.Seek(markerPos / DrawWidth * editableClock.DifficultyLength);
            }

            protected override void Update()
            {
                base.Update();
                line.X = (float)getX(editableClock.CurrentTime);
            }

            private double getX(double value)
            {
                double scale = (0 - DrawWidth) / (0 - editableClock.DifficultyLength);
                return DrawWidth + ((value - editableClock.DifficultyLength) * scale);
            }
        }
    }
}
