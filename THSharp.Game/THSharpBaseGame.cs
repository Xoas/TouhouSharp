﻿using osu.Framework.Allocation;
using osu.Framework.Platform;
using THSharp.Game.Config;
using THSharp.Game.Graphics;

namespace THSharp.Game
{
    public class THSharpBaseGame : osu.Framework.Game
    {
        protected THSharpConfigManager THSharpConfigManager;

        protected THSharpSkinElement THSharpSkinElement;

        protected override string MainResourceFile => "THSharp.Game.Resources.dll";

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        public THSharpBaseGame()
        {
            Name = @"TouhouSharp";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
            dependencies.Cache(THSharpConfigManager);
            dependencies.Cache(THSharpSkinElement);

            Window.CursorState = CursorState.Hidden;
        }

        public override void SetHost(GameHost host)
        {
            if (THSharpConfigManager == null)
                THSharpConfigManager = new THSharpConfigManager(host.Storage);

            if (THSharpSkinElement == null)
                THSharpSkinElement = new THSharpSkinElement(host.Storage, THSharpConfigManager);

            base.SetHost(host);
        }

        protected override void Dispose(bool isDisposing)
        {
            THSharpConfigManager?.Save();
            base.Dispose(isDisposing);
        }
    }
}
