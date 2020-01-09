using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace ABM.Datos
{
    public class Producto
    {        
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string nombre_tipo_producto { get; set; }
        public string nombre_sub_producto { get; set; }
        public decimal stock { get; set; }
        public decimal stock_valorado { get; set; }
        public decimal promedio { get; set; }
        public string display_text_nombre { get { return $"{nombre} {nombre_sub_producto}"; } }
        public string display_text_tipo { get { return $"{nombre_tipo_producto} "; } }
        public decimal precio_venta { get; set; }
        public decimal producto_alerta { get; set; }
    }
}