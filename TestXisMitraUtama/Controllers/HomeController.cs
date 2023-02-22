using System.IO;
using Microsoft.AspNetCore.Mvc;
using TestXisMitraUtama.Model;
using TestXisMitraUtama.Repository;
using TestXisMitraUtama.ViewModel;

namespace TestXisMitraUtama.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private readonly MovieRepository _repository;

        public HomeController(ILogger<HomeController> logger, MovieRepository repository, IWebHostEnvironment env)
        {
            _logger = logger;
            _repository = repository;
            _env = env;
        }

        [HttpGet("GetMovieList")]
        public async Task<IActionResult> GetMovie()
        {
            try
            {
                var movies = await _repository.GetList();
                return Ok(new
                {
                    data = movies
                });
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                return BadRequest();
            }
        }

        
        [HttpGet("GetMovieDetail/{id}")]
        public async Task<IActionResult> GetDetail(long id)
        {
            try
            {
                var movie = await _repository.Get(id);
                return Ok(new
                {
                    data = movie
                });
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                return BadRequest();
            }
        }

        
        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie ([FromForm]VMovie data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input");

            try
            {
                var path = string.Empty;
                if (data.Image.Length > 0)
                {
                    path = $"{_env.ContentRootPath}images\\{data.Image.FileName}";
                    if (!System.IO.File.Exists(path))
                        await data.Image.CopyToAsync(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                }


                var movie = await _repository.Insert(data, path);
                return Ok(new
                {
                    data = movie
                });
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                return BadRequest();
            }
        }

        [HttpPatch("UpdateMovie/{id}")]
        public async Task<IActionResult> UpdateMovie(long id, [FromForm] VMovie data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input");
            try
            {
                var path = string.Empty;
                if (data.Image.Length > 0)
                {
                    path = $"{_env.ContentRootPath}images\\{data.Image.FileName}";
                    if (!System.IO.File.Exists(path))
                        await data.Image.CopyToAsync(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                }
                

                var movie = await _repository.Update(id, data, path);
                return Ok(new
                {
                    data = movie
                });
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                return BadRequest();
            }
        }

        [HttpDelete("DeleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie (long id)
        {
            try
            {
                var success = await _repository.Delete(id);

                if (!success)
                    return BadRequest();
                return Ok();
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                return BadRequest();
            }
        }
    }
}