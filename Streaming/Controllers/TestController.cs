using Microsoft.AspNetCore.Mvc;
using Streaming.Database;
using Streaming.Entities;

namespace Streaming.Controllers
{
    [ApiController]
    [Route("api/test-sensor")]
    public class TestController : ControllerBase
    {
        private readonly StreamingDbContext _context;

        public TestController(StreamingDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestSensor()
        {
            var testSensor = new SensorData
            {
                Id = Guid.NewGuid(),
                WorkstationId = 1,
                WorkorderId = null, // Test için boş
                SensorTypeId = 0, // Temperature (örnek)
                SensorValue = 36.7,
                Timestamp = DateTime.UtcNow
            };

            await _context.SensorDatas.AddAsync(testSensor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Test sensor data inserted successfully." });
        }
    }
}