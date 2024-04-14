using Bogus;
using Spg.Payment.Infrastructure;

namespace Spg.Payment.Repository;
public class GenericRepository
{
    public readonly PaymentContext _paymentContext;
    public GenericRepository(PaymentContext paymentContext)
    {
        _paymentContext = paymentContext;
    }

    public void Add<T>(T entity) where T : class
    {
        _paymentContext.Set<T>().Add(entity);
        _paymentContext.SaveChanges();
    }

    public void Update<T>(T entity) where T : class
    {
        _paymentContext.Set<T>().Update(entity);
        _paymentContext.SaveChanges();
    }

    public void Delete<T>(T entity) where T : class
    {
        _paymentContext.Set<T>().Remove(entity);
        _paymentContext.SaveChanges();
    }

    public List<T> GetAll<T>() where T : class
    {
        return _paymentContext.Set<T>().ToList();
    }

    public T GetSingle<T>(int id) where T : class
    {
        return _paymentContext.Set<T>().Find(id);
    }
}
