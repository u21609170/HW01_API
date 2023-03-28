using HW01.Data;
using HW01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly HW01DbContext hW01DbContext;

        public CoursesController(HW01DbContext hW01DbContext)
        {
            this.hW01DbContext = hW01DbContext;
        }
        
        [HttpGet]

        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await this.hW01DbContext.Courses.ToListAsync();

            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course courseRequest)
        {
            courseRequest.id = Guid.NewGuid();
            await this.hW01DbContext.Courses.AddAsync(courseRequest);
            await this.hW01DbContext.SaveChangesAsync();
            return Ok(courseRequest);
        }

        [HttpGet]

        [Route("{id:GUID}")]
        public async Task<IActionResult> GetModuleByID([FromRoute] Guid id)
        {
            var course = await this.hW01DbContext.Courses.FirstOrDefaultAsync(x => x.id == id);

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(course);
            }

        }

        [HttpPut]
        [Route("{id:GUID}")]
        public async Task<IActionResult> UpdateModule([FromRoute] Guid id, Course updateCourseRequest)
        {
            var course = await this.hW01DbContext.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound(id);
            }

            course.Name = updateCourseRequest.Name;
            course.Duration = updateCourseRequest.Duration;
            course.Description = updateCourseRequest.Description;

            await this.hW01DbContext.SaveChangesAsync();

            return Ok(updateCourseRequest);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteModule([FromRoute] Guid id)
        {
            var course = await this.hW01DbContext.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound(id);
            }

            this.hW01DbContext.Courses.Remove(course);
            await this.hW01DbContext.SaveChangesAsync();

            return Ok(course);
        }
    }
}
