using System;
using System.Collections.Generic;
using System.Text;

namespace ABM.Datos
{
    public class Compras
    {
        public int id_compra{ get; set; }
        //public decimal Saldo { get; set; }
        public decimal saldo { get; set; }
        public decimal total { get; set; }        
        public DateTime fecha_compra { get; set; }
        public DateTime hora { get; set; }
        public int numero_factura { get; set; }
        public string proveedor { get; set; }
        
    }
}
