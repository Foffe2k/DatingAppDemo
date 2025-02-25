using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //Borttaget när de härleder från BaseApiController eftersom BAC står för att sätta attributen
    //[Route("api/[controller]")] //[controller] ersätts med det som står innan Controller i klassnamnet (här "Users" i "UsersController")
    //[ApiController]     
    public class UsersController(DataContext context) : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet] //Path: api/Users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //async för att markera synckront flöde. Wrappa return statement i Task<> för visa vad som ska vara asynkront. Metodnamnet ska inte wrappas
        { 
            var users = await context.Users.ToListAsync(); //await framför det asynkrona jobbet. Ändra metodnamnet till Async version.

            return users;
        }
        
        [Authorize]
        [HttpGet("{id:int}")] //Path: api/Users/# Också hårt typad som int i detta fall
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) return NotFound();

            return user;
        }
        
    }
}
