using CrudNest.Data;
using CrudNest.Models;
using CrudNest.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudNest.Controllers
{
    [Route("api/[controller]")] //https:/localhost:7***/api/Employees
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)//Constructor injection
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
           var allEmployees= dbContext.Employee.ToList();

            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeebyId(Guid id) 
        {
          var employee = dbContext.Employee.Find(id);

            if (employee == null) 
            {
                return NotFound("Employee not Found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto) //I am using Dto because of separation of concerns(reusable)
        {
            var employeeEntity = new Employee()
            {
                Email = addEmployeeDto.Email,
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };


            dbContext.Employee.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
            

        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id , UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employee.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not Found");
            }

            employee.Name = updateEmployeeDto.Name; //i am updating the propeties here 
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteEmployee(Guid id) 
        {
            var employee = dbContext.Employee.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not Found");
            }

            dbContext.Employee.Remove(employee);
            dbContext.SaveChanges();

            return Ok("Employee deleted successfully");

        }
    }
}
