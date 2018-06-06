using System.Globalization;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.MathUtils;
using OpenTK;
using OpenTK.Graphics;
using THSharp.Game.Graphics;
using THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters.DrawablePlayers;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters.Pieces
{
    //TODO: get rid of all these ReSharper disables with the Character Upgrade mk. II
    public class Seal : Container
    {
        public Container Sign { get; private set; }

        private CircularContainer characterSigil;

        // ReSharper disable once NotAccessedField.Local
        private SpriteText rightValue;
        // ReSharper disable once NotAccessedField.Local
        private SpriteText leftValue;

        // ReSharper disable once NotAccessedField.Local
        private CircularProgress health;
        // ReSharper disable once NotAccessedField.Local
        private CircularProgress energy;

        private readonly DrawableTouhouSharpPlayer character;

        // ReSharper disable once NotAccessedField.Local
        private Sprite gear1;
        // ReSharper disable once NotAccessedField.Local
        private Sprite gear2;
        // ReSharper disable once NotAccessedField.Local
        private Sprite gear3;
        // ReSharper disable once NotAccessedField.Local
        private Sprite gear4;
        // ReSharper disable once NotAccessedField.Local
        private Sprite gear5;

        public Seal(DrawableTouhouSharpPlayer character)
        {
            this.character = character;
        }

        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            Color4 lightColor = character.Character.PrimaryColor.Lighten(0.5f);
            Color4 darkColor = character.Character.PrimaryColor.Darken(0.5f);

            Size = new Vector2(90);

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            AlwaysPresent = true;

            Children = new Drawable[]
            {
                Sign = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,

                    Size = new Vector2(0.6f),

                    Alpha = 0,
                    AlwaysPresent = true,

                    Children = new Drawable[]
                    {
                        characterSigil = new CircularContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Masking = true,
                        },
                        new Sprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(2f),

                            Colour = character.Character.PrimaryColor,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.GetSkinTextureElement("seal"),
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.2f,
                    Size = new Vector2(1.5f),
                    Padding = new MarginPadding(-Blur.KernelSize(5)),
                    Child = (health = new CircularProgress
                    {
                        RelativeSizeAxes = Axes.Both,
                        InnerRadius = 0.05f,
                        Colour = character.Character.ComplementaryColor
                    }).WithEffect(new GlowEffect
                    {
                        Colour = character.Character.ComplementaryColor,
                        Strength = 2,
                        PadExtent = true
                    }),
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.2f,
                    Size = new Vector2(1.75f),
                    Padding = new MarginPadding(-Blur.KernelSize(5)),

                    Child = (energy = new CircularProgress
                    {
                        RelativeSizeAxes = Axes.Both,
                        InnerRadius = 0.05f,
                        Colour = character.Character.SecondaryColor
                    }).WithEffect(new GlowEffect
                    {
                        Colour = character.Character.SecondaryColor,
                        Strength = 2,
                        PadExtent = true
                    }),
                },
                new Container
                {
                    Position = new Vector2(-30, 0),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreRight,

                    Child = (leftValue = new SpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreRight,
                        Colour = character.Character.ComplementaryColor,
                        Font = "Venera",
                        TextSize = 16,
                        Alpha = 0.75f,
                    }).WithEffect(new GlowEffect
                    {
                        Colour = Color4.Transparent,
                        PadExtent = true,
                    }),
                },
                new Container
                {
                    Position = new Vector2(30, 0),
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreLeft,

                    Child = (rightValue = new SpriteText
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreLeft,
                        Colour = character.Character.SecondaryColor,
                        Font = "Venera",
                        TextSize = 16,
                        Alpha = 0.75f,
                    }).WithEffect(new GlowEffect
                    {
                        Colour = Color4.Transparent,
                        PadExtent = true,
                    }),
                },
            };

            switch (character.Character.Name)
            {
                case "SakuyaIzayoi":
                    characterSigil.Children = new Drawable[]
                    {
                        gear1 = new Sprite
                        {
                            Colour = lightColor,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.GetSkinTextureElement("gearSmall"),
                            Position = new Vector2(-41, 10),
                        },
                        gear2 = new Sprite
                        {
                            Colour = character.Character.PrimaryColor,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.GetSkinTextureElement("gearMedium"),
                            Position = new Vector2(-4, 16),
                        },
                        gear3 = new Sprite
                        {
                            Colour = darkColor,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.GetSkinTextureElement("gearLarge"),
                            Position = new Vector2(-16, -34),
                        },
                        gear4 = new Sprite
                        {
                            Colour = character.Character.PrimaryColor,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.GetSkinTextureElement("gearMedium"),
                            Position = new Vector2(35, -40),
                        },
                        gear5 = new Sprite
                        {
                            Colour = lightColor,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.GetSkinTextureElement("gearSmall"),
                            Position = new Vector2(33, 8),
                        },
                    };
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();

            Sign.RotateTo((float)(-Clock.CurrentTime / 1000 * 90) * 0.1f);

            Sign.Alpha = (float)character.Energy / (float)(character.Character.MaxEnergy * 2);
            energy.Current.Value = character.Energy / character.Character.MaxEnergy;

            health.Current.Value = character.Health / character.Character.MaxHealth;
            
            switch (character.Character.Name)
            {
                case "Sakuya Izayoi":
                    const float speed = 0.25f;
                    gear1.RotateTo((float)(Clock.CurrentTime / 1000 * 90) * 1.25f * speed);
                    gear2.RotateTo((float)(-Clock.CurrentTime / 1000 * 90) * 1.1f * speed);
                    gear3.RotateTo((float)(Clock.CurrentTime / 1000 * 90) * speed);
                    gear4.RotateTo((float)(-Clock.CurrentTime / 1000 * 90) * 1.1f * speed);
                    gear5.RotateTo((float)(Clock.CurrentTime / 1000 * 90) * 1.25f * speed);
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    if (character is DrawableSakuya s) leftValue.Text = s.SetRate.ToString(CultureInfo.InvariantCulture);
                    break;
            }
        }
    }
}
