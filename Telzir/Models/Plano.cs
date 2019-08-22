using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Telzir.Models
{
    public class Plano
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório", AllowEmptyStrings = false)]        
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do plano é obrigatória", AllowEmptyStrings = false)]        
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a porcentagem do plano", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal Porcentagem { get; set; }
        public DateTime Criado { get; set; }
        public DateTime Editado { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Pacote> Pacotes { get; set; } 

    }
}