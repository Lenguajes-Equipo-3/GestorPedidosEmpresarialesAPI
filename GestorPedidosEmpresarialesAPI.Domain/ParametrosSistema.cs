using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
   public class ParametrosSistema
    {
        private int idParametro;
        private string nombreEmpresa;
        private string direccion;
        private string telefono;
        private string correo;
        private decimal impuestoVentas;
        private byte[]? logo; 
        private string moneda;
        private DateTime ultimaActualizacion;

        public ParametrosSistema(int idParametro,string nombreEmpresa,string direccion, string telefono, string correo, decimal impuestoVentas,byte[]? logo,string moneda,
        DateTime ultimaActualizacion )
        {
            this.idParametro = idParametro;
            this.nombreEmpresa = nombreEmpresa;
            this.direccion = direccion;
            this.telefono = telefono;
            this.correo = correo;
            this.impuestoVentas = impuestoVentas;
            this.logo = logo;
            this.moneda = moneda;
            this.ultimaActualizacion = ultimaActualizacion;
        }
        public ParametrosSistema() { }


        public int IdParametro { get=> idParametro; set => idParametro = value; }
        public string NombreEmpresa { get => nombreEmpresa ; set => nombreEmpresa = value; }
        public string Direccion { get => direccion ; set => direccion= value; }
        public string Telefono { get => telefono ; set => telefono= value; }
        public string Correo { get => correo; set => correo = value; }
        public decimal ImpuestoVentas { get => impuestoVentas; set => impuestoVentas = value; }
        public byte[]? Logo { get => logo ; set =>logo = value ; }
        public string Moneda { get => moneda; set => moneda = value ; }
        public DateTime UltimaActualizacion { get => ultimaActualizacion; set => ultimaActualizacion= value; }
    }
}
