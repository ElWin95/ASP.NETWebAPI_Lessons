using GenericRepoPractice.Domain.Entities;
using GenericRepoPractice.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepoPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.TeacherRepository.GetAll());
        }
        [HttpPost]
        public IActionResult Create()
        {
            _unitOfWork.TeacherRepository.Add(new Teacher { Name="teacher4"});
            _unitOfWork.Save();
            return Ok(201);
        }
    }
}
