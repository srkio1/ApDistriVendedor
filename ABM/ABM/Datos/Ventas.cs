using System;
using System.Collections.Generic;
using System.Text;

namespace ABM.Datos
{
    public class Ventas
    {
        public int id_venta { get; set; }
        public DateTime fecha { get; set; }
        public int numero_factura { get; set; }
        public DateTime hora { get; set; }
        public string cliente { get; set; }
        public string vendedor { get; set; }
        public string tipo_venta { get; set; }
        public decimal descuento { get; set; }
        public decimal saldo { get; set; }
        public decimal total { get; set; }
    }
}
