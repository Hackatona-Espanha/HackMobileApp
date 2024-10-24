namespace Hack4Edu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }


        /*
        EXMPLO PARA ALTERAR O IDIOMA DA APP EM TEMPO DE EXECUÇÃO
        LocalizationResourceManager.Istance.SetCulture(en-US)

        EXEMPLO PARA SETAR LOCALIZED STRINGS NO CODE-BEHIND
        text = LocalizationResourceManager.Instace[key].ToString()

        public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
        BindingContext = this;
        */

    }
}
