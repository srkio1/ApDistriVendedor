﻿using System;
using System.Collections.Generic;
using System.Text;


namespace ABM.Datos
{
    public class Cliente
    {        
        public int id_cliente { get; set; }
        public string nombre { get; set; }
        public string ubicacion_latitud { get; set; }
        public string ubicacion_longitud { get; set; }
        public int telefono { get; set; }
        public int nit { get; set; }
    }
}
