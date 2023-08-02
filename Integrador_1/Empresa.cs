using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Integrador_1
{
    public class Empresa
    {
        List<Auto> la;
        List<Persona> lp;
        public Empresa() { la=new List<Auto>();lp=new List<Persona>(); }    
        
        public void AgregarPersona(Persona pPersona)
        {
            lp.Add(new Persona(pPersona));
        }
        public void BorrarPersona(Persona pPersona)
        {

            try
            {
                Persona p = lp.Find(x => x.DNI==pPersona.DNI);
                if (p==null) throw new Exception("La persona no se puede borrar porque no existe !!!");
                //todo: completar cuando esté el método cantidad de autos enj persona la validación que antes de borrar se asegura que la persona no posea autos
                foreach(Auto a in p.RotornaListaAutos())
                {
                    la.Find(x => x.Patente==a.Patente).CargaDueno=null;
                }
                lp.Remove(p);
            }
            catch (Exception ex) { throw ex;}
        }
        public void ModificarPersona(Persona pPersona)
        {

            try
            {
                Persona p = lp.Find(x => x.DNI==pPersona.DNI);
                if (p==null) throw new Exception("La persona no se puede modificar porque no existe !!!");
                p.Nombre=pPersona.Nombre;
                p.Apellido=pPersona.Apellido;
                //todo: ver si la persona tiene autos y acutualizarle el nombre y apellido a cada uno
            }
            catch (Exception ex) { throw ex; }
        }
        public object RotornaListaPersonas()
        {
            return (from p in lp select new { DNI = p.DNI, Apellido_y_Nombre = $"{p.Apellido}, {p.Nombre}" }).ToArray();
        }
        public bool ValidaDNI(Persona pPersona)
        {
           return lp.Exists(x => x.DNI==pPersona.DNI);
        }

        public void AgregarAuto(Auto pAuto)
        {
            la.Add(new Auto(pAuto));
        }
        public List<Auto> RotornaListaAutos()
        {
            return (from a in la select new Auto(a.Patente,a.Marca,a.Modelo,a.Año,a.Precio)).ToList<Auto>();
        }

        public void AsignaAutoAPersona(Auto pAuto, Persona pPersona)
        {

            try
            {
                Persona p = lp.Find(x => x.DNI==pPersona.DNI);
                Auto a = la.Find(x => x.Patente==pAuto.Patente);
                if (p==null || a==null) throw new Exception("Algún componente de la asignación el nulo !!!");
                if (a.RetornaDueno()!=null) throw new Exception("El auto posee dueño !!!");
                p.AgregarAuto(a);
                a.CargaDueno=p;
            }
            catch (Exception ex) { throw ex; }

            

        }
        public object RetornaListaDeAutosDePersona(Persona pPersona)
        {

            try
            {
                Persona p = lp.Find(x => x.DNI==pPersona.DNI);
                if (p==null) throw new Exception("La persona no existe !!!");
                return (from a in p.RotornaListaAutos() select new { Patente = a.Patente, Marca = a.Marca, Modelo = a.Modelo, Año = a.Año, Precio = a.Precio }).ToArray(); ;

            }
            catch (Exception ex) { throw ex; }

        }
        public object RetornaListaDeAutosGrilla4()
        {

            try
            {
                return (from a in la select new { Patente = a.Patente, 
                                                  Marca = a.Marca, 
                                                  Modelo = a.Modelo, 
                                                  Año = a.Año, 
                                                  Precio = a.Precio,
                                                  DNI= a.RetornaDueno()==null?"":a.RetornaDueno().DNI,
                                                  Apellido_Nombre= a.RetornaDueno()==null ? "" : a.RetornaDueno().Apellido + ", " + a.RetornaDueno().Nombre }).ToArray(); 

            }
            catch (Exception ex) { throw ex; }

        }
    }
}
