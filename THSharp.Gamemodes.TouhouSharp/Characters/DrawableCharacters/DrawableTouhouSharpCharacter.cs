using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using Symcol.Core.Graphics.Containers;
using Symcol.Core.Graphics.Sprites;
using THSharp.Game.Gamemodes.Characters.DrawableCharacters;
using THSharp.Game.Gamemodes.Projectiles.DrawableProjectiles;
using THSharp.Game.Graphics;
using THSharp.Gamemodes.TouhouSharp.Playfield;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters
{
    public abstract class DrawableTouhouSharpCharacter : DrawableCharacter
    {
        protected readonly TouhouSharpPlayfield TouhouSharpPlayfield;

        public readonly TouhouSharpCharacter TouhouSharpCharacter;

        protected SymcolContainer RealityContainer { get; set; }
        protected SymcolSprite RealitySpriteStill { get; set; }
        protected SymcolSprite RealitySpriteRight { get; set; }
        protected SymcolSprite RealitySpriteLeft { get; set; }

        protected SymcolContainer SoulContainer { get; set; }
        protected SymcolSprite SoulSpriteStill { get; set; }
        protected SymcolSprite SoulSpriteRight { get; set; }
        protected SymcolSprite SoulSpriteLeft { get; set; }

        protected CircularContainer VisibleHitbox;

        protected DrawableTouhouSharpCharacter(TouhouSharpCharacter c, TouhouSharpPlayfield playfield)
            : base(c, playfield)
        {
            TouhouSharpCharacter = c;
            TouhouSharpPlayfield = playfield;

            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;
        }

        protected override void LoadAnimationSprites(THSharpSkinElement textures)
        {
            base.LoadAnimationSprites(textures);

            AddRange(new Drawable[]
            {
                SoulContainer = new SymcolContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Character.PrimaryColor,
                    Alpha = 1,
                    Children = new Drawable[]
                    {
                        SoulSpriteStill = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 1,
                        },
                        SoulSpriteRight = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                        SoulSpriteLeft = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                    }
                },
                RealityContainer = new SymcolContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                    Children = new Drawable[]
                    {
                        RealitySpriteStill = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 1,
                        },
                        RealitySpriteRight = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                        RealitySpriteLeft = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                    }
                },
                VisibleHitbox = new SymcolCircularContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                    Size = new Vector2((float)Character.HitboxWidth + (float)Character.HitboxWidth / 4),
                    BorderColour = Character.PrimaryColor,
                    BorderThickness = (float)Character.HitboxWidth / 4,
                    Masking = true,

                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    EdgeEffect = new EdgeEffectParameters
                    {

                        Radius = (float)Character.HitboxWidth / 2,
                        Type = EdgeEffectType.Shadow,
                        Colour = Character.PrimaryColor.Opacity(0.5f)
                    }
                }
            });
        }

        protected override void Update()
        {
            base.Update();

            foreach (Drawable draw in TouhouSharpPlayfield.GameField.Current)
                if (draw is DrawableProjectile drawableProjectile && drawableProjectile.Hitbox != null)
                    ParseProjectiles(drawableProjectile);
        }
    }
}
