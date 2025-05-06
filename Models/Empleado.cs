using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models {


    public class Empleado {
        [Key]
        public int ID { get; set; }

        public int Id_Status { get; set; }

        [ForeignKey(nameof(Puesto))]
        public int Id_Puesto { get; set; }

        [ForeignKey(nameof(Jefe))]
        public int? Id_Jefe { get; set; }

        [ForeignKey(nameof(Ubicacion))]
        public int? Id_Ubicacion { get; set; }


        public Status? Status { get; set; }
        public Puesto? Puesto { get; set; }
        public Empleado? Jefe { get; set; }
        public Ubicacion? Ubicacion { get; set; }
        public User? User { get; set; }
        public Empleado_Perfil? Perfil { get; set; }

        public ICollection<ContactosEmpleado> Contactos { get; set;} = new List<ContactosEmpleado>();
        public ICollection<Empleados_Direccion> Direcciones { get; set;} = new List<Empleados_Direccion>();

    }


}