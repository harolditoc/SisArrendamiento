﻿    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace SisArrendamiento.Models;

    public partial class LuzBaño
    {
        public int Codigo { get; set; }

        public decimal? AnteriorLuzLectura { get; set; }

        public decimal? ActualLuzLectura { get; set; }

        public decimal? KwConsumido { get; set; }

        public decimal? Monto { get; set; }

        public virtual ICollection<Alquiler> Alquilers { get; } = new List<Alquiler>();
    }
