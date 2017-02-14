using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToMany.Data;
using ManyToMany.Models;
using ManyToMany.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToMany.API
{
    [Route("api/[controller]")]
    public class ActorsController : Controller
    {
        ApplicationDbContext _db;

        public ActorsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Actor> Get()
        {
            return _db.Actors.ToList();
        }

        [HttpGet("{id}")]
        public ActorWithMovies Get(int id)
        {
            ActorWithMovies actor = (from a in _db.Actors
                                     where a.Id == id
                                     select new ActorWithMovies
                                     {
                                         Id = a.Id,
                                         FirstName = a.FirstName,
                                         LastName = a.LastName,
                                         Movies = (from am in _db.MovieActors
                                                   where am.ActorId == id
                                                   select am.Movie).ToList()
                                     }).FirstOrDefault();
            return actor;
        }
       
    }
}
