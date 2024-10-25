using Plugin.Maui.Audio;

namespace Hack4Edu
{
    public partial class App : Application
    {
        private readonly IAudioManager _audioManager;
        public App(IAudioManager audioManager)
        {
            InitializeComponent();
            this._audioManager = audioManager;

            MainPage = new NavigationPage(new MainPage(_audioManager));
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
