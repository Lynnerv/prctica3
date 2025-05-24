using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prctica3.Models
{
    public class PostEnriquecido
    {
    public Post Post { get; set; }
    public User Autor { get; set; }
    public List<Comment> Comentarios { get; set; }   
    }
}