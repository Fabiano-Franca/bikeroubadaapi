

using BikeRoubada.Business.Notificacoes;

namespace BikeRoubada.Business.Interfaces
{
    public interface INotificador
    {
        public bool TemNotificacao();
        public List<Notificacao> ObterNotificacoes();
        public void Handle(Notificacao notificacao);
    }
}
