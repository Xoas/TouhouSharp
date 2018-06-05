using osu.Framework.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using osu.Framework.Platform;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes
{
    //Source for most of this: osu.Game.Rulesets.RulesetStore.cs
    public static class GamemodeStore
    {
        /// <summary>
        /// List of all currently loaded Gamemodes
        /// </summary>
        public static List<Gamemode> LoadedGamemodes = new List<Gamemode>();

        /// <summary>
        /// Called when a new Gamemode is added
        /// </summary>
        public static Action<Gamemode> OnGamemodeAdd;

        /// <summary>
        /// Called when a gamemode is lost
        /// </summary>
        public static Action<Gamemode> OnGamemodeRemoved;

        /// <summary>
        /// Will try and find the specified gamemode by name, if it is not found return null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Gamemode GetGamemode(string name)
        {
            foreach (Gamemode gamemode in LoadedGamemodes)
                if (gamemode.Name == name)
                    return gamemode;
            return null;
        }

        /// <summary>
        /// Will try and find the specified gamemode by name, if it is not found return one from the list of loaded gamemodes
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Gamemode GetWorkingGamemode(string name = "")
        {
            foreach (Gamemode gamemode in LoadedGamemodes)
                if (gamemode.Name == name)
                    return gamemode;
            return LoadedGamemodes.FirstOrDefault();
        }

        private static Dictionary<Assembly, Type> loadedAssemblies = new Dictionary<Assembly, Type>();

        private const string gamemode_prefix = "THSharp.Gamemodes";

        public static void ReloadGamemodes(THSharpSkinElement textures, Storage storage)
        {
            foreach (Gamemode g in LoadedGamemodes)
                OnGamemodeRemoved?.Invoke(g);

            LoadedGamemodes = new List<Gamemode>();

            loadedAssemblies = new Dictionary<Assembly, Type>();

            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory, $"{gamemode_prefix}.*.dll"))
            {
                var filename = Path.GetFileNameWithoutExtension(file);

                if (loadedAssemblies.Values.Any(t => t.Namespace == filename))
                    return;

                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    loadedAssemblies[assembly] = assembly.GetTypes().First(t => t.IsPublic && t.IsSubclassOf(typeof(Gamemode)));
                }
                catch (Exception)
                {
                    Logger.Log("Error loading a gamemode!", LoggingTarget.Runtime, LogLevel.Error);
                }
            }

            var instances = loadedAssemblies.Values.Select(g => (Gamemode)Activator.CreateInstance(g)).ToList();

            //add all official modes in order
            foreach (Gamemode g in instances.Where(g => g.OfficialID != null).OrderBy(g => g.OfficialID))
            {
                Logger.Log("Successfully loaded official gamemode: " + g.Name);
                LoadedGamemodes.Add(g);
                g.LoadDependencies(textures, storage);
                OnGamemodeAdd?.Invoke(g);
            }

            //add any other modes
            foreach (Gamemode g in instances.Where(g => g.OfficialID == null))
            {
                Logger.Log("Successfully loaded un-official gamemode: " + g.Name);
                LoadedGamemodes.Add(g);
                g.LoadDependencies(textures, storage);
                OnGamemodeAdd?.Invoke(g);
            }
        }
    }
}
