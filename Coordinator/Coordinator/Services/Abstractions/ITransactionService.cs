namespace Coordinator.Services.Abstractions
{
    public interface ITransactionService
    {
        Task<Guid> CreateTransactionAsync();
        Task PrepareServicesAsync(Guid transactionId);//servislerin hazır olup olmadığını kontrol eder.

        //NodeState tablosundaki tüm serislerin Ready durumu eğer ready ise 2. aşamaya geçebiliriz.
        Task<bool> CheckReadyServicesAsync(Guid transactionId);

        //CheckReadyServices kontrolü gerçekleştirdikten sonra 2. aşamaya geçmek için talimat verir.
        Task CommitAsync(Guid transactionId); 

        //Tüm servislerin işlerini başarılı bir şekilde tamamlayıp tamamlamadığını kontrol eder. 
        Task<bool> CheckTransactionStateServicesAsync(Guid transactionId);

        //İşlemler başarılı bir şekilde tamamlanmadı
        //ise yapılan işlemler servislerden geri alınması için talimat verilir.
        Task RoolbackAsync(Guid transactionId);
 

    }
}
