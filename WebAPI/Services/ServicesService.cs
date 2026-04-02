using System.Text.Json;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class ServicesService : IServicesService
{
    private readonly string _filePath;

    public ServicesService()
    {
        // Loyiha ishga tushgan asosiy (Root) papkani oladi
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;

        // Root ichidagi "DateBase" papkasiga yo'l hosil qiladi
        _filePath = Path.Combine(rootPath, "DateBase", "services.json");

        // Agar "DateBase" papkasi yo'q bo'lsa, uni yaratadi
        string directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private List<Service> LoadData()
    {
        Console.WriteLine($"Qidirilayotgan yo'l: {_filePath}"); // Buni qo'shing
        if (!File.Exists(_filePath)) 
        {
            Console.WriteLine("Xato: Fayl topilmadi!"); // Buni qo'shing
            return new List<Service>();
        }
        var json = File.ReadAllText(_filePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    
        var result = JsonSerializer.Deserialize<List<Service>>(json, options);
        Console.WriteLine(result.Count);
        return result ?? new List<Service>();
    }


    // Ma'lumotlarni faylga yozish
    private void SaveData(List<Service> services)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(services, options);
        File.WriteAllText(_filePath, json);
    }

    public List<Service> GetServices()
    {
        return LoadData();
    }

    public Service GetServiceById(string name)
    {
        return LoadData().FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public Service CreateService(Service service)
    {
        var services = LoadData();
        // Avtomatik ID generatsiya
        service.Id = services.Any() ? services.Max(s => s.Id) + 1 : 1;
        services.Add(service);
        SaveData(services);
        return service;
    }

    public Service UpdateService(Service service)
    {
        var services = LoadData();
        var index = services.FindIndex(s => s.Id == service.Id);
        
        if (index != -1)
        {
            services[index] = service;
            SaveData(services);
            return service;
        }
        return null;
    }

    public Service DeleteService(Service service)
    {
        var services = LoadData();
        var toDelete = services.FirstOrDefault(s => s.Id == service.Id);
        
        if (toDelete != null)
        {
            services.RemoveAll(s => s.Id == service.Id);
            SaveData(services);
        }
        return toDelete;
    }
}
