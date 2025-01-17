using Coordinator.Enums;

namespace Coordinator.Models
{
    public record NodeState(Guid TransactionId)
    {
        public Guid Id { get; set; }
        public ReadyType IsReady {  get; set; }//1. aşamanın durumunu ifade eder.
        public TransactionState TransactionState {  get; set; }//2. aşamanın neticesinde işlemin başarılı tamamlanıp, tamamlanmadığını ifade eder.
        public Node Node { get; set; }
    }
}
