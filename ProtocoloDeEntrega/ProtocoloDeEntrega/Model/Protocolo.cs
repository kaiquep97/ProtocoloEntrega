using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocoloDeEntrega.Model
{
    public class Protocolo
    {
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Local { get; set; }
        public string Entrega { get; set; }
        public byte[] Assinatura { get; set; }
    }
}
