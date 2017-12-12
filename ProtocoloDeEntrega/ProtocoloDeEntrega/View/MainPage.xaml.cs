using ProtocoloDeEntrega.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProtocoloDeEntrega
{
	public partial class MainPage : ContentPage
	{
        private MainPageViewModel vm;

        public MainPage()
		{
			InitializeComponent();
            vm = new MainPageViewModel();
            BindingContext = vm;
		}
        

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            Stream img = await assinatura.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
            byte[] signature;
            
            using(BinaryReader binary = new BinaryReader(img))
            {
                binary.BaseStream.Position = 0;
                signature = binary.ReadBytes((int)img.Length);
            }

            vm.Assinatura = signature;

            vm.GerarPDF();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "LimpaAssinatura", (msg) => {
                assinatura.Clear();
            });

            MessagingCenter.Subscribe<Exception>(this, "ErroAssinatura", (msg) => {
                DisplayAlert("Erro", msg.Message, "OK");
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "LimpaAssinatura");
            MessagingCenter.Unsubscribe<Exception>(this, "ErroAssinatura");
        }
    }
}
