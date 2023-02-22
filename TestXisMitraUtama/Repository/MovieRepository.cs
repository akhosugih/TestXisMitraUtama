using Microsoft.EntityFrameworkCore;
using TestXisMitraUtama.Database;
using TestXisMitraUtama.Interface;
using TestXisMitraUtama.Model;
using TestXisMitraUtama.ViewModel;

namespace TestXisMitraUtama.Repository
{
    public class MovieRepository
    {
        private DataContext context;

        public MovieRepository(DataContext dataContext)
        {
            this.context = dataContext;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var data = await context.Movies.FindAsync(id);
                if (data == null)
                    return false;

                context.Movies.Remove(data);

                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

        }

        public async Task<Movie> Get(long id)
        {
            return await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Movie>> GetList()
        {
            return await context.Movies.ToListAsync();
        }

        public async Task<bool> Insert(VMovie data, string imageName)
        {
            try
            {
                var movie = new Movie()
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Title = data.Title,
                    Description = data.Description,
                    Rating = data.Rating
                };

                if (!string.IsNullOrEmpty(imageName))
                    movie.Image = imageName;

                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<bool> Update(long id, VMovie data, string imageName)
        {
            try
            {
                var movie = await context.Movies.FindAsync(id);
                movie.Title = data.Title;
                movie.Description = data.Description;
                movie.Rating = data.Rating;
                movie.Image = string.IsNullOrEmpty(imageName) ? string.Empty : imageName;
                movie.UpdatedAt = DateTime.Now;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
