using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prctica3.Models
{
    public class Post
    {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; } 
    }
}