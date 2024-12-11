using System;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; } //Om en property heter Id tolkar EF det som om att det är tabellens primary key. Datatypen är int så autoincremenatar den värdet
    public required string UserName { get; set; }
}
