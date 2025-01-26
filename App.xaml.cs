using Proiect_MDP_Mobile.Data;

namespace Proiect_MDP_Mobile
{
    public partial class App : Application
    {
        static RacketShopDatabase database;

        public static RacketShopDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RacketShopDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RacketShop.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
