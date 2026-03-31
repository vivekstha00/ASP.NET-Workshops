using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.DTOs;
using ProductAPI;

namespace WeatherAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _context;     

    public CustomerController(AppDbContext context)
    {
        _context = context;
    }   

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _context.Customers
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,      
                Email = c.Email,
                Phone = c.Phone
            })
            .ToListAsync();         

        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto customerDto)
    {
        var customer = new Customer
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Email = customerDto.Email,
            Phone = customerDto.Phone   
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = customer.Id }, customer);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();        
        return Ok(new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();        
        customer.FirstName = dto.FirstName;
        customer.LastName = dto.LastName;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;
        await _context.SaveChangesAsync();  
        return NoContent(); 
    }   

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();            
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return NoContent();
    }   

}