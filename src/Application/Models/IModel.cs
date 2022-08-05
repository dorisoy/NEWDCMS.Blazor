namespace DCMS.Application.Models
{
    public interface IModel<TId> 
    {
        public TId UniqueId { get; set; }
    }
}