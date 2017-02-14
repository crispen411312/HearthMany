using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToMany.Data;
using ManyToMany.ViewModels;
using ManyToMany.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToMany.API
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        // this is only here bacuse we are not using repo or services 
        private ApplicationDbContext _db;
        //Constructor
        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Movie> Get()
        {
            return _db.Movies.ToList();
        }

        [HttpGet("{id}")]
        public MovieWithActors Get(int id)
        {
            MovieWithActors Mov = (from m in _db.Movies
                                   where m.Id == id
                                   select new MovieWithActors
                                   {
                                       Id = m.Id,
                                       Title = m.Title,
                                       Director = m.Director,
                                       Actors = (from ma in _db.MovieActors // this is the join table
                                                 where ma.MovieId == m.Id // where the id's match
                                                 select ma.Actor).ToList() // select the actor and add to list
                                   }).FirstOrDefault(); // return first or default like normal.
            return Mov;
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id,[FromBody]Movie mov)
        {
            if (mov == null)
            {
                return BadRequest();
            } else if (mov.Id == 0)
            {
                Movie tempMov = new Movie
                {
                    Title = mov.Title,
                    Director = mov.Director,
                };
                _db.Movies.Add(tempMov);
                _db.SaveChanges();
                _db.MovieActors.Add(new MovieActor
                {
                    MovieId = id,
                    ActorId = // ahhhh needs to pass in the actor id. 
                })
                

                //foreach (Actor actor in mov.) // --------------- to select 
                //{ 
                //     _db.MovieActors.Add(new MovieActor
                //     {
                //         MovieId = movLookUp.Id, //-------------------this needs to be the newly assigned move id.
                //         ActorId = actor.Id
                //     });
                //    _db.SaveChanges();
                //}


                return Ok();

            } else
            {
                return BadRequest();
            }
        }


    }
}
