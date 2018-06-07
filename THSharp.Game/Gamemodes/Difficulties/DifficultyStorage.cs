using System.Collections.Generic;
using System.IO;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace THSharp.Game.Gamemodes.Difficulties
{
    public sealed class DifficultyStorage
    {
        private static Storage storage;

        public static List<DifficultyFile> DifficultyFiles = new List<DifficultyFile>();

        private const string difficulty_storage_directory = "Difficulties";

        //private const string difficulty_extension = ".ths";

        public DifficultyStorage(Storage storage)
        {
            try
            {
                DifficultyStorage.storage = storage.GetStorageForDirectory(difficulty_storage_directory);
                ReloadDifficulties();
            }
            catch
            {
                Logger.Log("There is no difficulties directory!", LoggingTarget.Database, LogLevel.Error);
            }
        }

        public static void ReloadDifficulties()
        {
            DifficultyFiles = new List<DifficultyFile>();

            foreach (string set in storage.GetDirectories(""))
            {
                Storage s = storage.GetStorageForDirectory(difficulty_storage_directory + "/" + set);
                foreach (string difficulty in s.GetFiles(""))
                {
                    using (Stream stream = storage.GetStream(difficulty, FileAccess.Read, FileMode.Open))
                    using (StreamReader r = new StreamReader(stream))
                        DifficultyFiles.Add(new DifficultyFile
                        {
                            FileName = difficulty,
                            FileContents = r.ReadToEnd()
                        });
                }
            }
        }
    }
}
