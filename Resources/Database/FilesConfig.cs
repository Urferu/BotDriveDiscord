using System.IO;

namespace DriveBot.Resources.Database
{
    public class FilesConfig
    {
        public static string getGames()
        {
            string games = "";
            StreamReader swGames = new StreamReader("games.json");
            games = swGames.ReadToEnd();
            swGames.Close();
            return games;
        }

        public static string getUpdates()
        {
            string updates = "";
            StreamReader swUpdates = new StreamReader("updates.json");
            updates = swUpdates.ReadToEnd();
            swUpdates.Close();
            return updates;
        }

        public static string getDlc()
        {
            string dlcs = "";
            StreamReader swDlcs = new StreamReader("dlcs.json");
            dlcs = swDlcs.ReadToEnd();
            swDlcs.Close();
            return dlcs;
        }
    }
}
