using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface IServicesService
{
    public Service CreateService(Service service);
    public Service UpdateService(Service service);
    public Service DeleteService(Service service);
    public Service GetServiceById(string name);
    public List<Service> GetServices();
    
}