using cep.Service;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace cep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BotaoBuscarCep.Clicked += BuscarCepAsync;
        }

        private async void BuscarCepAsync(object sender, EventArgs args)
        {
            var cep = Cep.Text.Trim();

            if (CepEhValido(cep))
            {
                try
                {
                    var end = await CepService.ObterPorCep(cep);

                    if (end != null)
                        Resultado.Text = $"Endereço: {end.Logradouro}\nBairro: {end.Bairro}\nCidade: {end.Localidade} \nEstado: {end.Uf} ";
                    else
                        await DisplayAlert("DEU RUIM", $"O endereço não foi encontrado para o CEP informado: {cep}", "OK");
                }
                catch (Exception e)
                {
                    await DisplayAlert("DEU RUIM DEMAIS", e.Message, "OK");
                }
            }
        }

        private bool CepEhValido(string cep)
        {
            var valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("DEU RUIM", "O CEP deve conter 8 caracteres.", "OK");

                valido = false;
            }

            if (!int.TryParse(cep, out _))
            {
                DisplayAlert("DEU RUIM", "O CEP deve ser composto apenas por números.", "OK");

                valido = false;
            }

            return valido;
        }
    }
}
