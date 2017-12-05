using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace LauncherServer.Models
{
    public class Game
    {
        // represents a game on steam
        public int id { get; set; }
        public int steamId { get; set; }
        public string name { get; set; }
        public string exe { get; set; }
        public string genre { get; set; }
        public virtual List<SteamUser> users { get; set; }
    }

    public class SteamUser
    {
        // represents a user login for steam
        public int id { get; set; }
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string salt { get; set; }
        public bool inUse { get; set; }
        public Computer inUseBy { get; set; }
        public virtual List<Game> games { get; set; }

    }

    public class Computer
    {
        // represents a computer that can request logins
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string ip { get; set; }
        public string key { get; set; } // for api calls
        public string secret { get; set; } // for encoding the password for on the fly (needed?)
        public bool authorized { get; set; }
    }

    public class LauncherDbContext : DbContext
    {
        public LauncherDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<SteamUser> SteamUsers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Computer> Computers { get; set; }
    }

    public class GameStartViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string exe { get; set; }
        public int steamId { get; set; }
        public string status { get; set; }
        public string name { get; set; }
    }

    public class StatusViewModel
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class GameViewModel
    {
        public int id { get; set; }
        public int steamId { get; set; }
        public string name { get; set; }
        public string exe { get; set; }
        public string genre { get; set; }
    }
}