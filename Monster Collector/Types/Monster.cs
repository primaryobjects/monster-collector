using System;
using System.ComponentModel.DataAnnotations;

public class Monster
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Monster()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        Id = Guid.NewGuid().ToString();
        Name = Constants.MonsterNames[rand.Next(Constants.MonsterNames.Count)];
        Description = "A big scary " + Name;
        Health = rand.Next(20, 101);
        Attack = rand.Next(10, 51);
        Defense = rand.Next(5, 26);
    }

    public override string ToString()
    {
        return "(" + Id + ") Health " + Health + ", Attack " + Attack + ", Defense " + Defense;
    }
}