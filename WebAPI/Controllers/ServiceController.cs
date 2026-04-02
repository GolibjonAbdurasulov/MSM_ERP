using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServicesService _servicesService;

    public ServiceController(IServicesService servicesService)
    {
        _servicesService = servicesService;
    }

    // Barcha servislarni olish: GET /api/Service
    [HttpGet("getall")]
    public ActionResult<List<Service>> GetAll()
    {
        return Ok(_servicesService.GetServices());
    }

    // Nomi bo'yicha bitta servisni olish: GET /api/Service/ETL
    [HttpGet("getbyname")]
    public ActionResult<Service> GetByName(string name)
    {
        var service = _servicesService.GetServiceById(name);
        if (service == null) return NotFound("Servis topilmadi.");
        return Ok(service);
    }

    // Yangi servis qo'shish: POST /api/Service
    [HttpPost("create")]
    public ActionResult<Service> Create([FromBody] Service service)
    {
        var created = _servicesService.CreateService(service);
        return CreatedAtAction(nameof(GetByName), new { name = created.Name }, created);
    }

    // Servisni tahrirlash: PUT /api/Service
    [HttpPut("update")]
    public ActionResult<Service> Update([FromBody] Service service)
    {
        var updated = _servicesService.UpdateService(service);
        if (updated == null) return NotFound("Yangilanadigan servis topilmadi.");
        return Ok(updated);
    }

    // Servisni o'chirish: DELETE /api/Service
    [HttpDelete("delete")]
    public ActionResult<Service> Delete([FromBody] Service service)
    {
        var deleted = _servicesService.DeleteService(service);
        if (deleted == null) return NotFound("O'chiriladigan servis topilmadi.");
        return Ok(deleted);
    }
}
