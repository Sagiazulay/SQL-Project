using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil5b
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string m_conn = "Host=localhost;Username=postgres;Password=admin;Database=postgres";

                ActorsDAO d = new ActorsDAO(m_conn);
            // d.AddActor(6,"Sagi", DateTime.Now);
            //d.UpdateActor(6, "Sagi Azulay");
            //d.DeleteActor(6);
            //d.GetAllActors();
            //d.GetActorByID(6);
            GenresDAO g = new GenresDAO(m_conn);
            //g.AddGenre(6, "DOCO");
            //g.UpdateGenre(6, "Doco");
            //g.DeleteGenre(6);
            //g.GetAllGenres();
            //g.GetGenreByID(3);
            MoviesDAO m = new MoviesDAO(m_conn);
            //m.AddMovie(6, "SAGI", DateTime.Now, 2);
            //m.GetAllMovies();
            //m.GetMovieByID(1);
            //m.UpdateMovie(1, "Bad Boyz");


            
        }
    }
}
