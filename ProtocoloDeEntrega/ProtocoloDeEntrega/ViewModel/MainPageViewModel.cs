using ProtocoloDeEntrega.Interface;
using ProtocoloDeEntrega.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProtocoloDeEntrega.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private Protocolo protocolo{get;set;}
        
        public string Data
        {
            get { return protocolo.Data; }
            set { protocolo.Data = value; }
        }

        public string Horario
        {
            get { return protocolo.Hora; }
            set
            {
                protocolo.Hora = value;
                OnPropertyChanged();
            }
        }

        public string Local
        {
            get { return protocolo.Local; }
            set
            {
                protocolo.Local = value;
                OnPropertyChanged();
            }
        }
        
        public string Entrega
        {
            get { return protocolo.Entrega; }
            set { protocolo.Entrega = value;
                OnPropertyChanged();
            }
        }


        public byte[] Assinatura
        {
            get { return protocolo.Assinatura; }
            set { protocolo.Assinatura = value; }
        }
        
        public MainPageViewModel()
        {
            protocolo = new Protocolo();
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => Horario = DateTime.Now.ToString("HH:mm"));
                return true;
            });
            Data = DateTime.Now.ToString("dd/MM/yyyy");
        }


        public void GerarPDF()
        {
            var gerou = DependencyService.Get<ICreatePDF>().CreatePDF(protocolo);

            if (gerou)
                Limpar();
            else
                MessagingCenter.Send<Exception>(new Exception("Erro ao criar arquivo"), "ErroAssinatura");
        }

        public void Limpar()
        {
            Entrega = "";
            Local = "";
            Assinatura = null;
            MessagingCenter.Send<string>("", "LimpaAssinatura");
        }
    }
}
