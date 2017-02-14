using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToMany.Models
{
    public class MovieActor
    {
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Actor Actor { get; set; }
        public int ActorId { get; set; }

    }
}
