using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador_1
{
    public class Persona
    {
        List<Auto> la;

        public Persona() { la=new List<Auto>(); }
        public Persona( string pDNI,string pNombre="",string pApellido="") : this() { DNI=pDNI;Nombre=pNombre;Apellido=pApellido; }
        public Persona(Persona pPersona): this(pPersona.DNI,pPersona.Nombre,pPersona.Apellido) { }
        public string DNI { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public void AgregarAuto(Auto pAuto)
        {
            la.Add(new Auto(pAuto));
        }
        public List<Auto> RotornaListaAutos()
        {
            return (from a in la select new Auto(a.Patente, a.Marca, a.Modelo, a.Año, a.Precio)).ToList<Auto>();
        }
    }
}
