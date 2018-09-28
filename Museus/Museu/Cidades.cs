namespace Museus.Museu{
    public class Cidades{
        private uint _paisId;
        private uint _id;
        private string _nome;

        public uint PaisID { get => _paisId; set => _paisId = value; }
        public uint ID { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
    }
}
