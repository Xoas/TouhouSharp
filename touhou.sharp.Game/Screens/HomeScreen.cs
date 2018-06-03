﻿using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using Symcol.Core.Graphics.Containers;

namespace touhou.sharp.Game.Screens
{
    public class HomeScreen : THSharpMenuScreen
    {
        public HomeScreen()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Blue
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,

                    Colour = Color4.White,
                    TextSize = 24,
                    Text = "There is no game yet, check back later!"
                },
                new SymcolClickableContainer
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Masking = true,

                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.12f, 0.08f),
                    Position = new Vector2(-10),

                    CornerRadius = 16, 
                    BorderThickness = 4,
                    Action = () => Push(new EditorScreen()),

                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Color4.Red,
                            RelativeSizeAxes = Axes.Both
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,

                            Colour = Color4.White,
                            TextSize = 24,
                            Text = "Editor"
                        }
                    }
                },
                new SymcolClickableContainer
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Masking = true,

                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.12f, 0.08f),
                    Position = new Vector2(10, -10),

                    CornerRadius = 16,
                    BorderThickness = 4,
                    Action = () => Push(new PlayerScreen()),

                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Color4.Red,
                            RelativeSizeAxes = Axes.Both
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,

                            Colour = Color4.White,
                            TextSize = 24,
                            Text = "Player"
                        }
                    }
                }
            };
        }
    }
}
