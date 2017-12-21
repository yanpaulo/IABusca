using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IABusca.WebApp.Models
{
    public class Busca
    {
        public TipoAlgoritmo A1 { get; set; }

        public TipoAlgoritmo A2 { get; set; }

        [Required]
        public string LocalOrigem { get; set; }

        [Required]
        public string LocalDestino { get; set; }
    }
}