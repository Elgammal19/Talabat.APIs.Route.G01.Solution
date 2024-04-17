using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.Employee_Specs;

namespace Talabat.APIs.Controllers
{
	public class EmployeesController : BaseApiController
	{
		private readonly IGenericRepository<Employee> _empRepo;

		public EmployeesController(IGenericRepository<Employee> empRepo)
        {
			_empRepo = empRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
		{
			var spec = new EmployeeWithDepartmentSpecifications();
			var emp = await _empRepo.GetAllWithSpecAsync(spec);

			return Ok(emp);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> GetEmployeeById(int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications(id);
			var emp = await _empRepo.GetByIdWithSpecAsync(spec);

			if (emp is null) return NotFound();

			return Ok(emp);
		}

    }
}
