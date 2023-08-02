using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Integrador_1
{
    public class Auto
    {
        Persona dueno;
        public Auto(string patente, string marca="", string modelo="", string año = "" , decimal precio=0m)
        {
            Patente=patente;
            Marca=marca;
            Modelo=modelo;
            Año=año;
            Precio=precio;
        }
        public Auto(Auto pAuto) : this(pAuto.Patente,pAuto.Marca,pAuto.Modelo,pAuto.Año,pAuto.Precio)
        {
           if(pAuto.RetornaDueno()!=null)
            {
                this.CargaDueno=new Persona(pAuto.RetornaDueno());
            }
        }
        public Auto() { }

        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Año { get; set; }
        public decimal Precio { get; set; }
        public Persona CargaDueno { set { dueno=value; } }

        public Persona RetornaDueno() { return dueno==null ? null:  new Persona(dueno); }
    }
}
