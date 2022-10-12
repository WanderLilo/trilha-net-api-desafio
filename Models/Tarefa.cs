using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} é requerido !")]
        [StringLength(200,ErrorMessage = "{0} deve ter entre {2} e {1} caracteres",MinimumLength = 3 )]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}