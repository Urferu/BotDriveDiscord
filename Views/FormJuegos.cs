using System.Windows.Forms;
using DriveBot.Resources.Database;
using DriveBot.Resources.Utils;

namespace DriveBot.Views
{
    public partial class FormJuegos : Form
    {
        stdClassCSharp games;
        public FormJuegos()
        {
            InitializeComponent();
            games = stdClassCSharp.jsonToStdClass(FilesConfig.getGames());
        }

        private void FormJuegos_Load(object sender, System.EventArgs e)
        {
            foreach(stdClassCSharp game in games.toArray())
            {
                dataGridView1.Rows.Add(game["Titulo"]);
            }
        }
    }
}
