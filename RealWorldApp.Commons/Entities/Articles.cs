﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Entities;

public class Articles 
{

    public int Id { get; set; }


    public string Title { get; set; }
    public string Text { get; set; }
    public List<User> Username { get; set; }
    public List<Comments> Comment { get; set; }
    public List<Tags> Tag { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Favorited { get; set; }
    public int FavoritesCount { get; set; }
    public string Slug { get; set; }
  

}