using System;
using System.Collections.Generic;
using System.Text;

namespace Domca.EntityFrameworkCore.Models;

internal class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Tracker> Trackers { get; set; }
    public User()
    {
        Trackers = new List<Tracker>();
    }
}
