using osu.Framework.Allocation;
using osu.Framework.Platform;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes.Difficulties;
using THSharp.Game.Graphics;

namespace THSharp.Game
{
    public class THSharpBaseGame : osu.Framework.Game
    {
        protected THSharpConfigManager THSharpConfigManager;

        protected THSharpSkinElement THSharpSkinElement;

        protected DifficultyStorage DifficultyStorage;

        protected override string MainResourceFile => "THSharp.Game.Resources.dll";

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
            dependencies.Cache(THSharpConfigManager);
            dependencies.Cache(THSharpSkinElement);
            //dependencies.Cache(DifficultyStorage);

            Window.CursorState = CursorState.Hidden;
            Window.Title = @"TouhouSharp";
        }

        public override void SetHost(GameHost host)
        {
            if (THSharpConfigManager == null)
                THSharpConfigManager = new THSharpConfigManager(host.Storage);

            if (THSharpSkinElement == null)
                THSharpSkinElement = new THSharpSkinElement(host.Storage, THSharpConfigManager);

            if (DifficultyStorage == null)
                DifficultyStorage = new DifficultyStorage(host.Storage);

            base.SetHost(host);
        }

        protected override void Dispose(bool isDisposing)
        {
            THSharpConfigManager?.Save();
            base.Dispose(isDisposing);
        }
    }
}
