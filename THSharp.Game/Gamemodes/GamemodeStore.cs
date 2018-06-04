using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace THSharp.Game.Gamemodes
{
    //Source for most of this: osu.Game.Rulesets.RulesetStore.cs
    public class GamemodeStore
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

        private static Dictionary<Assembly, Type> loadedAssemblies = new Dictionary<Assembly, Type>();

        private const string gamemode_prefix = "THSharp.Gamemodes";

        public static void ReloadGamemodes()
        {
            foreach (Gamemode g in LoadedGamemodes)
                OnGamemodeRemoved(g);

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
                }
            }

            var instances = loadedAssemblies.Values.Select(g => (Gamemode)Activator.CreateInstance(g)).ToList();

            //add all official modes in order
            foreach (Gamemode g in instances.Where(g => g.OfficialID != null).OrderBy(g => g.OfficialID))
            {
                LoadedGamemodes.Add(g);
                OnGamemodeAdd(g);
            }

            //add any other modes
            foreach (Gamemode g in instances.Where(g => g.OfficialID == null))
            {
                LoadedGamemodes.Add(g);
                OnGamemodeAdd(g);
            }
        }
    }
}
