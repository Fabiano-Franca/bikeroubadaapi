using BikeRoubada.Business.Enums;

namespace BikeRoubada.Business.Models
{
    public class Arquivo : Entity
    {
        public string NomeArquivo { get; set; }
        public Guid? IdRoubo { get; set; }
        public Roubo? Roubo { get; set; }
        public Guid? IdBicicleta { get; set; }
        public Bicicleta? Bicicleta { get; set; }
        public TipoArquivo Tipo {  get; set; }
        public bool Destaque { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
