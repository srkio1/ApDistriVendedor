using System;
using System.Collections.Generic;
using System.Text;

namespace ABM.Datos
{
    public class DetalleVenta
    {
        public int id_dv { get; set; }
        public int cantidad { get; set; }
        public string nombre_producto { get; set; }
        public decimal precio_producto { get; set; }
        public decimal descuento { get; set; }
        public int factura { get; set; }
    }
}
