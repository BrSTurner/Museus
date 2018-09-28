namespace Museus.Museu{
    public class Museu{
        private string _nome;
        private double _valor;
        private uint _cidade;
        private byte _nota;
        public string Nome { get => _nome; private set => _nome = value; }
        public double Valor { get => _valor; private set => _valor = value; }
        public uint Cidade { get => _cidade; private set => _cidade = value; }
        public byte Nota { get => _nota; private set => _nota = value; }
        public Museu(string Nome, double Valor, uint Cidade, byte Nota){
            this.Nome = Nome;
            this.Valor = Valor;
            this.Cidade = Cidade;
            this.Nota = Nota;
        }
    }
}
